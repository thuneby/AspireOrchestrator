using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Parsing.Models.IPModels
{
    public class IpExtendedRecord : IpRecordBase
    {
        [StringLength(8)]
        public string DatoForAfvigelse { get; set; } = "";

        [StringLength(2)]
        public string AfvigelsesKode { get; set; } = "";

        [StringLength(12)]
        public string PensionsgivendeLoen { get; set; } = "";
    }
}
