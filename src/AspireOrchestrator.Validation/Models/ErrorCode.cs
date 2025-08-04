namespace AspireOrchestrator.Validation.Models
{
    public enum ErrorCode
    {
        InvalidCvr = 10,
        InvalidCpr = 20,
        CprModulusError = 25,
        UnknownLaborAgreement = 30,
        InvalidPeriod = 40,
        InvalidDate = 45,
        UnknownCompany = 50,
        UnknownAgreement = 60,
        UnknownPerson = 70
    }
}
