using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Interfaces.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<PaginatedResult<Brand>> FilterBrand(GetAllBrandQuery query);
    }
}