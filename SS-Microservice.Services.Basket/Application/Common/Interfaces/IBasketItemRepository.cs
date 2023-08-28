using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Basket.Core.Entities;

namespace SS_Microservice.Services.Basket.Application.Common.Interfaces
{
    public interface IBasketItemRepository : IGenericRepository<BasketItem>
    {
        Task<PaginatedResult<BasketItem>> GetBasketItem(int basketId, int pageIndex, int pageSize);

        Task<BasketItem> IsBasketItemExist(int basketId, string productId);
    }
}