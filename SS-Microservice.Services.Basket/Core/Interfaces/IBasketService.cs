using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Message.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Message.Basket.Queries;

namespace SS_Microservice.Services.Basket.Core.Interfaces
{
    public interface IBasketService
    {
        Task<int> CreateBasket(CreateBasketCommand command);

        Task<BasketDto> GetBasket(GetBasketQuery query);

        Task<bool> AddProductToBasket(AddBasketCommand command);

        Task<bool> UpdateProductQuantity(UpdateBasketCommand command);

        Task<bool> RemoveProductFromBasket(DeleteBasketCommand command);

        Task<bool> ClearBasket(ClearBasketCommand command);
    }
}