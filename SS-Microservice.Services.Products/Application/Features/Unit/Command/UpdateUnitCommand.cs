using MediatR;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Unit;

namespace SS_Microservice.Services.Products.Application.Features.Unit.Command
{
    public class UpdateUnitCommand : UpdateUnitRequest, IRequest<bool>
    {
    }

    public class UpdateUnitHandler : IRequestHandler<UpdateUnitCommand, bool>
    {
        private readonly IUnitService _unitService;

        public UpdateUnitHandler(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public async Task<bool> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            return await _unitService.UpdateUnit(request);
        }
    }
}
