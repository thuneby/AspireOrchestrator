namespace AspireOrchestrator.Core.OrchestratorModels
{
    public enum ProcessState
    {
        Receive = 10,
        Parse = 20,
        Convert = 30,
        Validate = 40,
        Process = 50, 
        GenerateReceipt = 60,
        TransferResult = 90,
        WorkFlowCompleted = 999
    }
}
