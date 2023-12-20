using MediatR;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Commands
{
    public class ToggleReviewCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class ToggleReviewHandler : IRequestHandler<ToggleReviewCommand, bool>
    {
        private readonly IReviewService _reviewService;
        private readonly ISender _sender;

        public ToggleReviewHandler(IReviewService reviewService, MediatR.ISender sender)
        {
            _reviewService = reviewService;
            _sender = sender;
        }

        public async Task<bool> Handle(ToggleReviewCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _reviewService.ToggleReview(request);

            if (isSuccess)
            {
                var productReview = await _reviewService.GetProductReview(request.Id);
                await _sender.Send(new UpdateProductRatingCommand()
                {
                    ProductRatings = new List<ProductRating>()
                    {
                        new()
                        {
                            ProductId = productReview.ProductId,
                            Rating = productReview.Rating
                        }
                    }
                });
            }

            return isSuccess;
        }
    }
}
