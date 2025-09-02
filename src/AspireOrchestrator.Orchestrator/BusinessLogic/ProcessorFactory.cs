using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic.Processors;
using AspireOrchestrator.Orchestrator.Interfaces;
using AspireOrchestrator.Orchestrator.Services;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public class ProcessorFactory(ParseService parseService, ValidateService validateService, PaymentProcessingService paymentService, TransferService transferService, ILoggerFactory loggerFactory): IProcessorFactory
    {
        public IProcessor? GetProcessor(EventEntity entity)
        {
            return entity.ProcessState switch
            {
                ProcessState.Receive => new ReceiveFileProcessor(loggerFactory),
                ProcessState.Parse => new ParseFileProcessor(parseService, loggerFactory),
                ProcessState.Validate => new ValidationProcessor(validateService, loggerFactory),
                ProcessState.ProcessPayment => new ProcessPaymentProcessor(paymentService, loggerFactory),
                ProcessState.GenerateReceipt => new GenerateReceiptProcessor(loggerFactory),
                ProcessState.TransferResult => new TransferProcessor(transferService, loggerFactory),
                ProcessState.WorkFlowCompleted => null,
                _ => null
            };
        }
    }
}
