using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<long> CreatePaymentMethod(CreatePaymentMethodCommand command);

        Task<bool> UpdatePaymentMethod(UpdatePaymentMethodCommand command);

        Task<bool> DeletePaymentMethod(DeletePaymentMethodCommand command);

        Task<bool> DeleteListPaymentMethod(DeleteListPaymentMethodCommand command);

        Task<PaginatedResult<PaymentMethodDto>> GetListPaymentMethod(GetListPaymentMethodQuery query);

        Task<PaymentMethodDto> GetPaymentMethod(GetPaymentMethodQuery query);
    }
}