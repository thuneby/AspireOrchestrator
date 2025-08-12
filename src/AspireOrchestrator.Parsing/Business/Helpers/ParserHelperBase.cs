using System.Text;
using AspireOrchestrator.Core.OrchestratorModels;

namespace AspireOrchestrator.Parsing.Business.Helpers
{
    public abstract class ParserHelperBase
    {
        public static Encoding GetEncoding(DocumentType documentType)
        {
            return documentType switch
            {
                DocumentType.NetsIs => Encoding.Default, // 28591?
                _ => Encoding.UTF8
            };
        }
    }
}
