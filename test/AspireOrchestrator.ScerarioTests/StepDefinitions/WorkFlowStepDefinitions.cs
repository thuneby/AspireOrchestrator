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

        [When(@"the next event is processed")]
        public async Task WhenTheEventIsProcessed()
        {
            await _scenarioDriver.WhenEventIsProcessed(); 
        }

        [When(@"the flow has been processed")]
        public async Task WhenTheFlowHasBeenProcessed()
        {
            await _scenarioDriver.WhenFlowHasBeenProcessed();
        }

        [Then(@"the event should be in the following state")]
        public void ThenTheEventShouldHaveTheFollowingState(Table eventTable)
        {
            _scenarioDriver.ThenEventShouldHaveState(eventTable);
        }

        [Then(@"the following events should be in the event store")]
        public void ThenTheFollowingEventsShouldBeInTheEventStore(Table eventTable)
        {
            _scenarioDriver.ThenEventsShouldBeInTheEventStore(eventTable);
        }

        [Then(@"(EventEntity|Tenant) table contains rows")]
        public void ThenTableNameContainsRows(TableName tableName, Table specFlowTable)
        {
            _scenarioDriver.ThenTableNameContainsRows(tableName, specFlowTable);
        }
    }
}
