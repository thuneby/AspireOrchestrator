using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public static class ProcessHelper
    {
        public static EventType GetEventType(DocumentType documentType)
        {
            return documentType switch
            {
                DocumentType.ManualEntry => EventType.HandleReceipt,
                DocumentType.IpStandard => EventType.HandleReceipt,
                DocumentType.IpUdvidet => EventType.HandleReceipt,
                DocumentType.NordeaStandard => EventType.HandleReceipt,
                DocumentType.NetsIs => EventType.HandleReceipt,
                DocumentType.NetsOs => EventType.HandleReceipt,
                DocumentType.NetsBs602 => EventType.HandleInvoice,
                DocumentType.Excel => EventType.HandleReceipt,
                DocumentType.ExcelLine => EventType.HandleReceipt,
                DocumentType.Invoice => EventType.HandleInvoice,
                DocumentType.Pa41 => EventType.HandleReceipt,
                DocumentType.Camt53 => EventType.HandleDeposit,
                DocumentType.Camt54 => EventType.HandleDeposit,
                DocumentType.PosteringsData => EventType.HandleDeposit,
                DocumentType.ReceiptDetailJson => EventType.HandleReceipt,
                _ => throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null)
            };
        }
    }
}
