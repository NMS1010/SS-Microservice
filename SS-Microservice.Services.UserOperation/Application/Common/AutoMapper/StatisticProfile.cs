using AutoMapper;
using SS_Microservice.Services.UserOperation.Application.Features.Statistic.Queries;
using SS_Microservice.Services.UserOperation.Application.Models.Statistic;

namespace SS_Microservice.Services.UserOperation.Application.Common.AutoMapper
{
    public class StatisticProfile : Profile
    {
        public StatisticProfile()
        {
            CreateMap<GetStatisticReviewRequest, GetStatisticReviewQuery>();
        }
    }
}
