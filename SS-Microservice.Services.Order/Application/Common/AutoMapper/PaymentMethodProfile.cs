using AutoMapper;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class PaymentMethodProfile : Profile
    {
        public PaymentMethodProfile()
        {
            CreateMap<PaymentMethod, PaymentMethodDto>();

            CreateMap<CreatePaymentMethodCommand, PaymentMethod>();
            CreateMap<UpdatePaymentMethodCommand, PaymentMethod>();

            // mapping request - command
            CreateMap<CreatePaymentMethodRequest, CreatePaymentMethodCommand>();
            CreateMap<UpdatePaymentMethodRequest, UpdatePaymentMethodCommand>();
            CreateMap<GetPaymentMethodPagingRequest, GetListPaymentMethodQuery>();
        }
    }
}
