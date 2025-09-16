using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Parsing.Business.Helpers;
using AspireOrchestrator.Parsing.Business.Mappers.DepositMappers;
using AspireOrchestrator.Parsing.Business.Mappers.TestMappers;
using AspireOrchestrator.Parsing.Interfaces;
using AspireOrchestrator.Parsing.Models.Nordea;
using AspireOrchestrator.Parsing.Models.NordeaRW;

namespace AspireOrchestrator.Parsing.Business
{
    public class PosteringsDataParser(PosteringsDataParserHelper flatParserHelper, PosteringsDataMapper textMapper, PosteringsRecordMapper recordMapper) : 
        SimpleParser<PosteringsData, PosteringRecord, Deposit>(flatParserHelper, textMapper, recordMapper), IDepositParser
    {
    }

}
