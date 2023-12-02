using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class GetListReviewQuery : GetReviewPagingRequest, IRequest<PaginatedResult<ReviewDto>>
    {
    }

    public class GetListReviewHandler : IRequestHandler<GetListReviewQuery, PaginatedResult<ReviewDto>>
    {
        private readonly IReviewService _reviewService;

        public GetListReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<PaginatedResult<ReviewDto>> Handle(GetListReviewQuery request, CancellationToken cancellationToken)
        {
            return await _reviewService.GetListReview(request);
        }
    }
}
