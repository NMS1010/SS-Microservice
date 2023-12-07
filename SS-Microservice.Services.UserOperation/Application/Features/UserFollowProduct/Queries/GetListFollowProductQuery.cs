using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Queries
{
    public class GetListFollowProductQuery : GetFollowProductPagingRequest, IRequest<PaginatedResult<ProductDto>>
    {
    }

    public class GetListFollowProductHandler : IRequestHandler<GetListFollowProductQuery, PaginatedResult<ProductDto>>
    {
        private readonly IUserFollowProductService _userFollowProductService;
        private readonly IProductClientAPI _productClientAPI;

        public GetListFollowProductHandler(IUserFollowProductService userFollowProductService, IProductClientAPI productClientAPI)
        {
            _userFollowProductService = userFollowProductService;
            _productClientAPI = productClientAPI;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetListFollowProductQuery request, CancellationToken cancellationToken)
        {
            var res = await _userFollowProductService.GetListFollowProduct(request);
            for (int i = 0; i <= res.Items.Count; i++)
            {
                var productResp = await _productClientAPI.GetProduct(res.Items[i].Id);
                if (productResp == null || productResp.Data == null)
                    throw new InternalServiceCommunicationException("Get product failed");

                res.Items[i] = productResp.Data;
            }

            return res;
        }
    }
}
