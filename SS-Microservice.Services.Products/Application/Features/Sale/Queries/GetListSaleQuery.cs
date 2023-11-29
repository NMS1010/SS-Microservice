using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Sale;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Queries
{
    public class GetListSaleQuery : GetSalePagingRequest, IRequest<PaginatedResult<SaleDto>>
    {
    }

    public class GetListSaleHandler : IRequestHandler<GetListSaleQuery, PaginatedResult<SaleDto>>
    {
        private readonly ISaleService _saleService;

        public GetListSaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<PaginatedResult<SaleDto>> Handle(GetListSaleQuery request, CancellationToken cancellationToken)
        {
            return await _saleService.GetListSale(request);
        }
    }
}
