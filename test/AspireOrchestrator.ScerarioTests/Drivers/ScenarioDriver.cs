using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.PersistenceTests.Common;
using Microsoft.Extensions.Logging;
using TechTalk.SpecFlow.Assist;

namespace AspireOrchestrator.ScerarioTests.Drivers
{
    public class ScenarioDriver: TestBase
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Tenant _baseTenant = new Tenant { Id = 1, TenantName = "Test Tenant" };
        private readonly EventRepository _eventRepository;
        private readonly WorkFlowProcessor _workFlowProcessor;

        public ScenarioDriver(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var logger = TestLoggerFactory.CreateLogger<EventRepository>();
            _eventRepository = new EventRepository(OrchestratorContext, logger, _baseTenant);
            _workFlowProcessor = new WorkFlowProcessor(_eventRepository, new ProcessorFactory(TestLoggerFactory), 
                TestLoggerFactory.CreateLogger<WorkFlowProcessor>());
        }

        public void GivenEvent(Table eventTable)
        {
            var entity = eventTable.CreateInstance<EventEntity>();
            _scenarioContext["event"] = entity;
            _eventRepository.AddEvent(entity);
        }

        public async Task WhenEventIsProcessed()
        {
            var entity = _scenarioContext.Get<EventEntity>("event");
            _scenarioContext["event"] = await _workFlowProcessor.ProcessEvent(entity); 
        }

        public void ThenEventShouldHaveState(Table eventTable)
        {
            var expected = eventTable.CreateSet<EventEntity>().First();
            var actual = _scenarioContext.Get<EventEntity>("event");
            Assert.Equal(expected.ProcessState, actual.ProcessState);
        }
    }
}
