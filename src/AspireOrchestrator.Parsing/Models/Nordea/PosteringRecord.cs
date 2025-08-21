using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.Parsing.Models.Nordea
{
    public class PosteringRecord: GuidModelBase
    {
        public string SwiftAdresse { get; set; }
        public string Regnr { get; set; }
        public string Kontonr { get; set; }
        public string Valutakode { get; set; }
        public string Kundenavn { get; set; }
        public string Udskriftsnummer { get; set; }
        public string Bogforingsdato { get; set; }
        public string Rentedato { get; set; }
        public string Belob { get; set; }
        public string Fortegn { get; set; }              // Field 10
        public string NumeriskBelob { get; set; }
        public string SwiftTekstkode { get; set; }
        public string PosteringsTypeKode { get; set; }
        public string PosteringsTypeTekst { get; set; }
        public string Reference { get; set; }
        public string ReferenceAntal { get; set; }
        public string ReferenceKode1 { get; set; }
        public string ReferenceTekst1 { get; set; }
        public string ReferenceKode2 { get; set; }
        public string ReferenceTekst2 { get; set; }      // Field 20
        public string ReferenceKode3 { get; set; }
        public string ReferenceTekst3 { get; set; }
        public string ReferenceKode4 { get; set; }
        public string ReferenceTekst4 { get; set; }
        public string ReferenceKode5 { get; set; }
        public string ReferenceTekst5 { get; set; }
        public string ReferenceKode6 { get; set; }
        public string ReferenceTekst6 { get; set; }
        public string AntalAdvisLinier { get; set; }
        public string AdvisLinie1 { get; set; }          // Field 30
        public string AdvisLinie2 { get; set; }
        public string AdvisLinie3 { get; set; }
        public string AdvisLinie4 { get; set; }
        public string AdvisLinie5 { get; set; }
        public string AdvisLinie6 { get; set; }
        public string Saldo { get; set; }
        public string SaldoFortegn { get; set; }
        public string NumeriskSaldo { get; set; }
        public string Blank39 { get; set; }
        public string Blank40 { get; set; }              // Field 40
        public string KontoNavn { get; set; }
        public string Iban { get; set; }
        public string TilbageForsel { get; set; }
        public string Indbetaler1 { get; set; }
        public string Indbetaler2 { get; set; }
        public string Indbetaler3 { get; set; }
        public string Indbetaler4 { get; set; }
        public string Indbetaler5 { get; set; }
        public string DebitorIdentifikation { get; set; }
        public string RefPrimDok { get; set; }           // Field 50
        public string Meddelelsesnr { get; set; }
        public string Arkivoplysning { get; set; }
        public string AntalMeddelelser { get; set; }
        public string Meddelelse01 { get; set; }
    }
}
