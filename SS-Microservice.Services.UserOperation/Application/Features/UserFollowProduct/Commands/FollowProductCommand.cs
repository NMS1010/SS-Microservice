using MediatR;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;

namespace SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Commands
{
    public class FollowProductCommand : FollowProductRequest, IRequest<bool>
    {
    }

    public class FollowProductHandler : IRequestHandler<FollowProductCommand, bool>
    {
        private readonly IUserFollowProductService _followProductService;

        public FollowProductHandler(IUserFollowProductService followProductService)
        {
            _followProductService = followProductService;
        }

        public async Task<bool> Handle(FollowProductCommand request, CancellationToken cancellationToken)
        {
            return await _followProductService.FollowProduct(request);
        }
    }
}
