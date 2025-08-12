using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Parsing.Models.IPModels
{
    public class IpRecord : IpRecordBase
    {
        [StringLength(8)]
        public string DatoForFratraedelse { get; set; } = "";
    }
}
