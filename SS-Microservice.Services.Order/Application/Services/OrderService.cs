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

namespace SS_Microservice.Services.Order.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        private async Task<Domain.Entities.Order> InitOrder(string userId, CreateOrderRequest request)
        {
            //var userAddress = user.Addresses.FirstOrDefault(x => x.IsDefault == true)
            //        ?? throw new NotFoundException("Cannot find default user's address");

            var delivery = await _unitOfWork.Repository<Delivery>().GetById(request.DeliveryId)
                ?? throw new InvalidRequestException("Unexpected deliveryId");

            var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(request.PaymentMethodId)
                ?? throw new InvalidRequestException("Unexpected paymentMethodId");

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in request.Items)
            {
                //var variant = await _unitOfWork.Repository<Variant>().GetById(item.VariantId)
                //    ?? throw new InvalidRequestException("Unexpected variantId");
                //var variantPrice = item.Quantity * variant.TotalPrice;
                var variantPrice = 0;

                totalAmount += variantPrice;
                orderItems.Add(new OrderItem()
                {
                    Quantity = item.Quantity,
                    VariantId = item.VariantId,
                    //UnitPrice = variant.TotalPrice,
                    UnitPrice = 0,
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
                UserId = "", // todo
                AddressId = 0, //todo
                DeliveryMethod = delivery.Name,
                Transaction = transaction,
                OrderItems = orderItems,
                Note = request.Note,
                ShippingCost = delivery.Price,
                Tax = (double)ORDER_TAX.TAX,
                Code = string.Empty.GenerateUniqueCode(),
                Status = ORDER_STATUS.NOT_PROCESSED,
                PaymentStatus = false,
                TotalAmount = totalAmount
            };

            return order;
        }

        //private async Task UpdateProduct(List<CreateOrderItemRequest> Items, Domain.Entities.Order order, string type)
        //{
        //    foreach (var item in Items)
        //    {
        //        var variant = await _unitOfWork.Repository<Variant>().GetEntityWithSpec(new VariantSpecification(item.VariantId))
        //            ?? throw new InvalidRequestException("Unexpected variantId");
        //        var product = variant.Product ?? throw new InvalidRequestException("Unexpected variantId");

        //        var q = item.Quantity * variant.Quantity;

        //        if (product.ActualInventory < q)
        //            throw new Exception("Unexpected quantity");

        //        var docket = new Docket()
        //        {
        //            Code = StringUtil.GenerateUniqueCode(),
        //            Product = product,
        //            Order = order,
        //            Type = type,
        //            Quantity = q,
        //        };

        //        await _unitOfWork.Repository<Docket>().Insert(docket);

        //        product.Quantity -= q;
        //        product.ActualInventory -= q;
        //        product.Sold += q;

        //        _unitOfWork.Repository<Product>().Update(product);
        //    }
        //}

        //private async Task UpdateUserCart(List<CreateOrderItemRequest> items, Cart cart)
        //{
        //    foreach (var item in items)
        //    {
        //        var cartItem = await _unitOfWork.Repository<CartItem>()
        //            .GetEntityWithSpec(new CartItemSpecification(cart.Id, item.VariantId))
        //            ?? throw new InvalidRequestException("Unexpected variantId");

        //        _unitOfWork.Repository<CartItem>().Delete(cartItem);
        //    }
        //}

        //private async Task SendOrderMail(AppUser user, Order order)
        //{
        //    var address = await _unitOfWork.Repository<Address>().GetEntityWithSpec(new AddressSpecification(user.Id, true));
        //    var orderDetail = new OrderConfirmationMail()
        //    {
        //        Email = address.Email,
        //        Receiver = address.Receiver,
        //        Phone = address.Phone,
        //        Address = $"{address.Street}, {address?.Ward?.Name}, {address?.District?.Name}, {address?.Province?.Name}",
        //        PaymentMethod = order.Transaction.PaymentMethod,
        //        TotalPrice = order.TotalAmount
        //    };

        //    var req = new CreateMailRequest()
        //    {
        //        Email = user.Email,
        //        Name = user.FirstName + " " + user.LastName,
        //        Type = MAIL_TYPE.ORDER_CONFIRMATION,
        //        Title = "Xác nhận đặt hàng",
        //        OrderConfirmationMail = orderDetail
        //    };

        //    _mailService.SendMail(req);
        //}

        //private async Task NotifyCreateOrder(AppUser user, Order order)
        //{
        //    var notiRequest = new CreateNotificationRequest()
        //    {
        //        UserId = user.Id,
        //        Image = order.OrderItems.FirstOrDefault()?.Variant.Product.Images.FirstOrDefault()?.Image,
        //        Title = "Đặt hàng thành công",
        //        Content = $"Đơn hàng #{order.Code} của bạn đã được hệ thống ghi nhận và đang được xử lý",
        //        Type = NOTIFICATION_TYPE.ORDER,
        //        Anchor = "/user/order/" + order.Code,
        //    };
        //    if (order.Transaction.PaymentMethod != PAYMENT_CODE.PAYPAL)
        //    {
        //        await SendOrderMail(user, order);
        //    }
        //    else
        //    {
        //        notiRequest.Title = "Đơn hàng cần thanh toán";
        //        notiRequest.Content = $"Đơn hàng #{order.Code} của bạn cần được thanh toán qua Paypal trước khi hệ thống có thể xử lý";
        //        notiRequest.Anchor = "/checkout/payment/" + order.Code;

        //        BackgroundJob.Schedule<IBackgroundJobService>(x => x.CancelOrder(order.Id), TimeSpan.FromMinutes(1));
        //    }

        //    await _notificationService.CreateOrderNotification(notiRequest);
        //}

        public async Task<string> CreateOrder(CreateOrderCommand command)
        {

            //var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(new UserSpecification(command.UserId))
            //    ?? throw new NotFoundException("Cannot find current user");

            var order = await InitOrder(command.UserId, command);

            try
            {
                await _unitOfWork.CreateTransaction();

                await _unitOfWork.Repository<Domain.Entities.Order>().Insert(order);

                //await UpdateProduct(command.Items, order);

                //await UpdateUserCart(command.Items, user.Cart);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot handle to create order, an error has occured");
                }

                await _unitOfWork.Commit();
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }

            try
            {
                //await NotifyCreateOrder(user, order);

                return order.Code;
            }
            catch
            {
                return order.Code;
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
                dto.Items = await GetOrderItemDto(listOrderItem);
                orderDtos.Add(dto);
            }

            return new PaginatedResult<OrderDto>(orderDtos, query.PageIndex, count, query.PageSize);
        }

        public Task<PaginatedResult<OrderDto>> GetListUserOrder(GetListUserOrderQuery request)
        {
            return GetListOrder(_mapper.Map<GetListOrderQuery>(request));
        }

        private async Task<List<OrderItemDto>> GetOrderItemDto(List<OrderItem> listOrderItem)
        {
            var listItems = new List<OrderItemDto>();
            foreach (var oi in listOrderItem)
            {
                //var variant = await _unitOfWork.Repository<Variant>().GetEntityWithSpec(new VariantSpecification(oi.Variant.Id))
                //?? throw new NotFoundException("Cannot find varaint item");

                //var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(new ProductSpecification(variant.Product.Id))
                //    ?? throw new NotFoundException("Cannot find product of variant item");

                var orderItemDto = _mapper.Map<OrderItemDto>(oi);
                //orderItemDto.ProductId = product.Id;
                //orderItemDto.VariantQuantity = variant.Quantity;
                //orderItemDto.VariantName = variant.Name;
                //orderItemDto.Sku = /*product.Code + "-" +*/ variant.Sku;
                //orderItemDto.ProductName = product.Name;
                //orderItemDto.ProductSlug = product.Slug;
                //orderItemDto.ProductUnit = product.Unit.Name;
                //orderItemDto.ProductImage = product.Images.FirstOrDefault(x => x.IsDefault)?.Image ?? product.Images.FirstOrDefault()?.Image;

                listItems.Add(orderItemDto);
            }
            return listItems;
        }

        public async Task<OrderDto> GetOrder(GetOrderQuery query)
        {
            var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(new OrderSpecification(query.Id))
                ?? throw new InvalidRequestException("Unexpected orderId");
            var listOrderItem = await _unitOfWork.Repository<OrderItem>().ListAsync(new OrderItemSpecification(order.Id));

            var listItems = await GetOrderItemDto(listOrderItem);

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = listItems;

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

                var orderItems = await _unitOfWork.Repository<OrderItem>().ListAsync(new OrderItemSpecification(order.Id));
                //await UpdateProduct(orderItems.Select(x => new CreateOrderItemRequest()
                //{
                //    Quantity = -1 * x.Quantity,
                //    VariantId = x.Variant.Id
                //}).ToList(), order, DOCKET_TYPE.IMPORT);

            }
            else
            {
                order.CancelReason = null;
                order.OtherCancelReason = null;
            }
        }

        //private async Task NotifyUpdateOrder(Order order)
        //{
        //    var orderItem = await _unitOfWork.Repository<OrderItem>().GetEntityWithSpec(new OrderItemSpecification(order.OrderItems.FirstOrDefault().Id, order.Status));

        //    await _notificationService.CreateOrderNotification(new CreateNotificationRequest()
        //    {
        //        UserId = order.User.Id,
        //        Image = orderItem?.Variant.Product.Images.FirstOrDefault()?.Image,
        //        Title = "Cập nhật đơn hàng",
        //        Content = $"Đơn hàng #{order.Code} của bạn đã chuyển sang trạng thái {ORDER_STATUS.OrderStatusSubTitle[order.Status]}",
        //        Type = NOTIFICATION_TYPE.ORDER,
        //        Anchor = "/user/order/" + order.Code,
        //    });
        //}

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

            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }

            try
            {

                //await NotifyUpdateOrder(order);

                return true;
            }
            catch
            {
                return true;
            }

        }

        public async Task<OrderDto> GetOrderByCode(GetOrderByCodeQuery query)
        {
            var order = await _unitOfWork.Repository<Domain.Entities.Order>().GetEntityWithSpec(new OrderSpecification(query.Code, query.UserId))
                ?? throw new InvalidRequestException("Unexpected order code");
            var listOrderItem = await _unitOfWork.Repository<OrderItem>().ListAsync(new OrderItemSpecification(order.Id));
            //var listReview = await _unitOfWork.Repository<Review>().ListAsync(new ReviewSpecification(order.Id));

            var listItems = await GetOrderItemDto(listOrderItem);

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = listItems;
            //orderDto.IsReview = listReview.Count == listItems.Count;
            //orderDto.ReviewedDate = listReview.Count == listItems.Count ? listReview.Max(x => x.CreatedAt) : null;

            return orderDto;
        }

        //private async Task NotifyCompletePaypalOrder(Domain.Entities.Order order)
        //{
        //    await SendOrderMail(order.User, order);

        //    var orderItems = await _unitOfWork.Repository<OrderItem>().ListAsync(new OrderItemSpecification(order.Id));

        //    await _notificationService.CreateOrderNotification(new CreateNotificationRequest()
        //    {
        //        UserId = order.User.Id,
        //        Image = orderItems.FirstOrDefault()?.Variant.Product.Images.FirstOrDefault(x => x.IsDefault)?.Image,
        //        Title = "Thanh toán thành công",
        //        Content = $"Đơn hàng #{order.Code} của bạn đã được thanh toán, hệ thống đã ghi nhận và đang được xử lý",
        //        Type = NOTIFICATION_TYPE.ORDER,
        //        Anchor = "/user/order/" + order.Code,
        //    });
        //}

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
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }

            try
            {
                //await NotifyCompletePaypalOrder(order);

                return true;
            }
            catch
            {
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