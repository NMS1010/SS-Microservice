using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.StringUtil;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Application.Common.Constants;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Order;
using SS_Microservice.Services.Order.Application.Specifications.Order;
using SS_Microservice.Services.Order.Domain.Entities;
using SS_Microservice.Services.Order.Infrastructure.Consumers.Events.OrderingSaga;

namespace SS_Microservice.Services.Order.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        private async Task<Domain.Entities.Order> InitOrder(string userId, CreateOrderRequest request)
        {

            var delivery = await _unitOfWork.Repository<Delivery>().GetById(request.DeliveryId)
                ?? throw new InvalidRequestException("Unexpected deliveryId");

            var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(request.PaymentMethodId)
                ?? throw new InvalidRequestException("Unexpected paymentMethodId");

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in request.Items)
            {
                var variantPrice = item.Quantity * item.TotalPrice;

                totalAmount += variantPrice;
                orderItems.Add(new OrderItem()
                {
                    Quantity = item.Quantity,
                    VariantId = item.VariantId,
                    UnitPrice = item.TotalPrice,
                    TotalPrice = variantPrice
                });
            }

            var tax = totalAmount * ORDER_TAX.TAX;
            totalAmount = totalAmount + delivery.Price + tax;

            var transaction = new Transaction()
            {
                PaymentMethod = paymentMethod.Code,
                TotalPay = totalAmount
            };


            var order = new Domain.Entities.Order()
            {
                UserId = userId,
                AddressId = request.AddressId,
                DeliveryMethod = delivery.Name,
                Transaction = transaction,
                OrderItems = orderItems,
                Note = request.Note,
                ShippingCost = delivery.Price,
                Tax = (double)ORDER_TAX.TAX,
                Code = string.Empty.GenerateUniqueCode(),
                Status = ORDER_STATUS.DRAFT,
                PaymentStatus = false,
                TotalAmount = totalAmount
            };

            return order;
        }


        public async Task<CreateOrderDto> CreateOrder(CreateOrderCommand command)
        {
            var order = await InitOrder(command.UserId, command);

            try
            {
                await _unitOfWork.CreateTransaction();

                await _unitOfWork.Repository<Domain.Entities.Order>().Insert(order);

                await _unitOfWork.Save();

                await _unitOfWork.Commit();

                return new CreateOrderDto()
                {
                    OrderCode = order.Code,
                    OrderId = order.Id,
                    PaymentMethod = order.Transaction.PaymentMethod,
                    TotalPrice = order.Transaction.TotalPay
                };
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task<PaginatedResult<OrderDto>> GetListOrder(GetListOrderQuery query)
        {
            var orders = await _unitOfWork.Repository<Domain.Entities.Order>().ListAsync(new OrderSpecification(query, isPaging: true));
            var count = await _unitOfWork.Repository<Domain.Entities.Order>().CountAsync(new OrderSpecification(query));

            var orderDtos = new List<OrderDto>();
            foreach (var order in orders)
            {
                var listOrderItem = await _unitOfWork.Repository<OrderItem>().ListAsync(new OrderItemSpecification(order.Id));
                var dto = _mapper.Map<OrderDto>(order);
                dto.Items = listOrderItem.Select(x => _mapper.Map<OrderItemDto>(x)).ToList();
                orderDtos.Add(dto);
            }

            return new PaginatedResult<OrderDto>(orderDtos, query.PageIndex, count, query.PageSize);
        }

        public Task<PaginatedResult<OrderDto>> GetListUserOrder(GetListUserOrderQuery request)
        {
            return GetListOrder(_mapper.Map<GetListOrderQuery>(request));
        }

        public async Task<OrderDto> GetOrder(GetOrderQuery query)
        {
            var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(new OrderSpecification(query.Id))
                ?? throw new InvalidRequestException("Unexpected orderId");
            var listOrderItem = await _unitOfWork.Repository<OrderItem>().ListAsync(new OrderItemSpecification(order.Id));

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = listOrderItem.Select(x => _mapper.Map<OrderItemDto>(x)).ToList();

            return orderDto;
        }

        private void ValidateOrderStatus(Domain.Entities.Order order, string status)
        {
            // Cannot update order while it's not paid by user through PayPal
            if (order.Status == ORDER_STATUS.NOT_PROCESSED
                && status != ORDER_STATUS.CANCELLED
                && order.PaymentStatus == false
                && order.Transaction.PaymentMethod == PAYMENT_CODE.PAYPAL)
            {
                throw new InvalidRequestException("Cannot update this order status, it wasn't paid by user through PayPal");
            }

            //  Cannot cancel order while it's paid
            if (status == ORDER_STATUS.CANCELLED
                && order.PaymentStatus == true)
            {
                throw new InvalidRequestException("Cannot update this order status, it was paid by user before");
            }

            // Cannot update order while it's delivered
            if (order.Status == ORDER_STATUS.DELIVERED)
                throw new InvalidRequestException("Unexpected order status, cannot update order while it's delivered");

            // Cannot update order while it's cancelled
            if (order.Status == ORDER_STATUS.CANCELLED)
                throw new InvalidRequestException("Unexpected order status, order was cancelled");

            // Customer cannot cancel order while it's processing, only admin and staff can do that
            if (order.Status != ORDER_STATUS.NOT_PROCESSED
                && status == ORDER_STATUS.CANCELLED
                && !(_currentUserService.IsInRole("ADMIN") || _currentUserService.IsInRole("STAFF")))
                throw new InvalidRequestException("Unexpected order status, cannot cancel order while it's processing");
        }

        private async Task HandleChangeStatus(Domain.Entities.Order order, UpdateOrderRequest request)
        {
            var now = DateTime.Now;
            order.Status = request.Status;
            if (request.Status == ORDER_STATUS.DELIVERED)
            {
                order.PaymentStatus = true;
                if (order.Transaction.PaymentMethod == PAYMENT_CODE.COD)
                    order.Transaction.PaidAt = now;
                order.Transaction.CompletedAt = now;
            }
            else if (request.Status == ORDER_STATUS.CANCELLED)
            {
                if (request.OrderCancellationReasonId.HasValue)
                {
                    var cancellationReason = await _unitOfWork.Repository<OrderCancellationReason>()
                        .GetById(request.OrderCancellationReasonId)
                        ?? throw new InvalidRequestException("Unexpected orderCancellationReasonId");
                    order.CancelReason = cancellationReason;
                }
                else if (!string.IsNullOrEmpty(request.OtherCancellation))
                {
                    order.OtherCancelReason = request.OtherCancellation;
                }

            }
            else
            {
                order.CancelReason = null;
                order.OtherCancelReason = null;
            }
        }

        public async Task<bool> UpdateOrder(UpdateOrderCommand command)
        {

            var spec = new OrderSpecification(command.OrderId);
            if (!string.IsNullOrEmpty(command.UserId))
            {
                spec = new OrderSpecification(command.OrderId, command.UserId);
            }
            var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(spec)
                ?? throw new InvalidRequestException("Unexpected orderId");

            ValidateOrderStatus(order, command.Status);

            await HandleChangeStatus(order, command);

            try
            {
                await _unitOfWork.CreateTransaction();

                _unitOfWork.Repository<Domain.Entities.Order>().Update(order);

                var isSuccess = await _unitOfWork.Save() > 0;

                if (!isSuccess)
                {
                    throw new Exception("Cannot handle to update order, an error has occured");
                }
                await _unitOfWork.Commit();

                return isSuccess;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task<OrderDto> GetOrderByCode(GetOrderByCodeQuery query)
        {
            var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(new OrderSpecification(query.Code, query.UserId))
                ?? throw new InvalidRequestException("Unexpected order code");
            var listOrderItem = await _unitOfWork.Repository<OrderItem>().ListAsync(new OrderItemSpecification(order.Id));
            //var listReview = await _unitOfWork.Repository<Review>().ListAsync(new ReviewSpecification(order.Id));

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = listOrderItem.Select(x => _mapper.Map<OrderItemDto>(x)).ToList();
            //orderDto.IsReview = listReview.Count == listItems.Count;
            //orderDto.ReviewedDate = listReview.Count == listItems.Count ? listReview.Max(x => x.CreatedAt) : null;

            return orderDto;
        }

        public async Task<bool> CompletePaypalOrder(CompletePaypalOrderCommand command)
        {
            var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(new OrderSpecification(command.OrderId, command.UserId))
            ?? throw new InvalidRequestException("Unexpected order id");

            order.Transaction.PaidAt = DateTime.Now;
            order.Transaction.PaypalOrderId = command.PaypalOrderId;
            order.Transaction.PaypalOrderStatus = command.PaypalOrderStatus;
            order.PaymentStatus = true;
            order.Status = ORDER_STATUS.PROCESSING;
            try
            {
                await _unitOfWork.CreateTransaction();

                _unitOfWork.Repository<Domain.Entities.Order>().Update(order);

                await _unitOfWork.Save();
                await _unitOfWork.Commit();

                return true;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<List<OrderDto>> GetTopOrderLatest(GetTopLatestOrderQuery query)
        {
            List<OrderDto> orderDtos = new();
            var orders = await _unitOfWork.Repository<Domain.Entities.Order>().ListAsync(new OrderSpecification(query.Top));
            orders.ForEach(order => orderDtos.Add(_mapper.Map<OrderDto>(order)));
            return orderDtos;
        }

        public async Task<bool> DeleteOrder(DeleteOrderCommand command)
        {
            var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(new OrderSpecification(command.Id))
                ?? throw new InvalidRequestException("Unexpected orderId");

            try
            {
                foreach (var item in order.OrderItems)
                {
                    _unitOfWork.Repository<OrderItem>().Delete(item);
                }

                var transaction = order.Transaction;
                _unitOfWork.Repository<Transaction>().Delete(transaction);

                _unitOfWork.Repository<Domain.Entities.Order>().Delete(order);

                var isSuccess = await _unitOfWork.Save() > 0;
                await _unitOfWork.Commit();

                return isSuccess;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }

        }
    }
}