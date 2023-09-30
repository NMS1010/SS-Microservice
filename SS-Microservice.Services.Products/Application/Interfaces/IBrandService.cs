using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Brand.Commands;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IBrandService
    {
        Task<bool> AddBrand(CreateBrandCommand command);

        Task<bool> UpdateBrand(UpdateBrandCommand command);

        Task<bool> DeleteBrand(DeleteBrandCommand command);

        Task<PaginatedResult<BrandDto>> GetAllBrand(GetAllBrandQuery query);

        Task<BrandDto> GetBrandById(GetBrandByIdQuery query);
    }
}