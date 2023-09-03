using MediatR;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.Queries
{
    public class GetBasketQuery : BasketPagingRequest, IRequest<BasketDto>
    {
        public string UserId { get; set; }
    }
}