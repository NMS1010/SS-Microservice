using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Unit.Command;
using SS_Microservice.Services.Products.Application.Features.Unit.Query;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IUnitService
    {
        Task<PaginatedResult<UnitDto>> GetListUnit(GetListUnitQuery query);

        Task<UnitDto> GetUnit(GetUnitQuery query);

        Task<long> CreateUnit(CreateUnitCommand command);

        Task<bool> UpdateUnit(UpdateUnitCommand command);

        Task<bool> DeleteUnit(DeleteUnitCommand command);

        Task<bool> DeleteListUnit(DeleteListUnitCommand command);
    }
}