using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Sale;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Commands
{
    public class CreateSaleCommand : CreateSaleRequest, IRequest<long>
    {
    }

    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, long>
    {
        private readonly ISaleService _saleService;

        public CreateSaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<long> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleService.CreateSale(request);
        }
    }
}
