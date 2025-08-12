using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Core.OrchestratorModels 
{
    public enum DocumentType
    {
        [Display(Name = "Manuel")]
        ManualEntry = 0,
        [Display(Name = "Nets IS")]
        NetsIs = 1,
        [Display(Name = "Excel")]
        Excel = 3,
        [Display(Name = "Opkrævning")]
        Invoice = 4,
        [Display(Name = "Nets BS-602")]
        NetsBs602 = 5,
        [Display(Name = "Pensionsoverførsel §41")]
        Pa41 = 6,
        [Display(Name = "Camt.053 Kontoudtog")]
        Camt53 = 8,
        [Display(Name = "Camt.054 Indbetalingsadvisering")]
        Camt54 = 9,
        [Display(Name = "IP-Standard")]
        IpStandard = 10,
        [Display(Name = "IP-Udvidet")]
        IpUdvidet = 11,
        [Display(Name = "Nordea Standardformat")]
        NordeaStandard = 12,
        [Display(Name = "Nets OS")]
        NetsOs = 13,
        [Display(Name = "Excel")]
        ExcelLine = 16,
        [Display(Name = "Posteringsdata")]
        PosteringsData = 17,
        [Display(Name = "E-mail")]
        Email = 18,
        [Display(Name = "Nets OS Stamkort")]
        NetsMasterData = 19,
        [Display(Name = "Nordea virksomhedsdata")]
        CompanyData = 20,
        [Display(Name = "JSON")]
        ReceiptDetailJson = 21,
        [Display(Name = "Ukendt")]
        Unknown = 99,
    }
}
