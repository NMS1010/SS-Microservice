using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Sale;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Commands
{
    public class UpdateSaleCommand : UpdateSaleRequest, IRequest<bool>
    {
    }

    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, bool>
    {
        private readonly ISaleService _saleService;

        public UpdateSaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleService.UpdateSale(request);
        }
    }
}
