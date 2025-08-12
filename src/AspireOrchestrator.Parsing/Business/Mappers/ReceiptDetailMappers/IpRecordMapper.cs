using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Parsing.Business.Helpers;
using AspireOrchestrator.Parsing.Models.IPModels;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Parsing.Business.Mappers.ReceiptDetailMappers
{
    public class IpRecordMapper: GuidMapperBase<IpRecord, ReceiptDetail>
    {
        public IpRecordMapper(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            MapperConfiguration = GetMapperConfiguration(loggerFactory);
            Mapper = MapperConfiguration.CreateMapper();
        }

        private MapperConfiguration GetMapperConfiguration(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IpRecord, ReceiptDetail>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => ConversionHelper.GetDecimal100(src.Pensionsbidrag)))
                .ForMember(dest => dest.Cpr, opt => opt.MapFrom(src => ConversionHelper.CprHelper(src.Cpr)))
                .ForMember(dest => dest.Cvr, opt => opt.MapFrom(src => src.Cvr))
                .ForMember(dest => dest.PaymentReference, opt => opt.MapFrom(src => src.Cvr))
                .ForMember(dest => dest.ReceivedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ReceiptType, opt => opt.MapFrom(src => ReceiptType.Payment)) // ToDo
                .ForMember(dest => dest.LaborAgreementNumber, opt => opt.MapFrom(src => src.OverenskomstKode))
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.PeriodeStart)))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.PeriodeSlut)))
                .ForMember(dest => dest.PersonFullName, opt => opt.MapFrom(src => src.Navn))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.DatoForDannelse)))
                .ForMember(dest => dest.TotalContributionRate, opt => opt.MapFrom(src => ConversionHelper.GetDecimal100(src.SamletBidragsProcent)))
                .ForMember(dest => dest.ContributionRateFromDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.DatoForBidragsProcent)))
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.AfsendersKundenr))
                .ForMember(dest => dest.SubmissionDate, opt => opt.MapFrom(src => ConversionHelper.ParseDate(src.DatoForDannelse)))
                .ForMember(dest => dest.EmploymentTerminationDate, opt => opt.MapFrom(src => GetTerminationDate(src.DatoForFratraedelse)))
                , loggerFactory);
            return config;
        }

        private DateTime? GetTerminationDate(string dateString)
        {
            var terminationDate = ConversionHelper.ParseDate(dateString);
            if (!ConversionHelper.IsMinDateTime(terminationDate))
            {
                return null;
            }
            return terminationDate;
        }
    }
}
