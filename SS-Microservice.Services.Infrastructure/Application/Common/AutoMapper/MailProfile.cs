using AutoMapper;
using SS_Microservice.Common.Messages.Commands.Mail;
using SS_Microservice.Services.Infrastructure.Application.Messaging.Commands.Mail;

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
