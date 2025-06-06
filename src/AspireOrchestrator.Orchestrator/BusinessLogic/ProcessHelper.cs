﻿using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public static class ProcessHelper
    {
        public static DocumentType GetDocumentType(EventType eventType)
        {
            return eventType switch
            {
                EventType.HandleReceipt => DocumentType.Pdf,
                _ => throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null)
            };
        }
    }
}
