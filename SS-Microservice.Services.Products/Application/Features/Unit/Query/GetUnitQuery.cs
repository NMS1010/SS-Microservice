using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Unit.Query
{
    public class GetUnitQuery : IRequest<UnitDto>
    {
        public long Id { get; set; }
    }

    public class GetUnitHandler : IRequestHandler<GetUnitQuery, UnitDto>
    {
        private readonly IUnitService _unitService;

        public GetUnitHandler(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public async Task<UnitDto> Handle(GetUnitQuery request, CancellationToken cancellationToken)
        {
            return await _unitService.GetUnit(request);
        }
    }
}
