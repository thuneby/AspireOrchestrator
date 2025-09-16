using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Parsing.Models;
using FileHelpers;

namespace AspireOrchestrator.Parsing.Business.Helpers
{
    public abstract class FlatParserHelperBase<T>: ParserHelperBase
        where T : TextModelBase
    {
        public (IEnumerable<T>, IEnumerable<string>) GetRecordsFromPayload(Stream payload, DocumentType documentType)
        {
            var errors = new HashSet<string>();
            var engine = new FileHelperEngine<T>
            {
                ErrorManager =
                {
                    ErrorMode = ErrorMode.SaveAndContinue
                }
            };
            var records = engine.ReadStream(new StreamReader(payload, GetEncoding(documentType)));
            if (engine.ErrorManager.HasErrors)
                foreach (var error in engine.ErrorManager.Errors)
                {
                    errors.Add("Error in line: " + error.LineNumber + " - " + error.ExceptionInfo.Message);
                    //errors.Add(error.ExceptionInfo.Message);
                }
            var errorList = errors.ToList();
            return (records, errorList);
        }

    }
}
