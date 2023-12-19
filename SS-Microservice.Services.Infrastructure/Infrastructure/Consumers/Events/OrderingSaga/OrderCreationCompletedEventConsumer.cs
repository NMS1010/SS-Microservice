using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Infrastructure.Application.Common.Constants;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Commands.Mail;
using System.Globalization;

namespace SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Events.Order
{
    public class OrderCreationCompletedEventConsumer : IConsumer<IOrderCreationCompletedEvent>
    {
        private readonly ISender _sender;
        private readonly IMailService _mailService;
        private readonly ILogger<OrderCreationCompletedEventConsumer> _logger;

        public OrderCreationCompletedEventConsumer(IMailService mailService, ISender sender, ILogger<OrderCreationCompletedEventConsumer> logger)
        {
            _mailService = mailService;
            _sender = sender;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IOrderCreationCompletedEvent> context)
        {
            var notificationCommand = new CreateOrderNotificationCommand
            {
                UserId = context.Message.UserId,
                Title = "Đặt hàng thành công",
                Content = $"Đơn hàng #{context.Message.OrderCode} của bạn đã được hệ thống ghi nhận và đang được xử lý",
                Anchor = "/user/order/" + context.Message.OrderCode,
                Status = false,
                Type = NOTIFICATION_TYPE.ORDER,
                Image = context.Message.Image
            };

            if (context.Message.PaymentMethod.ToLower() == "paypal")
            {
                notificationCommand.Title = "Đơn hàng cần thanh toán";
                notificationCommand.Content = $"Đơn hàng #{context.Message.OrderCode} của bạn cần được thanh toán qua Paypal trước khi hệ thống có thể xử lý";
                notificationCommand.Anchor = "/checkout/payment/" + context.Message.OrderCode;
            }
            else
            {
                var payloads = new Dictionary<string, string>()
                {
                    { "address", context.Message.Address},
                    { "receiver", context.Message.Receiver},
                    { "phone", context.Message.Phone},
                    { "email", context.Message.ReceiverEmail},
                    { "name", context.Message.UserName },
                    { "paymentMethod", context.Message.PaymentMethod },
                    { "totalPrice", context.Message.TotalPrice.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")) }
                };

                var sendMailCommand = new SendMailCommand()
                {
                    To = context.Message.Email,
                    Type = MAIL_TYPE.ORDER_CONFIRMATION,
                    Payloads = payloads
                };
                _mailService.SendMail(sendMailCommand);
            }

            await _sender.Send(notificationCommand);
        }
    }
}