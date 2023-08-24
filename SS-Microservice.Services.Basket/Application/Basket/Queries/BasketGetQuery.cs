using MediatR;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Basket.Queries
{
    public class BasketGetQuery : BasketPagingRequest, IRequest<BasketDto>
    {
        public string UserId { get; set; }
    }
}