using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Infrastructure.Consumers.Commands.OrderingSaga;
using SS_Microservice.Services.Basket.Infrastructure.Consumers.Events.User;

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