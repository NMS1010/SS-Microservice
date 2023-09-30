using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(CreateCategoryCommand command);

        Task<bool> UpdateCategory(UpdateCategoryCommand command);

        Task<bool> DeleteCategory(DeleteCategoryCommand command);

        Task<PaginatedResult<CategoryDto>> GetAllCategory(GetAllCategoryQuery query);

        Task<CategoryDto> GetCategoryById(GetCategoryByIdQuery query);
    }
}