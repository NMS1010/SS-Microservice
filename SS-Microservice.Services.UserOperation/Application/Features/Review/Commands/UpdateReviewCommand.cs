using MediatR;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Commands
{
    public class UpdateReviewCommand : UpdateReviewRequest, IRequest<bool>
    {
    }

    public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, bool>
    {
        private readonly IReviewService _reviewService;
        private readonly ISender _sender;

        public UpdateReviewHandler(IReviewService reviewService, ISender sender)
        {
            _reviewService = reviewService;
            _sender = sender;
        }

        public async Task<bool> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _reviewService.UpdateReview(request);
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
