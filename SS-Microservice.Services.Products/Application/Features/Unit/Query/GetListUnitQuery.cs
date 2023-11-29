using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Model.Unit;

namespace SS_Microservice.Services.Products.Application.Features.Unit.Query
{
    public class GetListUnitQuery : GetUnitPagingRequest, IRequest<PaginatedResult<UnitDto>>
    {
    }

    public class GetListUnitHandler : IRequestHandler<GetListUnitQuery, PaginatedResult<UnitDto>>
    {
        private readonly IUnitService _unitService;

        public GetListUnitHandler(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public async Task<PaginatedResult<UnitDto>> Handle(GetListUnitQuery request, CancellationToken cancellationToken)
        {
            return await _unitService.GetListUnit(request);
        }
    }
}
