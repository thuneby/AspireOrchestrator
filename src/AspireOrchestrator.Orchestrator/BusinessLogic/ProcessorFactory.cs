using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic.Processors;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public class ProcessorFactory(ILoggerFactory loggerFactory)
    {
        public IProcessor? GetProcessor(EventEntity entity)
        {
            return entity.ProcessState switch
            {
                ProcessState.Receive => new ReceiveFileProcessor(loggerFactory),
                ProcessState.Parse => new ParseFileProcessor(loggerFactory),
                ProcessState.Convert => new ConvertDocumentProcessor(loggerFactory),
                ProcessState.Validate => new ValidationProcessor(loggerFactory),
                ProcessState.ProcessPayment => new ProcessPaymentProcessor(loggerFactory),
                ProcessState.GenerateReceipt => new GenerateReceiptProcessor(loggerFactory),
                ProcessState.TransferResult => new TransferProcessor(loggerFactory),
                ProcessState.WorkFlowCompleted => null,
                _ => null
            };
        }
    }
}
