using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic.Processors;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Parsing.WebApi.Controllers;
using AspireOrchestrator.Validation.WebApi.Controllers;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.ScenarioTests.Processors
{
    public class TestProcessorFactory(ParseController parseController, ValidationController validationController, ILoggerFactory loggerFactory): IProcessorFactory
    {
        public IProcessor? GetProcessor(EventEntity entity)
        {
            return entity.ProcessState switch
            {
                ProcessState.Receive => new ReceiveFileProcessor(loggerFactory),
                ProcessState.Parse => new TestParseProcessor(parseController),
                //ProcessState.Convert => new ConvertDocumentProcessor(loggerFactory),
                ProcessState.Validate => new TestValidationProcessor(validationController),
                ProcessState.ProcessPayment => new ProcessPaymentProcessor(loggerFactory),
                ProcessState.GenerateReceipt => new GenerateReceiptProcessor(loggerFactory),
                ProcessState.TransferResult => new TransferProcessor(loggerFactory),
                ProcessState.WorkFlowCompleted => null,
                _ => null
            };
        }
    }
}
