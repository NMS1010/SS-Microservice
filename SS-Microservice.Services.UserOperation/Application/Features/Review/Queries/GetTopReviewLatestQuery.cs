using MediatR;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class GetTopReviewLatestQuery : IRequest<List<ReviewDto>>
    {
        public int Top { get; set; } = 5;
    }

    public class GetTopReviewLatestQueryHandler : IRequestHandler<GetTopReviewLatestQuery, List<ReviewDto>>
    {
        private readonly IReviewService _reviewService;

        public GetTopReviewLatestQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<List<ReviewDto>> Handle(GetTopReviewLatestQuery request, CancellationToken cancellationToken)
        {
            return await _reviewService.GetTopReviewLatest(request);
        }
    }
}
