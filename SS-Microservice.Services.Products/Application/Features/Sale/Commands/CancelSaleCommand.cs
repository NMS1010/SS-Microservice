using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Commands
{
    public class CancelSaleCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleService _saleService;

        public CancelSaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleService.CancelSale(request);
        }
    }
}
