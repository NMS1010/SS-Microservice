using MediatR;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class CountReviewQuery : IRequest<List<long>>
    {
        public long ProductId { get; set; }
    }

    public class CountReviewHandler : IRequestHandler<CountReviewQuery, List<long>>
    {
        private readonly IReviewService _reviewService;

        public CountReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<List<long>> Handle(CountReviewQuery request, CancellationToken cancellationToken)
        {
            return await _reviewService.CountReview(request);
        }
    }
}
