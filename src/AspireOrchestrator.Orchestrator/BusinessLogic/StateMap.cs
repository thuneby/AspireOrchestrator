using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public static class StateMap
    {
        public static ProcessState GetNextStep(EventEntity entity)
        {
            var currentStep = entity.ProcessState;
            switch (entity.EventType)
            {
                case EventType.HandleReceipt:
                {
                    return currentStep switch
                    {
                        ProcessState.Receive => ProcessState.Parse,
                        ProcessState.Parse => ProcessState.Validate,
                        //ProcessState.Convert => ProcessState.Validate,
                        ProcessState.Validate => ProcessState.ProcessPayment,
                        ProcessState.ProcessPayment => ProcessState.TransferResult,
                        ProcessState.TransferResult => ProcessState.WorkFlowCompleted,
                        ProcessState.WorkFlowCompleted => ProcessState.WorkFlowCompleted,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                case EventType.HandleDeposit:
                    return currentStep switch
                    {
                        ProcessState.Receive => ProcessState.Parse,
                        ProcessState.Parse => ProcessState.ProcessPayment,
                        //ProcessState.Convert => ProcessState.ProcessPayment,
                        ProcessState.ProcessPayment => ProcessState.TransferResult,
                        ProcessState.TransferResult => ProcessState.WorkFlowCompleted,
                        ProcessState.WorkFlowCompleted => ProcessState.WorkFlowCompleted,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                case EventType.ValidateReceipt:
                    return currentStep switch
                    {
                        ProcessState.Validate => ProcessState.ProcessPayment,
                        ProcessState.ProcessPayment => ProcessState.TransferResult,
                        ProcessState.TransferResult => ProcessState.WorkFlowCompleted,
                        ProcessState.WorkFlowCompleted => ProcessState.WorkFlowCompleted,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
