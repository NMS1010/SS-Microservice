using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Unit.Command
{
    public class DeleteListUnitCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }
    public class DeleteListUnitHandler : IRequestHandler<DeleteListUnitCommand, bool>
    {
        private readonly IUnitService _unitService;

        public DeleteListUnitHandler(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public async Task<bool> Handle(DeleteListUnitCommand request, CancellationToken cancellationToken)
        {
            return await _unitService.DeleteListUnit(request);
        }
    }
}
