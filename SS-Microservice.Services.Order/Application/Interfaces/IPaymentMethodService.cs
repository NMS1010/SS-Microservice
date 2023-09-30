using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<PaginatedResult<PaymentMethodDto>> GetPaymentMethodList(GetAllPaymentMethodQuery query);

        Task<PaymentMethodDto> GetPaymentMethod(GetPaymentMethodByIdQuery query);

        Task<bool> CreatePaymentMethod(CreatePaymentMethodCommand command);

        Task<bool> UpdatePaymentMethod(UpdatePaymentMethodCommand command);

        Task<bool> DeletePaymentMethod(DeletePaymentMethodCommand command);
    }
}