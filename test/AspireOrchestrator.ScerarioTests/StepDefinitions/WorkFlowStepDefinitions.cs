using AspireOrchestrator.ScerarioTests.Drivers;

namespace AspireOrchestrator.ScerarioTests.StepDefinitions
{
    [Binding]
    public class WorkFlowStepDefinitions
    {
        private readonly ScenarioDriver _scenarioDriver;

        public WorkFlowStepDefinitions(ScenarioDriver scenarioDriver)
        {
            _scenarioDriver = scenarioDriver;
        }


        [Given(@"the following event")]
        public void GivenTheFollowingEvent(Table eventTable)
        { 
            _scenarioDriver.GivenEvent(eventTable);
        }

        [When(@"the event is processed")]
        public async void WhenTheEventIsProcessed()
        {
            await _scenarioDriver.WhenEventIsProcessed();
        }

        [Then(@"the event should be in the following state")]
        public void ThenTheEventShouldHaveTheFollowingState(Table eventTable)
        {
            _scenarioDriver.ThenEventShouldHaveState(eventTable);
        }
    }
}
