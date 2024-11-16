﻿using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public static class StateMap
    {
        public static ProcessState GetNextStep(EventEntity entity)
        {
            var currentStep = entity.ProcessState;
            switch (entity.EventType)
            {
                case EventType.HandlePdf:
                {
                    return currentStep switch
                    {
                        ProcessState.Receive => ProcessState.Parse,
                        ProcessState.Parse => ProcessState.Convert,
                        ProcessState.Convert => ProcessState.Validate,
                        ProcessState.Validate => ProcessState.Process,
                        ProcessState.Process => ProcessState.GenerateReceipt,
                        ProcessState.GenerateReceipt => ProcessState.TransferResult,
                        ProcessState.TransferResult => ProcessState.WorkFlowCompleted,
                        ProcessState.WorkFlowCompleted => ProcessState.WorkFlowCompleted,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
