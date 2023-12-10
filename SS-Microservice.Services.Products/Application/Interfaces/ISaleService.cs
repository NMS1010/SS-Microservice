using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Sale.Commands;
using SS_Microservice.Services.Products.Application.Features.Sale.Queries;

namespace SS_Microservice.Services.Products.Application.Interfaces
{
    public interface ISaleService
    {
        Task<PaginatedResult<SaleDto>> GetListSale(GetListSaleQuery query);

        Task<SaleDto> GetSale(GetSaleQuery query);

        Task<SaleDto> GetSaleLatest(GetLatestSaleQuery query);

        Task<long> CreateSale(CreateSaleCommand command);

        Task<bool> UpdateSale(UpdateSaleCommand command);

        Task<bool> DeleteSale(DeleteSaleCommand command);

        Task<bool> DeleteListSale(DeleteListSaleCommand command);

        Task<bool> ApplySale(ApplySaleCommand command);

        Task<bool> CancelSale(CancelSaleCommand command);
    }
}