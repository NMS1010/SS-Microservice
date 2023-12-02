using MediatR;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class GetReviewQuery : IRequest<ReviewDto>
    {
        public long Id { get; set; }
    }

    public class GetReviewHandler : IRequestHandler<GetReviewQuery, ReviewDto>
    {
        private readonly IReviewService _reviewService;

        public GetReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<ReviewDto> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {
            return await _reviewService.GetReview(request);
        }
    }
}
