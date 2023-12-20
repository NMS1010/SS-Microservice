using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Commands
{
    public class UpdateProductRatingCommand : IUpdateProductRatingCommand, IRequest
    {
        public List<ProductRating> ProductRatings { get; set; }

        public Guid CorrelationId { get; set; }
    }

    public class UpdateProductRatingCommandHanlder : IRequestHandler<UpdateProductRatingCommand>
    {
        private readonly ILogger<UpdateProductRatingCommandHanlder> _logger;
        private readonly ISendEndpointProvider _sendEndpoint;
        private readonly string _handlerName = nameof(UpdateProductRatingCommandHanlder);

        public UpdateProductRatingCommandHanlder(ILogger<UpdateProductRatingCommandHanlder> logger, ISendEndpointProvider sendEndpoint)
        {
            _logger = logger;
            _sendEndpoint = sendEndpoint;
        }

        public async Task Handle(UpdateProductRatingCommand request, CancellationToken cancellationToken)
        {
            var endpoint = await _sendEndpoint.GetSendEndpoint(new Uri($"queue:{EventBusConstant.UpdateProductRating}"));

            LoggerMessaging.StartPublishing(APPLICATION_SERVICE.USER_OPERATION_SERVICE, nameof(UpdateProductRatingCommand), _handlerName);

            await endpoint.Send<IUpdateProductRatingCommand>(request);

            LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.USER_OPERATION_SERVICE, nameof(UpdateProductRatingCommand), _handlerName);
        }
    }
}
