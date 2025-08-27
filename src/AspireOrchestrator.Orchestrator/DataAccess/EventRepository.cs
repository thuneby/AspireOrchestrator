using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.DataAccess.Repositories;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.DataAccess
{
    public class EventRepository(OrchestratorContext context, ILogger<EventRepository> logger)
        : GuidRepositoryBase<EventEntity>(context, logger), IEventRepository
    {
        public void AddOrUpdateEventEntity(EventEntity eventEntity)
        {
            var existingEntity = context.EventEntity.Any(x => x.Id == eventEntity.Id);
            if (existingEntity == true)
            {
                context.Update(eventEntity);
                context.SaveChanges();
            }
            else
            {
                if ( !context.EventEntity.Any(x => x.FlowId == eventEntity.FlowId && x.ProcessState == eventEntity.ProcessState))
                    AddEvent(eventEntity);
            }

        }

        public IEnumerable<EventEntity> GetEventsByTenant(int tenantId)
        {
            var events = Query().Where(x => x.TenantId == tenantId).ToList();
            return events;
        }

        public void AddEvent(EventEntity eventEntity)
        {
            if (eventEntity.FlowId == null)
            {
                var flow = new Flow()
                {
                    CreatedDate = eventEntity.CreatedDate
                };
                context.Add(flow);
                context.SaveChanges(); // FlowId is updated after saving
                eventEntity.FlowId = flow.Id;
            }
            Add(eventEntity);
        }

        public EventEntity AddOrGetEventFromFileName(long tenantId, string fileName, EventType eventType)
        {
            var existing = context.EventEntity
                .FirstOrDefault(x => x.TenantId == tenantId && x.ProcessState == ProcessState.Receive && x.EventType == eventType &&
                                     !string.IsNullOrWhiteSpace(x.Parameters) && x.Parameters == fileName);
            if (existing != null && existing.EventState != EventState.Completed)
                return existing;
            var documentType = GetDocumentType(eventType);
            var eventEntity = new EventEntity
            {
                EventType = eventType,
                TenantId = tenantId,
                ProcessState = ProcessState.Receive,
                Parameters = fileName,
                DocumentType = documentType
            };
            AddEvent(eventEntity);
            return eventEntity;
        }

        public EventEntity? GetEvent(Guid id)
        {
            return Get(id);
        }

        public EventEntity? GetNextEvent(Guid flowId)
        {
            var flowEvents = GetEventFlow(flowId);
            var nextEvent = flowEvents.FirstOrDefault(x => x.EventState != EventState.Completed);
            return nextEvent;
        }

        public IEnumerable<EventEntity> GetEventFlow(Guid flowId)
        {
            return context.EventEntity.Where(x => x.FlowId == flowId).OrderBy(x => x.ProcessState);
        }

        private static DocumentType GetDocumentType(EventType eventType)
        {
            return eventType switch
            {

                _ => throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null)
            };
        }

        public IEnumerable<EventEntity> GetAll(int take)
        {
            return GetList(take);
        }
    }
}
