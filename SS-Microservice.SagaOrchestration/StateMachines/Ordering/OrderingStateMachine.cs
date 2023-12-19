using MassTransit;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Contracts.Commands.Basket;
using SS_Microservice.Contracts.Commands.Inventory;
using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Contracts.Events.Basket;
using SS_Microservice.Contracts.Events.Inventory;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Contracts.Events.Product;
using SS_Microservice.Contracts.Models;
using SS_Microservice.SagaOrchestration.Messaging.Commands.Basket;
using SS_Microservice.SagaOrchestration.Messaging.Commands.Inventory;
using SS_Microservice.SagaOrchestration.Messaging.Commands.Product;
using SS_Microservice.SagaOrchestration.Messaging.Events.Order;
using SS_Microservice.SagaOrchestration.StateInstances.Ordering;

namespace SS_Microservice.SagaOrchestration.StateMachines.Ordering
{
    public class OrderingStateMachine : MassTransitStateMachine<OrderingStateInstance>
    {
        private readonly Serilog.ILogger _logger;

        // States
        private State OrderCreated { get; set; }

        private State StockReserved { get; set; }
        private State StockReservationRejected { get; set; }

        private State InventoryExported { get; set; }
        private State InventoryExportationRejected { get; set; }

        private State BasketCleared { get; set; }
        private State BasketClearedRejected { get; set; }

        // Commands
        public Event<IReserveStockCommand> ReserveStockCommand { get; set; }
        public Event<IExportInventoryCommand> ExportInventoryCommand { get; set; }
        public Event<IClearBasketCommand> ClearBasketCommand { get; set; }

        // Events
        public Event<IOrderCreatedEvent> OrderCreatedEvent { get; set; }

        public Event<IStockReservedEvent> StockReservedEvent { get; set; }
        public Event<IStockReservationRejectedEvent> StockReservationRejectedEvent { get; set; }

        public Event<IInventoryExportedEvent> InventoryExportedEvent { get; set; }
        public Event<IInventoryExportationRejectedEvent> InventoryExportationRejectedEvent { get; set; }

        public Event<IBasketClearedEvent> BasketClearedEvent { get; set; }
        public Event<IBasketClearedRejectedEvent> BasketClearRejectedEvent { get; set; }

        public OrderingStateMachine()
        {
            _logger = Serilog.Log.Logger;
            InstanceState(x => x.CurrentState);

            State(() => OrderCreated);
            State(() => StockReserved);
            State(() => StockReservationRejected);
            State(() => InventoryExported);
            State(() => InventoryExportationRejected);
            State(() => BasketCleared);
            State(() => BasketClearedRejected);

            Event(() => OrderCreatedEvent, y => y.CorrelateBy<long>(state => state.OrderId, context => context.Message.OrderId)
            .SelectId(ctx => Guid.NewGuid()));

            Event(() => ReserveStockCommand, y => y.CorrelateById(y => y.Message.CorrelationId));
            Event(() => ExportInventoryCommand, y => y.CorrelateById(y => y.Message.CorrelationId));
            Event(() => ClearBasketCommand, y => y.CorrelateById(y => y.Message.CorrelationId));

            Event(() => StockReservedEvent, y => y.CorrelateById(y => y.Message.CorrelationId));
            Event(() => StockReservationRejectedEvent, y => y.CorrelateById(y => y.Message.CorrelationId));

            Event(() => InventoryExportedEvent, y => y.CorrelateById(y => y.Message.CorrelationId));
            Event(() => InventoryExportationRejectedEvent, y => y.CorrelateById(y => y.Message.CorrelationId));

            Event(() => BasketClearedEvent, y => y.CorrelateById(y => y.Message.CorrelationId));
            Event(() => BasketClearRejectedEvent, y => y.CorrelateById(y => y.Message.CorrelationId));

            Initially(
               When(OrderCreatedEvent)
                   .Then(context =>
                   {
                       _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                              .Information("OrderCreatedEvent received in OrderingStateMachine: {ContextSaga} ", context.Saga);
                   })
                   .Then(context =>
                        {
                            context.Saga.OrderId = context.Message.OrderId;
                            context.Saga.OrderCode = context.Message.OrderCode;
                            context.Saga.UserId = context.Message.UserId;
                            context.Saga.UserName = context.Message.UserName;
                            context.Saga.Email = context.Message.Email;
                            context.Saga.PaymentMethod = context.Message.PaymentMethod;
                            context.Saga.TotalPrice = context.Message.TotalPrice;
                            context.Saga.CreatedAt = DateTime.Now;
                            context.Saga.Address = context.Message.Address;
                            context.Saga.Receiver = context.Message.Receiver;
                            context.Saga.ReceiverEmail = context.Message.ReceiverEmail;
                            context.Saga.Phone = context.Message.Phone;
                            context.Saga.ProductInstances = context.Message.Products.Select(x =>
                            {
                                return new ProductInstance
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,
                                    VariantId = x.VariantId
                                };
                            }).ToList();
                        }
                   )
                   .Send(new Uri($"queue:{EventBusConstant.ExportInventory}"),
                       context => new ExportInventoryCommand()
                       {
                           CorrelationId = context.Saga.CorrelationId,
                           OrderId = context.Saga.OrderId,
                           Stocks = context.Saga.ProductInstances.Select(x => new ProductStock
                           {
                               ProductId = x.ProductId,
                               VariantId = x.VariantId,
                               Quantity = x.Quantity
                           }).ToList()
                       }
                    )
                   .TransitionTo(OrderCreated)
                   .Then(context =>
                   {
                       _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                              .Information("ExportInventoryCommand sent in OrderingStateMachine: {ContextSaga} ", context.Saga);
                   })
            );

            During(OrderCreated,
                When(InventoryExportedEvent)
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("InventoryExportedEvent received in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                    .TransitionTo(InventoryExported)
                    .Send(new Uri($"queue:{EventBusConstant.ReserveStock}"),
                        context => new ReserveStockCommand()
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            Stocks = context.Saga.ProductInstances.Select(x => new ProductStock
                            {
                                ProductId = x.ProductId,
                                VariantId = x.VariantId,
                                Quantity = x.Quantity
                            }).ToList()
                        }
                    )
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("ReserveStockCommand sent in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    }),
                When(InventoryExportationRejectedEvent)
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("InventoryExportationRejectedEvent received in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                    .TransitionTo(InventoryExportationRejected)
                    .Publish<OrderingStateInstance, IInventoryExportationRejectedEvent, IOrderCreationRejectedEvent>(context => new OrderCreationRejectedEvent()
                    {
                        OrderId = context.Saga.OrderId,
                        UserId = context.Saga.UserId,
                    })
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("OrderCreationRejectedEvent publish in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
            );

            During(InventoryExported,
                When(StockReservedEvent)
                    .Then(context =>
                    {
                        context.Saga.Image = context.Message.Image;
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("StockReservedEvent received in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                   .TransitionTo(StockReserved)
                    .Send(new Uri($"queue:{EventBusConstant.ClearBasket}"),
                        context => new ClearBasketCommand()
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            UserId = context.Saga.UserId,
                            VariantIds = context.Saga.ProductInstances.Select(x => x.VariantId).ToList()
                        }
                    )
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("ClearBasketCommand sent in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    }),
                When(StockReservationRejectedEvent)
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("StockReservationRejectedEvent received in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                    .TransitionTo(StockReservationRejected)
                    .Publish<OrderingStateInstance, IStockReservationRejectedEvent, IOrderCreationRejectedEvent>(context => new OrderCreationRejectedEvent()
                    {
                        OrderId = context.Saga.OrderId,
                        UserId = context.Saga.UserId,
                    })
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("OrderCreationRejectedEvent publish in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                    .Send(new Uri($"queue:{EventBusConstant.RollBackInventory}"),
                        context => new RollBackInventoryCommand()
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            OrderId = context.Saga.OrderId
                        }
                    )
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("RollBackInventoryCommand sent in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
            );

            During(
                StockReserved,
                When(BasketClearedEvent)
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("BasketClearedEvent received in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                    .TransitionTo(BasketCleared)
                    .Publish<OrderingStateInstance, IBasketClearedEvent, IOrderCreationCompletedEvent>(context => new OrderCreationCompletedEvent()
                    {
                        OrderId = context.Saga.OrderId,
                        OrderCode = context.Saga.OrderCode,
                        UserId = context.Saga.UserId,
                        Image = context.Saga.Image,
                        Address = context.Saga.Address,
                        Receiver = context.Saga.Receiver,
                        ReceiverEmail = context.Saga.ReceiverEmail,
                        Phone = context.Saga.Phone,
                        Email = context.Saga.Email,
                        UserName = context.Saga.UserName,
                        PaymentMethod = context.Saga.PaymentMethod,
                        TotalPrice = context.Saga.TotalPrice,
                    })
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("OrderCreationCompletedEvent publish in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                    .Finalize(),
                When(BasketClearRejectedEvent)
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("BasketClearRejectedEvent received in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                    .TransitionTo(BasketClearedRejected)
                    .Publish<OrderingStateInstance, IBasketClearedRejectedEvent, IOrderCreationRejectedEvent>(context => new OrderCreationRejectedEvent()
                    {
                        OrderId = context.Saga.OrderId,
                        UserId = context.Saga.UserId,

                    })
                    .Send(new Uri($"queue:{EventBusConstant.RollBackInventory}"),
                        context => new RollBackInventoryCommand()
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            OrderId = context.Saga.OrderId
                        }
                    )
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("RollBackInventoryCommand sent in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
                    .Send(new Uri($"queue:{EventBusConstant.RollBackStock}"),
                        context => new RollBackStockCommand()
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            Stocks = context.Saga.ProductInstances.Select(x => new ProductStock
                            {
                                ProductId = x.ProductId,
                                VariantId = x.VariantId,
                                Quantity = x.Quantity
                            }).ToList()
                        }
                    )
                    .Then(context =>
                    {
                        _logger.ForContext("CorrelationId", context.Saga.CorrelationId)
                                .Information("RollBackStockCommand sent in OrderingStateMachine: {ContextSaga} ", context.Saga);
                    })
            );

            SetCompletedWhenFinalized();
        }
    }
}
