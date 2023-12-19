using AutoMapper;
using SS_Microservice.Contracts.Commands.Mail;
using SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Commands.Mail;

namespace SS_Microservice.Services.Infrastructure.Application.Common.AutoMapper
{
    public class MailProfile : Profile
    {
        public MailProfile()
        {
            CreateMap<ISendMailCommand, SendMailCommand>();
        }
    }
}
