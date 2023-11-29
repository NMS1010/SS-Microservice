using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Unit;

namespace SS_Microservice.Services.Products.Application.Features.Unit.Command
{
    public class CreateUnitCommand : CreateUnitRequest, IRequest<long>
    {
    }

    public class CreateUnitHandler : IRequestHandler<CreateUnitCommand, long>
    {
        private readonly IUnitService _unitService;

        public CreateUnitHandler(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public async Task<long> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            return await _unitService.CreateUnit(request);
        }
    }
}
