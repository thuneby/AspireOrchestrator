namespace AspireOrchestrator.Core.OrchestratorModels
{
    public enum EventType
    {
        HandleReceipt = 2,
        HandleDeposit = 3, 
        HandleInvoice = 4,
        ValidateReceipt = 5,
        MatchAndTransfer = 6, 
        GenerateTransfers = 8

    }
}
