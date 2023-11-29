using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Queries
{
    public class GetSaleQuery : IRequest<SaleDto>
    {
        public long Id { get; set; }
    }

    public class GetSaleHandler : IRequestHandler<GetSaleQuery, SaleDto>
    {
        private readonly ISaleService _saleService;

        public GetSaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<SaleDto> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            return await _saleService.GetSale(request);
        }
    }
}
