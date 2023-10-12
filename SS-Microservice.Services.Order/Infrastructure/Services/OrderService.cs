using AutoMapper;
using SS_Microservice.Common.Constants;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Order.Application.Common;
using SS_Microservice.Services.Order.Application.Common.Constants;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications;
using SS_Microservice.Services.Order.Domain.Entities;
using System.Threading.Tasks;

namespace SS_Microservice.Services.Order.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<(bool, long)> CreateOrder(CreateOrderCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var orderItems = new List<OrderItem>();
                decimal totalItemPrice = 0;
                command.Items.ForEach(item =>
                {
                    var totalPrice = item.Quantity * item.UnitPrice;
                    totalItemPrice += totalPrice;
                    orderItems.Add(new OrderItem()
                    {
                        ProductName = item.ProductName,
                        VariantId = item.VariantId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        TotalPrice = totalPrice
                    });
                });
                var delivery = await _unitOfWork.Repository<Delivery>().GetById(command.DeliveryId)
                    ?? throw new Exception("Cannot found delivery to create order");
                var order = new Domain.Entities.Order()
                {
                    AddressId = command.AddressId,
                    DeliveryMethodType = delivery.Name,
                    Note = command.Note,
                    UserId = command.UserId,
                    TotalAmount = totalItemPrice,
                    OrderStateId = command.OrderStateId,
                    OrderItems = orderItems,
                    PaymentStatus = false,
                    ShippingCost = delivery.Price,
                    Code = UtilMethod.GenerateCode(),
                    Tax = (decimal)TAX.Percent * totalItemPrice
                };
                var now = DateTime.Now;
                order.Transaction = new Transaction()
                {
                    TotalPay = order.TotalAmount + order.ShippingCost + order.Tax,
                    PaymentMethodType = command.PaymentMethodType
                };

                if (command.PaymentMethodType.ToLower() != PAYMENT_METHOD.COD)
                {
                    order.PaymentStatus = true;
                }

                if (command.PaymentMethodType.ToLower() == PAYMENT_METHOD.PAYPAL)
                {
                    order.Transaction.PaypalOrderStatus = command.PaypalOrderStatus;
                    order.Transaction.PaypalOrderId = command.PaypalOrderId;
                    order.Transaction.PaidAt = now;
                }
                await _unitOfWork.Repository<Domain.Entities.Order>().Insert(order);
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot create order");
                }
                await _unitOfWork.Commit();
                return (isSuccess, order.Id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<bool> DeleteOrder(DeleteOrderCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var orderRepo = _unitOfWork.Repository<Domain.Entities.Order>();
                var orderitemRepo = _unitOfWork.Repository<Domain.Entities.OrderItem>();
                var transactionRepo = _unitOfWork.Repository<Transaction>();

                var order = await orderRepo.GetEntityWithSpec(new OrderSpecification(command.OrderId, _currentUserService.UserId));
                var transaction = await transactionRepo.GetEntityWithSpec(new TransactionSpecification(order.Id));
                foreach (var oi in order.OrderItems)
                {
                    orderitemRepo.Delete(oi);
                }
                transactionRepo.Delete(transaction);
                orderRepo.Delete(order);
                var res = await _unitOfWork.Save();
                var isSuccess = res > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot handle action");
                }
                await _unitOfWork.Commit();
                return isSuccess;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<OrderDto> GetOrder(GetOrderByIdQuery query)
        {
            var orderSpec = new OrderSpecification(query.OrderId, query.UserId);
            var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(orderSpec)
                ?? throw new NotFoundException("Cannot find this order");

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<PaginatedResult<OrderDto>> GetOrderList(GetAllOrderQuery query)
        {
            var orderSpec = new OrderSpecification(query, isPaging: true);
            var orders = await _unitOfWork.Repository<Domain.Entities.Order>().ListAsync(orderSpec);

            var countOrderSpec = new OrderSpecification(query);
            var count = await _unitOfWork.Repository<Domain.Entities.Order>().CountAsync(countOrderSpec);
            var orderDtos = new List<OrderDto>();
            orders.ForEach(item =>
            {
                orderDtos.Add(_mapper.Map<OrderDto>(item));
            });

            return new PaginatedResult<OrderDto>(orderDtos, (int)query.PageIndex, count, (int)query.PageSize);
        }

        public async Task<bool> UpdateOrder(UpdateOrderCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(new OrderSpecification(command.OrderId, command.UserId))
                    ?? throw new NotFoundException("Cannot update state for this order");
                var orderState = await _unitOfWork.Repository<Domain.Entities.OrderState>().GetById(command.OrderStateId)
                    ?? throw new NotFoundException("Cannot update state for this order");
                order.OrderStateId = orderState.Id;
                if (orderState.Code == ORDER_STATE.ORDER_CANCLED)
                {
                    if (!string.IsNullOrEmpty(command.OtherCancelReason))
                        order.OtherCancelReason = command.OtherCancelReason;
                    else
                        order.OrderCancellationReasonId = command.OrderCancellationReasonId;
                }
                else if (orderState.Code == ORDER_STATE.ORDER_COMPLETED)
                {
                    order.PaymentStatus = true;
                    order.Transaction.CompletedAt = DateTime.Now;
                    if (!order.PaymentStatus)
                        order.Transaction.PaidAt = DateTime.Now;
                }

                _unitOfWork.Repository<Domain.Entities.Order>().Update(order);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot update this order");
                }
                await _unitOfWork.Commit();
                return isSuccess;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }
    }
}