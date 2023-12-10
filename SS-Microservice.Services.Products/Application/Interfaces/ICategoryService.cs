using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Features.ListCategory.Commands;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<PaginatedResult<CategoryDto>> GetListCategory(GetListCategoryQuery query);

        Task<CategoryDto> GetCategory(GetCategoryQuery query);

        Task<CategoryDto> GetCategoryBySlug(GetCategoryBySlugQuery query);

        Task<long> CreateCategory(CreateCategoryCommand command);

        Task<bool> UpdateCategory(UpdateCategoryCommand command);

        Task<bool> DeleteCategory(DeleteCategoryCommand command);

        Task<bool> DeleteListCategory(DeleteListCategoryCommand command);
    }
}