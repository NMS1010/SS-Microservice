using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderState.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Interfaces.Repositories;

namespace SS_Microservice.Services.Order.Infrastructure.Services
{
    public class OrderStateService : IOrderStateService
    {
        private readonly IOrderStateRepository _orderStateRepository;
        private readonly IMapper _mapper;

        public OrderStateService(IOrderStateRepository orderStateRepository, IMapper mapper)
        {
            _orderStateRepository = orderStateRepository;
            _mapper = mapper;
        }

        public async Task CreateOrderState(CreateOrderStateCommand command)
        {
            var orderState = new Domain.Entities.OrderState()
            {
                HexColor = command.HexColor,
                OrderStateName = command.OrderStateName,
                Order = command.Order,
            };
            await _orderStateRepository.Insert(orderState);
        }

        public async Task<bool> DeleteOrderState(DeleteOrderStateCommand command)
        {
            var orderState = await _orderStateRepository.GetById(command.OrderStateId);

            return _orderStateRepository.Delete(orderState);
        }

        public async Task<OrderStateDto> GetOrderState(GetOrderStateByIdQuery query)
        {
            var orderState = await _orderStateRepository.GetById(query);
            return _mapper.Map<OrderStateDto>(orderState);
        }

        public async Task<PaginatedResult<OrderStateDto>> GetOrderStateList(GetAllOrderStateQuery query)
        {
            var res = await _orderStateRepository.GetOrderStateList(query);
            var orderStates = res.Items;
            var orderStateDto = new List<OrderStateDto>();
            foreach (var item in orderStates)
            {
                orderStateDto.Add(_mapper.Map<OrderStateDto>(item));
            }

            return new PaginatedResult<OrderStateDto>(orderStateDto, res.PageIndex, res.TotalCount, (int)query.PageSize);
        }

        public async Task<bool> UpdateOrderState(UpdateOrderStateCommand command)
        {
            var orderState = await _orderStateRepository.GetById(command.OrderStateId);

            orderState.Order = command.Order;
            orderState.OrderStateName = command.OrderStateName;
            orderState.HexColor = command.HexColor;

            return _orderStateRepository.Update(orderState);
        }
    }
}