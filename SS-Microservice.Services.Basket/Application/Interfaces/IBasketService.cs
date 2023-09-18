using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;

namespace SS_Microservice.Services.Basket.Application.Interfaces
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