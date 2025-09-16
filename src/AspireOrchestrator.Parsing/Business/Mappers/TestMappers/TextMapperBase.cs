using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Parsing.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Parsing.Business.Mappers.TestMappers
{
    public class TextMapperBase<T1, T2>
        where T1 : TextModelBase
        where T2 : GuidModelBase
    {
        private readonly IMapper _mapper;

        protected TextMapperBase(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>(), loggerFactory);
            _mapper = config.CreateMapper();
        }
        
        public T2 Map(T1 record)
        {
            var result = _mapper.Map<T1, T2>(record);
            return result;
        }

        public T2 GetRecord(object baseRecord)
        {
            var textRecord = (T1) baseRecord;
            var result = Map(textRecord);
            return result;
        }
    }
}
