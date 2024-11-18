using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.PersistenceTests.Common;
using Microsoft.Extensions.Logging;
using TechTalk.SpecFlow.Assist;

namespace AspireOrchestrator.ScenarioTests.Drivers
{
    public class ScenarioDriver: TestBase
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly EventRepository _eventRepository;
        private readonly WorkFlowProcessor _workFlowProcessor;

        public ScenarioDriver(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var logger = TestLoggerFactory.CreateLogger<EventRepository>();
            _eventRepository = new EventRepository(OrchestratorContext, logger);
            _workFlowProcessor = new WorkFlowProcessor(_eventRepository, TestLoggerFactory);
        }

        public void GivenEvent(Table eventTable)
        {
            var entity = eventTable.CreateInstance<EventEntity>();
            _scenarioContext["event"] = entity;
            _eventRepository.AddEvent(entity);
        }

        public async Task WhenEventIsProcessed()
        {
            var entity = _eventRepository.GetEventsByTenant(1).First(x => x.EventState == EventState.New);
            _scenarioContext["event"] = await _workFlowProcessor.ProcessEvent(entity); 
        }

        public void ThenEventShouldHaveState(Table eventTable)
        {
            var expected = eventTable.CreateSet<EventEntity>().First();
            var actual = _scenarioContext.Get<EventEntity>("event");
            Assert.Equal(expected.ProcessState, actual.ProcessState);
        }

        internal void ThenEventsShouldBeInTheEventStore(Table eventTable)
        {
            var expected = eventTable.CreateSet<EventEntity>();
            var actual = _eventRepository.GetList();
            Assert.Equal(expected.Count(), actual.Count());


        }

        [Then(@"(EventEntity) table contains")]
        public void ThenTableNameContainsRows(TableName tableName, Table specFlowTable)
        {
            switch (tableName)
            {
                case TableName.EventEntity:
                    ThenGuidContainsRows<EventEntity>(specFlowTable, OrchestratorContext);
                    break;
                case TableName.Tenant:
                    ThenTableContainsRows<Tenant>(specFlowTable, OrchestratorContext);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tableName), tableName, null);
            }
        }

        private static void ThenGuidContainsRows<T>(Table specFlowTable, OrchestratorContext context)
            where T : Entity<Guid>
        {
            var entities = context.Set<T>().ToList();
            specFlowTable.CompareToSet(entities);
        }

        private static void ThenTableContainsRows<T>(Table specFlowTable, OrchestratorContext context)
            where T : Entity<long>
        {
            var entities = context.Set<T>().ToList();
            specFlowTable.CompareToSet(entities);
        }

        public async Task WhenFlowHasBeenProcessed()
        {
            _scenarioContext.TryGetValue("event", out EventEntity entity);
            var flowId = entity.FlowId;
            _scenarioContext["event"] = await _workFlowProcessor.ProcessFlow(flowId);
        }
    }
}
