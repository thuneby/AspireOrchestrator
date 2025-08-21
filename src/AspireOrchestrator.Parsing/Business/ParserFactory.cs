using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Parsing.Business.Helpers;
using AspireOrchestrator.Parsing.Business.Mappers.ReceiptDetailMappers;
using AspireOrchestrator.Parsing.Business.Mappers.TestMappers;
using AspireOrchestrator.Parsing.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Parsing.Business
{
    public static class ParserFactory
    {
        public static IAsyncParser GetReceiptDetailParser(DocumentType documentType, ILoggerFactory loggerFactory)
        {
            switch (documentType)
            {
                case DocumentType.ReceiptDetailJson:
                    return new JsonParser();
                case DocumentType.IpStandard:
                    return new IpStandardParser(new IpStandardParserHelper(), new IpStandardMapper(loggerFactory), new IpRecordMapper(loggerFactory));
                //case DocumentType.NetsIs:
                //    return new NetsIsParser();
                default:
                    throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null);
            }
        }
    }
}
