using MediatR;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Commands
{
    public class UnFollowProductCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }

    public class UnFollowProductHandler : IRequestHandler<UnFollowProductCommand, bool>
    {
        private readonly IUserFollowProductService _followProductService;

        public UnFollowProductHandler(IUserFollowProductService followProductService)
        {
            _followProductService = followProductService;
        }

        public async Task<bool> Handle(UnFollowProductCommand request, CancellationToken cancellationToken)
        {
            return await _followProductService.UnFollowProduct(request);
        }
    }
}
