using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Core.OrchestratorModels
{
    public class EventEntity : GuidModelBase
    {
        public EventEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public void UpdateProcessResult(EventState state = EventState.Completed)
        {
            EventState = state;
            if (state == EventState.Completed)
            {
                if (!string.IsNullOrWhiteSpace(ErrorMessage))
                    ErrorMessage = "Last known error: " + ErrorMessage;
            }
            EndTime = DateTime.UtcNow;
        }

        public void StartEvent()
        {
            EventState = EventState.Processing;
            StartTime = DateTime.UtcNow;
            ExecutionCount++;
        }

        public void AssignResult(EventEntity result)
        {
            EventState = result.EventState;
            Result = result.Result;
            ErrorMessage = result.ErrorMessage;
        }

        public long FlowId { get; set; }

        public EventType EventType { get; set; }
        public EventState EventState { get; set; } = EventState.New;
        public ProcessState ProcessState { get; set; }
        public DocumentType DocumentType { get; set; }
        public string Parameters { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        private short ExecutionCount { get; set; }
        public short Priority { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }


        //[ForeignKey("FlowId")]
        //[System.Text.Json.Serialization.JsonIgnore]
        //public virtual Flow Flow { get; set; } 
    }
}
