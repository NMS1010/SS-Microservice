using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Delivery.Commands;
using SS_Microservice.Services.Order.Application.Features.Delivery.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IDeliveryService
    {
        Task<PaginatedResult<DeliveryDto>> GetDeliveryList(GetAllDeliveryQuery query);

        Task<DeliveryDto> GetDelivery(GetDeliveryByIdQuery query);

        Task<bool> CreateDelivery(CreateDeliveryCommand command);

        Task<bool> UpdateDelivery(UpdateDeliveryCommand command);

        Task<bool> DeleteDelivery(DeleteDeliveryCommand command);
    }
}