using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.Interfaces;
using AspireOrchestrator.Parsing.Business.Helpers;
using AspireOrchestrator.Parsing.Business.Mappers;
using AspireOrchestrator.Parsing.Business.Mappers.TestMappers;
using AspireOrchestrator.Parsing.Models;

namespace AspireOrchestrator.Parsing.Business
{
    public abstract class SimpleParser<T1, T2, T3>(FlatParserHelperBase<T1> flatParserHelper, 
            TextMapperBase<T1, T2> textMapper,
            GuidMapperBase<T2, T3> recordMapper)
        where T1 : TextModelBase
        where T2 : GuidModelBase
        where T3 : GuidModelBase, ITransactionDocument
    {

        public async Task<IEnumerable<T3>> ParseAsync(Stream payload, DocumentType documentType)
        {
            var (textRecords, errors) = flatParserHelper.GetRecordsFromPayload(payload, documentType);
            var recordList = textRecords.ToList();
            var errorList = errors.ToList();
            if (errorList.Count > 0)
            {
                var exception = new Exception(errors.FirstOrDefault());
                throw exception;
            }
            if (recordList.Count == 0)
            {
                var exception = new Exception("Fejl - Formatet er ikke IP Standardformat!");
                throw exception;
            }

            var modelRecords = recordList.Select(textMapper.Map).ToList();
            var result = modelRecords.Select(recordMapper.Map).ToList();
            return result;
        }
    }
}
