using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;

namespace SS_Microservice.Services.Basket.Application.Interfaces
{
    public interface IBasketService
    {
        Task<long> CreateBasket(CreateBasketCommand command);

        Task<bool> CreateBasketItem(CreateBasketItemCommand command);

        Task<bool> UpdateBasketItem(UpdateBasketItemCommand command);

        Task<bool> DeleteBasketItem(DeleteBasketItemCommand command);

        Task<bool> DeleteListBasketItem(DeleteListBasketItemCommand command);

        Task<bool> ClearBasket(ClearBasketCommand command);

        Task<PaginatedResult<BasketItemDto>> GetBasketByUser(GetListBasketByUserQuery query);

        Task<List<BasketItemDto>> GetBasketItemByIds(GetListBasketItemQuery query);

    }
}