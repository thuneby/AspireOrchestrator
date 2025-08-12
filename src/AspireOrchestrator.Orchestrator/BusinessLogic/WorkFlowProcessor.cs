using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public class WorkFlowProcessor(IEventRepository eventRepository, IFlowRepository flowRepository, ILoggerFactory loggerFactory)
    {
        private readonly ILogger<WorkFlowProcessor> _logger = loggerFactory.CreateLogger<WorkFlowProcessor>();
        private readonly ProcessorFactory _processorFactory = new(loggerFactory);


        public async Task<EventEntity> ProcessFlow(Guid flowId)
        {
            var eventEntity = eventRepository.GetNextEvent(flowId);
            if (eventEntity == null)
            {
                _logger.LogError("Event ikke fundet for flowId {0}", flowId);
                return null;
            }
            var flow = flowRepository.Get(flowId);
            if (flow?.State == FlowState.New)
            {
                flow.State = FlowState.Active;
                flowRepository.Update(flow);
            }
            do
            {
                eventEntity = await ProcessEvent(eventEntity);
                if (eventEntity.EventState is EventState.Error or EventState.Processing)
                    return eventEntity;
            } while (eventEntity.ProcessState != ProcessState.WorkFlowCompleted);

            if (eventEntity.EventState != EventState.Completed || flow?.State != FlowState.Active) return eventEntity;
            flow.State = FlowState.Completed;
            flowRepository.Update(flow);
            return eventEntity;
        }

        public async Task<EventEntity> ProcessEvent(EventEntity eventEntity, bool returnNewEvent = true)
        {
                if (eventEntity.ProcessState == ProcessState.WorkFlowCompleted)
                    return eventEntity;
                eventEntity = await DoProcessing(eventEntity);
                return await UpdateEvent(eventEntity, returnNewEvent);
        }

        public async Task<EventEntity> UpdateEvent(EventEntity eventEntity, bool returnNewEvent = true)
        {
            try
            {
                if (eventEntity.EventState == EventState.Completed && eventEntity.ProcessState != ProcessState.WorkFlowCompleted)
                {
                    var newEvent = GetNextEvent(eventEntity);
                    // Update current event
                    eventRepository.Update(eventEntity);
                    // Add new event
                    eventRepository.AddEvent(newEvent);
                    return returnNewEvent ? newEvent : eventEntity;
                }
                eventRepository.Update(eventEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                eventEntity.ErrorMessage = e.Message;
                eventEntity.EventState = EventState.Error;
            }
            return eventEntity;
        }

        public async Task<EventEntity> DoProcessing(EventEntity entity)
        {
            if (entity.EventState == EventState.Completed)
                return entity;
            entity.StartEvent();
            eventRepository.Update(entity); // make sure nobody else takes it
            var processor = _processorFactory.GetProcessor(entity);
            if (processor == null)
            {
                entity.ErrorMessage = "Processor not found for event!";
                entity.EventState = EventState.Error;
                return entity;
            }
            var result = await processor.ProcessEvent(entity);
            return result;
        }

        private static EventEntity GetNextEvent(EventEntity originalEvent)
        {
            if (originalEvent.ProcessState == ProcessState.WorkFlowCompleted)
                return originalEvent;
            var entity = new EventEntity
            {
                Id = Guid.NewGuid(),
                EventState = EventState.New,
                EventType = originalEvent.EventType,
                FlowId = originalEvent.FlowId,
                TenantId = originalEvent.TenantId,
                DocumentType = originalEvent.DocumentType,
                ProcessState = StateMap.GetNextStep(originalEvent),
                Parameters = originalEvent.Result
            };
            if (entity.ProcessState != ProcessState.WorkFlowCompleted) return entity;
            entity.EventState = EventState.Completed;
            entity.StartTime = entity.CreatedDate;
            entity.EndTime = entity.CreatedDate;
            entity.Result = "Workflow Completed";
            return entity;
        }
    }
}
