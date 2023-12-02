using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Queries
{
    public class GetListFollowProductQuery : GetFollowProductPagingRequest, IRequest<PaginatedResult<ProductDto>>
    {
    }

    public class GetListFollowProductHandler : IRequestHandler<GetListFollowProductQuery, PaginatedResult<ProductDto>>
    {
        private readonly IUserFollowProductService _userFollowProductService;

        public GetListFollowProductHandler(IUserFollowProductService userFollowProductService)
        {
            _userFollowProductService = userFollowProductService;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetListFollowProductQuery request, CancellationToken cancellationToken)
        {
            return await _userFollowProductService.GetListFollowProduct(request);
        }
    }
}
