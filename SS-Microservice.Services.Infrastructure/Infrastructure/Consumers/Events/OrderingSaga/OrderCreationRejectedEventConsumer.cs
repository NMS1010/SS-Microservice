using MassTransit;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Infrastructure.Application.Common.Constants;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;

namespace SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Events.OrderingSaga
{
    public class OrderCreationRejectedEventConsumer : IConsumer<IOrderCreationRejectedEvent>
    {
        private readonly INotificationService _notificationService;

        public OrderCreationRejectedEventConsumer(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Consume(ConsumeContext<IOrderCreationRejectedEvent> context)
        {
            await _notificationService.CreateOrderNotification(new CreateOrderNotificationCommand
            {
                UserId = context.Message.UserId,
                Title = "Đặt hàng thất bại",
                Content = "Quá trình đặt hàng đã có lỗi xảy ra, vui lòng thử lại sau",
                Anchor = "#",
                Status = false,
                Type = NOTIFICATION_TYPE.ORDER,
                Image = "https://img.freepik.com/premium-vector/illustration-error-500_123815-26.jpg?w=900"
            });
        }
    }
}
