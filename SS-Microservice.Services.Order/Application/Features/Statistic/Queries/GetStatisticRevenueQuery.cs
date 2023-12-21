using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Statistic;
using SS_Microservice.Services.Order.Infrastructure.Services.Inventory;
using SS_Microservice.Services.Order.Infrastructure.Services.Inventory.Model.Request;

namespace SS_Microservice.Services.Order.Application.Features.Statistic.Queries
{
    public class GetStatisticRevenueQuery : GetStatisticRevenueRequest, IRequest<List<StatisticRevenueDto>>
    {
    }

    public class GetStatisticRevenueHandler : IRequestHandler<GetStatisticRevenueQuery, List<StatisticRevenueDto>>
    {
        private readonly IStatisticService _statisticService;
        private readonly IInventoryClientAPI _inventoryClientAPI;
        private readonly IProductGrpcService _productGrpcService;

        public GetStatisticRevenueHandler(IStatisticService statisticService,
            IInventoryClientAPI inventoryClientAPI, IProductGrpcService productGrpcService)
        {
            _statisticService = statisticService;
            _inventoryClientAPI = inventoryClientAPI;
            _productGrpcService = productGrpcService;
        }

        public async Task<List<StatisticRevenueDto>> Handle(GetStatisticRevenueQuery request, CancellationToken cancellationToken)
        {
            var res = await _statisticService.GetStatisticRevenue(request);

            var itemRequest = new List<DocketByDateItem>();
            for (int month = 1; month <= 12; month++)
            {
                itemRequest.Add(new DocketByDateItem()
                {
                    FirstDate = new(request.Year, month, 1),
                    LastDate = new(request.Year, month, DateTime.DaysInMonth(request.Year, month)),
                    Type = "IMPORT"
                });
            }
            var req = new GetDocketByDateRequest()
            {
                Items = itemRequest
            };

            var docketResp = await _inventoryClientAPI.GetDocketByDate(req);

            if (docketResp == null || docketResp.Data == null)
            {
                throw new InternalServiceCommunicationException("Cannot get list dockets");
            }

            var dockets = docketResp.Data;

            for (int month = 1; month <= 12; month++)
            {
                decimal exspense = 0;
                foreach (var docket in dockets[month - 1])
                {
                    var product = await _productGrpcService.GetProductById(new SS_Microservice.Common.Grpc.Product.Protos.GetProductById()
                    {
                        ProductId = docket.ProductId
                    }) ?? throw new InternalServiceCommunicationException("Cannot get product");

                    exspense += docket.Quantity * (decimal)product.ProductCost;
                }
                res[month - 1].Expense = exspense;
            }

            return res;
        }
    }
}
