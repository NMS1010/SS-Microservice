using MediatR;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class GetOrderReviewQuery : GetOrderReviewRequest, IRequest<OrderReviewDto>
    {
    }
    public class GetOrderReviewHandler : IRequestHandler<GetOrderReviewQuery, OrderReviewDto>
    {
        private readonly IReviewService _reviewService;

        public GetOrderReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<OrderReviewDto> Handle(GetOrderReviewQuery request, CancellationToken cancellationToken)
        {
            return await _reviewService.GetOrderReview(request);
        }
    }
}
