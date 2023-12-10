using AutoMapper;
using MassTransit;
using SS_Microservice.Contracts.Commands.Mail;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;

namespace SS_Microservice.Services.Infrastructure.Application.Messaging.Commands.Mail
{
    public class SendMailCommand : ISendMailCommand
    {
        public string To { get; set; }
        public string Type { get; set; }
        public IDictionary<string, string> Payloads { get; set; }
    }
    public class SendMailCommandConsumer : IConsumer<ISendMailCommand>
    {
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public SendMailCommandConsumer(IMailService mailService, IMapper mapper)
        {
            _mailService = mailService;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<ISendMailCommand> context)
        {
            SendMailCommand message = _mapper.Map<SendMailCommand>(context.Message);

            _mailService.SendMail(message);

            return Task.CompletedTask;
        }
    }
}
