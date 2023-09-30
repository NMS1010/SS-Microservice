using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Variant.Commands;
using SS_Microservice.Services.Products.Application.Features.Variant.Queries;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IProductVariantService
    {
        Task<bool> AddVariant(CreateVariantCommand command);

        Task<bool> UpdateVariant(UpdateVariantCommand command);

        Task<bool> DeleteVariant(DeleteVariantCommand command);

        Task<PaginatedResult<VariantDto>> GetAllVariant(GetAllVariantQuery query);

        Task<VariantDto> GetVariantById(GetVariantByIdQuery query);
    }
}