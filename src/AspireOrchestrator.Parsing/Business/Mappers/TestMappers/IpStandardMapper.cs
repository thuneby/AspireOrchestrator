using AspireOrchestrator.Parsing.Models.IPModels;
using AspireOrchestrator.Parsing.Models.IPRW;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Parsing.Business.Mappers.TestMappers
{
    public class IpStandardMapper : TextMapperBase<IpStandard, IpRecord>
    {
        public IpStandardMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }
    }
}
