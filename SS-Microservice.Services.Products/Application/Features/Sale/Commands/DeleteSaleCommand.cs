using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Commands
{
    public class DeleteSaleCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        private readonly ISaleService _saleService;

        public DeleteSaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleService.DeleteSale(request);
        }
    }
}
