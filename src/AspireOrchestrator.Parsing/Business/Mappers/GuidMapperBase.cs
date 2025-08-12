using AspireOrchestrator.Core.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Parsing.Business.Mappers
{
    public abstract class GuidMapperBase<T1, T2>
        where T1 : GuidModelBase
        where T2 : GuidModelBase
    {
        protected MapperConfiguration MapperConfiguration;
        protected IMapper Mapper;

        protected GuidMapperBase(ILoggerFactory loggerFactory)
        {
            MapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>(), loggerFactory);
            Mapper = MapperConfiguration.CreateMapper();
        }

        public T2 GetRecord(T1 record)
        {
            var result = Mapper.Map<T1, T2>(record);
            return result;
        }

        public T2 Map(T1 record)
        {
            var result = Mapper.Map<T1, T2>(record);
            return result;
        }

    }
}
