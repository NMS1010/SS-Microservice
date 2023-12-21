using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Infrastructure.Services.Auth;
using SS_Microservice.Services.Order.Infrastructure.Services.Inventory;

namespace SS_Microservice.Services.Order.Application.Features.Statistic.Queries
{
    public class GetStatisticTotalQuery : IRequest<StatisticTotalDto>
    {
    }

    public class GetStatisticTotalQueryHandler : IRequestHandler<GetStatisticTotalQuery, StatisticTotalDto>
    {
        private readonly IStatisticService _statisticService;
        private readonly IProductGrpcService _productGrpcService;
        private readonly IInventoryClientAPI _inventoryClientAPI;
        private readonly IAuthClientAPI _authClientAPI;

        public GetStatisticTotalQueryHandler(IStatisticService statisticService,
            IProductGrpcService productGrpcService, IInventoryClientAPI inventoryClientAPI, IAuthClientAPI authClientAPI)
        {
            _statisticService = statisticService;
            _productGrpcService = productGrpcService;
            _inventoryClientAPI = inventoryClientAPI;
            _authClientAPI = authClientAPI;
        }

        public async Task<StatisticTotalDto> Handle(GetStatisticTotalQuery request, CancellationToken cancellationToken)
        {
            var res = await _statisticService.GetStatisticTotal(request);

            var docketResp = await _inventoryClientAPI.GetDocketByType("IMPORT");
            if (docketResp == null || docketResp.Data == null)
            {
                throw new InternalServiceCommunicationException("Cannot get list dockets");
            }

            decimal exspense = 0;
            foreach (var docket in docketResp.Data)
            {
                var product = await _productGrpcService.GetProductById(new SS_Microservice.Common.Grpc.Product.Protos.GetProductById()
                {
                    ProductId = docket.ProductId
                }) ?? throw new InternalServiceCommunicationException("Cannot get product");

                exspense += docket.Quantity * (decimal)product.ProductCost;
            }
            res.Expense = exspense;

            var userResp = await _authClientAPI.GetTotalUserByRole("USER")
                ?? throw new InternalServiceCommunicationException("Cannot get list dockets");
            res.Users = userResp.Data;

            return res;
        }
    }
}
