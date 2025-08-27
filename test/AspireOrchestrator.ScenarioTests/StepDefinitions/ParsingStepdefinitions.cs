using AspireOrchestrator.ScenarioTests.Drivers;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.Models;
using Reqnroll;

namespace AspireOrchestrator.ScenarioTests.StepDefinitions
{
    [Binding]
    public class ParsingStepdefinitions(ScenarioDriver scenarioDriver)
    {
        [Given(@"^(ReceiptDetail|Deposit) tabel indeholder$")]
        public async Task GivenTheFollowingTableContains(string tableName, Table reqTable)
        {
            switch (tableName)
            {
                case "ReceiptDetail":
                    await scenarioDriver.GivenTable<ReceiptDetail>(reqTable);
                    break;
                case "Deposit":
                    await scenarioDriver.GivenTable<Deposit>(reqTable);
                    break;
                default:
                    throw new ArgumentException($"Unknown table name: {tableName}");
            }
        }

        [When(@"^(IpStandard|Camt53|PosteringsData) fil ""(.*)"" er i storage$")]
        public async Task WhenFileUploadedToStorage(DocumentType fileType, string fileName)
        {
            await scenarioDriver.WhenFileUploadedToStorage(fileType, fileName);
        }

        [When("filen er parset")]
        public async Task WhenFileParsed()
        {
            await scenarioDriver.WhenFileParsed();
        }


        [Then(@"^(ReceiptDetail|Deposit) tabel indeholder$")]
        public void ThenTheFollowingTableContains(string tableName, Table reqTable)
        {
            switch (tableName)
            {
                case "ReceiptDetail":
                    scenarioDriver.ThenTableContains<ReceiptDetail>(reqTable);
                    break;
                case "Deposit":
                    scenarioDriver.ThenTableContains<Deposit>(reqTable);
                    break;
                default:
                    throw new ArgumentException($"Unknown table name: {tableName}");
            }
        }

    }
}
