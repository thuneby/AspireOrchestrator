using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.DataAccess;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public class WorkFlowProcessor
    {
        private readonly EventRepository _eventRepository;
        private readonly ILogger<WorkFlowProcessor> _logger;
        private readonly ProcessorFactory _processorFactory;

        public WorkFlowProcessor(EventRepository eventRepository, ProcessorFactory processorFactory, ILogger<WorkFlowProcessor> logger)
        {
            _eventRepository = eventRepository;
            _processorFactory = processorFactory;
            _logger = logger;
        }


        public async Task<EventEntity> ProcessFlow(long flowId)
        {
            var eventEntity = _eventRepository.GetNextEvent(flowId);
            if (eventEntity == null)
            {
                _logger.LogError("Event ikke fundet for flowId {0}", flowId);
                return null;
            }

            do
            {
                eventEntity = await ProcessEvent(eventEntity);
                if (eventEntity.State == EventState.Error || eventEntity.State == EventState.Processing)
                    return eventEntity;
            } while (eventEntity.ProcessState != ProcessState.WorkFlowCompleted);
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
                if (eventEntity.State == EventState.Completed)
                {
                    var newEvent = GetNextEvent(eventEntity);
                    // Update current event
                    _eventRepository.Update(eventEntity);
                    // Add new event
                    _eventRepository.AddOrUpdateEventEntity(newEvent);
                    return returnNewEvent ? newEvent : eventEntity;
                }
                _eventRepository.Update(eventEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                eventEntity.ErrorMessage = e.Message;
                eventEntity.State = EventState.Error;
            }
            return eventEntity;
        }

        public async Task<EventEntity> DoProcessing(EventEntity entity)
        {
            if (entity.State == EventState.Completed)
                return entity;
            entity.StartEvent();
            _eventRepository.Update(entity); // make sure nobody else takes it
            var processor = _processorFactory.GetProcessor(entity);
            if (processor == null)
            {
                entity.ErrorMessage = "Processor not found for event!";
                entity.State = EventState.Error;
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
                State = EventState.New,
                EventType = originalEvent.EventType,
                FlowId = originalEvent.FlowId,
                TenantId = originalEvent.TenantId,
                DocumentType = originalEvent.DocumentType,
                ProcessState = StateMap.GetNextStep(originalEvent),
                Parameters = originalEvent.Result
            };
            if (entity.ProcessState != ProcessState.WorkFlowCompleted) return entity;
            entity.State = EventState.Completed;
            entity.StartTime = entity.CreatedDate;
            entity.EndTime = entity.CreatedDate;
            entity.Result = "Workflow Completed";
            return entity;
        }
    }
}
