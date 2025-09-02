using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Transfer.Models
{
    public class BackendReply: GuidModelBase
    {
        public Guid TransferId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
