using AspireOrchestrator.Core.Mappers;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Parsing.Business.Helpers;
using AspireOrchestrator.Parsing.Models.Nordea;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Parsing.Business.Mappers.DepositMappers
{
    public class PosteringsRecordMapper: GuidMapperBase<PosteringRecord, Deposit>
    {
        public PosteringsRecordMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            MapperConfiguration = GetMapperConfiguration(loggerFactory);
            Mapper = MapperConfiguration.CreateMapper();
        }

        private MapperConfiguration GetMapperConfiguration(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PosteringRecord, Deposit>()
                    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(dest => dest.ReconcileStatus, opt => opt.MapFrom(src => ReconcileStatus.Paid))
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => ConversionHelper.GetDecimalUs(src.Belob, "")))
                    .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Valutakode))
                    .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => ConversionHelper.Concatenate(src.Regnr, src.Kontonr)))
                    .ForMember(dest => dest.AccountReference, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Indbetaler1) ? src.Indbetaler1.Trim() : src.ReferenceTekst4.Trim()))
                    .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.ReferenceTekst1))
                    .ForMember(dest => dest.TrxDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.Bogforingsdato)))
                    .ForMember(dest => dest.ValDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.Rentedato)))
                    .ForMember(dest => dest.PaymentReference, opt => opt.MapFrom(src => src.ReferenceTekst1))
                , loggerFactory);
            return config;
        }
    }
}
