using AspireOrchestrator.Parsing.Models.Nordea;
using AspireOrchestrator.Parsing.Models.NordeaRW;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Parsing.Business.Mappers.TestMappers
{
    public class PosteringsDataMapper: TextMapperBase<PosteringsData, PosteringRecord>
    {
        public PosteringsDataMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }
    }
}
