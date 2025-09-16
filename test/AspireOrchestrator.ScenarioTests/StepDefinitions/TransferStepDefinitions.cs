using AspireOrchestrator.ScenarioTests.Drivers;
using Reqnroll;

namespace AspireOrchestrator.ScenarioTests.StepDefinitions
{
    [Binding]
    public class TransferStepDefinitions(ScenarioDriver scenarioDriver)
    {
        [When("Transfers er blevet afsendt")]
        public async Task WhenTransfersErBlevetAfsendt()
        {
            await scenarioDriver.TransferAll();
        }

        [When("Pensionskernen har svaret")]
        public async Task WhenPensionskernenHarSvaret()
        {
            await scenarioDriver.CreateReplies();
        }

        [When("Svarene er behandlet")]
        public async Task WhenSvareneErBehandlet()
        {
            await scenarioDriver.HandleReplies();
        }

    }
}
