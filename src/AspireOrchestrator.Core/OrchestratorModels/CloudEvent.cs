using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspireOrchestrator.Core.OrchestratorModels
{
    public class CloudEvent<T>
    {
        public string SpecVersion { get; set; } = "1.0";
        public string Type { get; set; } = "";
        public string Source { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Time = DateTime.UtcNow;
        public string DataContentType { get; set; } = "application/json";
        public T Data { get; set; } 
    }
}
