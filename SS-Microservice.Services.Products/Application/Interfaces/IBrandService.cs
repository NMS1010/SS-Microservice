using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Brand.Commands;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface IBrandService
    {
        Task<PaginatedResult<BrandDto>> GetListBrand(GetListBrandQuery query);

        Task<BrandDto> GetBrand(GetBrandQuery query);

        Task<long> CreateBrand(CreateBrandCommand command);

        Task<bool> UpdateBrand(UpdateBrandCommand command);

        Task<bool> DeleteBrand(DeleteBrandCommand command);

        Task<bool> DeleteListBrand(DeleteListBrandCommand command);
    }
}