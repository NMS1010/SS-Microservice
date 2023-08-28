using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Dto;

namespace SS_Microservice.Services.Basket.Application.Common.Interfaces
{
    public interface IBasketService
    {
        Task<int> CreateBasket(BasketCreateCommand command);

        Task<BasketDto> GetBasket(BasketGetQuery query);

        Task AddProductToBasket(BasketAddCommand command);

        Task<bool> UpdateProductQuantity(BasketUpdateCommand command);

        Task<bool> RemoveProductFromBasket(BasketDeleteCommand command);
    }
}