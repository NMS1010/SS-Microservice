using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Sale.Commands
{
    public class ApplySaleCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class ApplySaleHandler : IRequestHandler<ApplySaleCommand, bool>
    {
        private readonly ISaleService _saleService;

        public ApplySaleHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<bool> Handle(ApplySaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleService.ApplySale(request);
        }
    }
}
