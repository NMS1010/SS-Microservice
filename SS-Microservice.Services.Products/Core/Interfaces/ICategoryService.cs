using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Message.Category.Commands;
using SS_Microservice.Services.Products.Application.Message.Category.Queries;

namespace SS_Microservice.Services.Products.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(CreateCategoryCommand command);

        Task<bool> UpdateCategory(UpdateCategoryCommand command);

        Task<bool> DeleteCategory(DeleteCategoryCommand command);

        Task<PaginatedResult<CategoryDTO>> GetAllCategory(GetAllCategoryQuery query);

        Task<CategoryDTO> GetCategoryById(GetCategoryByIdQuery query);
    }
}