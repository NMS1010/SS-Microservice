using AutoMapper;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Commands;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Queries;
using SS_Microservice.Services.UserOperation.Application.Models.Review;
using SS_Microservice.Services.UserOperation.Domain.Entities;

namespace SS_Microservice.Services.UserOperation.Application.Common.AutoMapper
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Domain.Entities.Review, ReviewDto>();
            CreateMap<CreateReviewCommand, Review>();
            CreateMap<UpdateReviewCommand, Review>();
            // mapping request - command
            CreateMap<CreateReviewRequest, CreateReviewCommand>();
            CreateMap<UpdateReviewRequest, UpdateReviewCommand>();
            CreateMap<ReplyReviewRequest, ReplyReviewCommand>();
            CreateMap<GetReviewPagingRequest, GetListReviewQuery>();
        }
    }
}
