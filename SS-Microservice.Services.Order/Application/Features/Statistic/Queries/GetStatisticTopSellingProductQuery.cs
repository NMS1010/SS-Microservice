using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Statistic;
using SS_Microservice.Services.Order.Infrastructure.Services.Product;
using SS_Microservice.Services.Order.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.Order.Application.Features.Statistic.Queries
{
    public class GetStatisticTopSellingProductQuery : GetStatisticTopSellingProductRequest, IRequest<List<StatisticTopSellingProductDto>>
    {
        public List<ProductDto> Products { get; set; }
    }

    public class GetStatisticTopSellingProductHandler : IRequestHandler<GetStatisticTopSellingProductQuery, List<StatisticTopSellingProductDto>>
    {
        private readonly IStatisticService _statisticService;
        private readonly IProductClientAPI _productClientAPI;

        public GetStatisticTopSellingProductHandler(IStatisticService statisticService, IProductClientAPI productClientAPI)
        {
            _statisticService = statisticService;
            _productClientAPI = productClientAPI;
        }

        public async Task<List<StatisticTopSellingProductDto>> Handle(GetStatisticTopSellingProductQuery request, CancellationToken cancellationToken)
        {
            var productResp = await _productClientAPI.GetListProduct(new Infrastructure.Services.Product.Model.Request.GetProductPagingRequest()
            {
                PageSize = int.MaxValue
            });
            if (productResp == null || productResp.Data == null)
            {
                throw new InternalServiceCommunicationException("Cannot get product list");
            }

            request.Products = productResp.Data.Items;

            return await _statisticService.GetStatisticTopSellingProduct(request);
        }
    }
}
