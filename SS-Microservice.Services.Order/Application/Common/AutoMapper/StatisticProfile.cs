using AutoMapper;
using SS_Microservice.Services.Order.Application.Features.Statistic.Queries;
using SS_Microservice.Services.Order.Application.Models.Statistic;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class StatisticProfile : Profile
    {
        public StatisticProfile()
        {
            CreateMap<GetStatisticOrderStatusRequest, GetStatisticOrderStatusQuery>();
            CreateMap<GetStatisticRevenueRequest, GetStatisticRevenueQuery>();
            CreateMap<GetStatisticTopSellingProductYearRequest, GetStatisticTopSellingProductYearQuery>();
            CreateMap<GetStatisticTopSellingProductRequest, GetStatisticTopSellingProductQuery>();
        }
    }
}
