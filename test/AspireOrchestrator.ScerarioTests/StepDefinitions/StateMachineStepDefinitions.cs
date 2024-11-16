using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;

namespace AspireOrchestrator.ScerarioTests.StepDefinitions
{
    [Binding]
    public class StateMachineStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private EventEntity _eventEntity;
        public StateMachineStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        
        [When(@"the event with EventType (.*) and ProcessState (.*)")]
        public void WhenTheEventWithEventTypeHandleOsInfoAndProcessStateReceive(EventType eventType, ProcessState processState)
        {
            _eventEntity = new EventEntity
            {
                Id = Guid.NewGuid(),
                EventType = eventType,
                ProcessState = processState
            };
        }

        [Then(@"the ProcessState of the next event is (.*)")]
        public void ThenTheProcessStateOfTheNextEventIsParse(ProcessState processState)
        {
            var result = StateMap.GetNextStep(_eventEntity);
            result.Should().Be(processState);
        }
    }
}
