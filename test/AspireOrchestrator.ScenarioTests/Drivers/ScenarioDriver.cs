using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.Parsing.WebApi.Controllers;
using AspireOrchestrator.PersistenceTests.Common;
using AspireOrchestrator.ScenarioTests.Processors;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Reqnroll;

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
            var flowRepository = new FlowRepository(OrchestratorContext, TestLoggerFactory.CreateLogger<FlowRepository>());
            _eventRepository = new EventRepository(OrchestratorContext, logger);
            var receiptDetailRepository = new ReceiptDetailRepository(DomainContext,
                TestLoggerFactory.CreateLogger<ReceiptDetailRepository>());
            var containerClient = GetContainerClient().Result;
            var parseController = new ParseController(containerClient, receiptDetailRepository, TestLoggerFactory);
            var processorFactory = new TestProcessorFactory(parseController, TestLoggerFactory);
            _workFlowProcessor = new WorkFlowProcessor(processorFactory, _eventRepository, flowRepository, TestLoggerFactory);
        }

        private async Task<BlobServiceClient> GetContainerClient()
        {
            var endpoint = new Uri("https://127.0.0.1:64537/devstoreaccount1");
            var credential = new DefaultAzureCredential();
            var blobServiceClient = new BlobServiceClient(endpoint, credential, new BlobClientOptions());
            var properties = await blobServiceClient.GetPropertiesAsync().ConfigureAwait(false);
            return blobServiceClient;
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
            var flowId = entity.FlowId.Value;
            _scenarioContext["event"] = await _workFlowProcessor.ProcessFlow(flowId);
        }
    }
}
