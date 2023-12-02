using MediatR;
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

        public UpdateReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<bool> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            return await _reviewService.UpdateReview(request);
        }
    }
}
