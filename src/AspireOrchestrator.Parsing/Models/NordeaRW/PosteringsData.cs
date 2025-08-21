using FileHelpers;

namespace AspireOrchestrator.Parsing.Models.NordeaRW
{
    [DelimitedRecord(",")]
    public class PosteringsData: TextModelBase
    {
        [FieldQuoted]
        public string SwiftAdresse;
        [FieldQuoted]
        public string Regnr;
        [FieldQuoted]
        public string Kontonr;
        [FieldQuoted]
        public string Valutakode;
        [FieldQuoted]
        public string Kundenavn;
        [FieldQuoted]
        public string Udskriftsnummer;
        [FieldQuoted]
        public string Bogforingsdato;
        [FieldQuoted]
        public string Rentedato;
        [FieldQuoted]
        public string Belob;
        [FieldQuoted]
        public string Fortegn;              // Field 10
        [FieldQuoted]
        public string NumeriskBelob;
        [FieldQuoted]
        public string SwiftTekstkode;
        [FieldQuoted]
        public string PosteringsTypeKode;
        [FieldQuoted]
        public string PosteringsTypeTekst;
        [FieldQuoted]
        public string Reference;
        [FieldQuoted]
        public string ReferenceAntal;
        [FieldQuoted]
        public string ReferenceKode1;
        [FieldQuoted]
        public string ReferenceTekst1;
        [FieldQuoted]
        public string ReferenceKode2;
        [FieldQuoted]
        public string ReferenceTekst2;      // Field 20
        [FieldQuoted]
        public string ReferenceKode3;
        [FieldQuoted]
        public string ReferenceTekst3;
        [FieldQuoted]
        public string ReferenceKode4;
        [FieldQuoted]
        public string ReferenceTekst4;
        [FieldQuoted]
        public string ReferenceKode5;
        [FieldQuoted]
        public string ReferenceTekst5;
        [FieldQuoted]
        public string ReferenceKode6;
        [FieldQuoted]
        public string ReferenceTekst6;
        [FieldQuoted]
        public string AntalAdvisLinier;
        [FieldQuoted]
        public string AdvisLinie1;          // Field 30
        [FieldQuoted]
        public string AdvisLinie2;
        [FieldQuoted]
        public string AdvisLinie3;
        [FieldQuoted]
        public string AdvisLinie4;
        [FieldQuoted]
        public string AdvisLinie5;
        [FieldQuoted]
        public string AdvisLinie6;
        [FieldQuoted]
        public string Saldo;
        [FieldQuoted]
        public string SaldoFortegn;
        [FieldQuoted]
        public string NumeriskSaldo;
        [FieldQuoted]
        public string Blank39;
        [FieldQuoted]
        public string Blank40;              // Field 40
        [FieldQuoted]
        public string KontoNavn;
        [FieldQuoted]
        public string Iban;
        [FieldQuoted]
        public string TilbageForsel;
        [FieldQuoted]
        public string Indbetaler1;
        [FieldQuoted]
        public string Indbetaler2;
        [FieldQuoted]
        public string Indbetaler3;
        [FieldQuoted]
        public string Indbetaler4;
        [FieldQuoted]
        public string Indbetaler5;
        [FieldQuoted]
        public string DebitorIdentifikation;
        [FieldQuoted]
        public string RefPrimDok;           // Field 50
        [FieldQuoted]
        public string Meddelelsesnr;
        [FieldQuoted]
        public string Arkivoplysning;
        [FieldQuoted]
        public string AntalMeddelelser;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse01;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse02;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse03;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse04;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse05;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse06;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse07;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse08;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse09;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse10;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse11;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse12;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse13;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse14;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse15;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse16;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse17;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse18;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse19;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse20;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse21;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse22;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse23;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse24;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse25;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse26;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse27;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse28;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse29;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse30;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse31;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse32;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse33;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse34;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse35;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse36;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse37;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse38;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse39;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse40;
        [FieldQuoted]
        [FieldOptional]
        public string Meddelelse41;

    }
}
