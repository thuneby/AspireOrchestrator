using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Parsing.Business.Helpers;
using AspireOrchestrator.Parsing.Business.Mappers;
using AspireOrchestrator.Parsing.Business.Mappers.TestMappers;
using AspireOrchestrator.Parsing.Interfaces;
using AspireOrchestrator.Parsing.Models.IPModels;
using AspireOrchestrator.Parsing.Models.IPRW;

namespace AspireOrchestrator.Parsing.Business
{
    public class IpStandardParser(FlatParserHelperBase<IpStandard> flatParserHelper, TextMapperBase<IpStandard, IpRecord> textMapper, GuidMapperBase<IpRecord, ReceiptDetail> recordMapper) 
        : SimpleParser<IpStandard, IpRecord, ReceiptDetail>(flatParserHelper, textMapper, recordMapper), IAsyncParser
    {
    }
}
