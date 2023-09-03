using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.Commands
{
    public class ClearBasketCommand : IRequest<bool>
    {
        public int BasketId { get; set; }
    }
}