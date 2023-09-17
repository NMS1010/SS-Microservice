using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Products.Application.Message.Category.Queries;
using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<PaginatedResult<Category>> FilterCategory(GetAllCategoryQuery query);
    }
}