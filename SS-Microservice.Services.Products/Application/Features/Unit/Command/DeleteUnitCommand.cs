using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Unit.Command
{
    public class DeleteUnitCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteUnitHandler : IRequestHandler<DeleteUnitCommand, bool>
    {
        private readonly IUnitService _unitService;

        public DeleteUnitHandler(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public async Task<bool> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            return await _unitService.DeleteUnit(request);
        }
    }
}
