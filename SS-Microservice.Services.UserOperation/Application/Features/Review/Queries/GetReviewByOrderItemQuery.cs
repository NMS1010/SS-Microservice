using MediatR;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class GetReviewByOrderItemQuery : IRequest<ReviewDto>
    {
        public long OrderItemId { get; set; }
        public string UserId { get; set; }
    }

    public class GetReviewByOrderItemQueryHandler : IRequestHandler<GetReviewByOrderItemQuery, ReviewDto>
    {
        private readonly IReviewService _reviewService;

        public GetReviewByOrderItemQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<ReviewDto> Handle(GetReviewByOrderItemQuery request, CancellationToken cancellationToken)
        {
            return await _reviewService.GetReviewByOrderItem(request);
        }
    }
}
