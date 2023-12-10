using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Variant.Commands;
using SS_Microservice.Services.Products.Application.Features.Variant.Queries;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IVariantService
    {
        Task<PaginatedResult<VariantDto>> GetListVariant(GetListVariantQuery query);

        Task<VariantDto> GetVariant(GetVariantQuery query);

        Task<List<VariantDto>> GetListVariantByProductId(GetListVariantByProductQuery query);

        Task<long> CreateVariant(CreateVariantCommand command);

        Task<bool> UpdateVariant(UpdateVariantCommand command);

        Task<bool> DeleteVariant(DeleteVariantCommand command);

        Task<bool> DeleteListVariant(DeleteListVariantCommand command);
    }
}