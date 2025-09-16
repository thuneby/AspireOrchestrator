using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using Reqnroll;

namespace AspireOrchestrator.ScenarioTests.StepDefinitions
{
    [Binding]
    public class StateMachineStepDefinitions(ScenarioContext scenarioContext)
    {
        private readonly ScenarioContext _scenarioContext = scenarioContext;
        private EventEntity _eventEntity = new();

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

        [Then(@"^the ProcessState of the next event is (.*)$")]
        public void ThenTheProcessStateOfTheNextEventIsParse(ProcessState processState)
        {
            var result = StateMap.GetNextStep(_eventEntity);
            result.Should().Be(processState);
        }
    }
}
