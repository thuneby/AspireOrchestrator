using AspireOrchestrator.ScenarioTests.Drivers;
using Reqnroll;
using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.ScenarioTests.StepDefinitions
{
    [Binding]
    public class PaymentProcessingStepDefinitions(ScenarioDriver scenarioDriver)
    {
        [When(@"^(IpStandard|Camt53|PosteringsData) dokumenter er afstemt$")]
        public async Task WhenDocumentsMatched(DocumentType documentType)
        {
            await scenarioDriver.WhenDocumentsMatched(documentType);
        }
    }
}
