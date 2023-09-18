using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Interfaces.Repositories;
using SS_Microservice.Services.Order.Domain.Entities;
using System.Threading.Tasks;

namespace SS_Microservice.Services.Order.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<(bool, long)> CreateOrder(CreateOrderCommand command)
        {
            var orderItems = new List<OrderItem>();
            decimal totalItemPrice = 0;
            command.Items.ForEach(item =>
            {
                var totalPrice = item.Quantity * item.UnitPrice;
                totalItemPrice += totalPrice;
                orderItems.Add(new OrderItem()
                {
                    ProductName = item.ProductName,
                    ProductId = item.ProductId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalPrice = totalPrice,
                });
            });
            var order = new Domain.Entities.Order()
            {
                Address = command.Address,
                Email = command.Email,
                Name = command.Name,
                Phone = command.Phone,
                UserId = command.UserId,
                TotalItemPrice = totalItemPrice,
                TotalPrice = totalItemPrice,
                OrderStateId = command.OrderStateId,
                OrderItems = orderItems
            };

            var isSuccess = await _orderRepository.Insert(order);

            return (isSuccess, order.Id);
        }

        public async Task<bool> DeleteOrder(DeleteOrderCommand command)
        {
            var order = await _orderRepository.GetById(command.OrderId);
            return _orderRepository.Delete(order);
        }

        public async Task<OrderDto> GetOrder(GetOrderByIdQuery query)
        {
            var order = await _orderRepository.GetOrder(query);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<PaginatedResult<OrderDto>> GetOrderList(GetAllOrderQuery query)
        {
            var res = await _orderRepository.GetOrderList(query);
            var orders = res.Items;
            var orderDtos = new List<OrderDto>();
            orders.ForEach(item =>
            {
                orderDtos.Add(_mapper.Map<OrderDto>(item));
            });

            return new PaginatedResult<OrderDto>(orderDtos, res.PageIndex, res.TotalCount, (int)query.PageSize);
        }

        public async Task<bool> UpdateOrder(UpdateOrderCommand command)
        {
            var order = await _orderRepository.GetById(command.OrderId);
            order.OrderStateId = command.OrderStateId;

            return _orderRepository.Update(order);
        }
    }
}