using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Delivery.Commands;
using SS_Microservice.Services.Order.Application.Features.Delivery.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IDeliveryService
    {
        Task<long> CreateDelivery(CreateDeliveryCommand command);

        Task<bool> UpdateDelivery(UpdateDeliveryCommand command);

        Task<bool> DeleteDelivery(DeleteDeliveryCommand command);

        Task<bool> DeleteListDelivery(DeleteListDeliveryCommand command);

        Task<PaginatedResult<DeliveryDto>> GetListDelivery(GetListDeliveryQuery query);

        Task<DeliveryDto> GetDelivery(GetDeliveryQuery query);
    }
}