using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Commands
{
    public class DeleteListSaleCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }
    public class DeleteListSaleHandler : IRequestHandler<DeleteListSaleCommand, bool>
    {
        private readonly ISaleService _saleService;

        public DeleteListSaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<bool> Handle(DeleteListSaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleService.DeleteListSale(request);
        }
    }
}
