namespace AspireOrchestrator.Core.OrchestratorModels
{
    public enum ProcessState
    {
        Receive = 10,
        Parse = 20,
        //Convert = 30,
        Validate = 40,
        ProcessPayment = 50, 
        GenerateReceipt = 60,
        TransferResult = 90,
        GeneratePostings = 180,
        WorkFlowCompleted = 999
    }
}
