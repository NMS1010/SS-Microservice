using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Queries
{
    public class GetLatestSaleQuery : IRequest<SaleDto>
    {
    }

    public class GetLatestSaleHandler : IRequestHandler<GetLatestSaleQuery, SaleDto>
    {
        private readonly ISaleService _saleService;

        public GetLatestSaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<SaleDto> Handle(GetLatestSaleQuery request, CancellationToken cancellationToken)
        {
            return await _saleService.GetSaleLatest(request);
        }
    }
}
