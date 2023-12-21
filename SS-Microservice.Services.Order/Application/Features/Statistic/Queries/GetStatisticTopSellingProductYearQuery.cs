using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Statistic;
using SS_Microservice.Services.Order.Infrastructure.Services.Product;
using SS_Microservice.Services.Order.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.Order.Application.Features.Statistic.Queries
{
    public class GetStatisticTopSellingProductYearQuery : GetStatisticTopSellingProductYearRequest,
        IRequest<List<StatisticTopSellingProductYearDto>>
    {
        public List<ProductDto> Products { get; set; }
    }

    public class GetStatisticTopSellingProductYearHandler : IRequestHandler<GetStatisticTopSellingProductYearQuery,
        List<StatisticTopSellingProductYearDto>>
    {
        private readonly IStatisticService _statisticService;
        private readonly IProductClientAPI _productClientAPI;

        public GetStatisticTopSellingProductYearHandler(IStatisticService statisticService, IProductClientAPI productClientAPI)
        {
            _statisticService = statisticService;
            _productClientAPI = productClientAPI;
        }

        public async Task<List<StatisticTopSellingProductYearDto>> Handle(GetStatisticTopSellingProductYearQuery request,
            CancellationToken cancellationToken)
        {
            var productList = await _productClientAPI.GetListProduct(new Infrastructure.Services.Product.Model.Request.GetProductPagingRequest()
            {
                PageIndex = 1,
                PageSize = 5,
                IsSortAscending = false,
                ColumnName = "Sold"
            });

            request.Products = productList.Data.Items;

            return await _statisticService.GetStatisticTopSellingProductYear(request);
        }
    }
}
