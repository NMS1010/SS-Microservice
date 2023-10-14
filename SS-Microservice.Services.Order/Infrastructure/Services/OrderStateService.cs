using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderState.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Services
{
    public class OrderStateService : IOrderStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderStateService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateOrderState(CreateOrderStateCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var orderState = new OrderState()
                {
                    HexColor = command.HexColor,
                    Name = command.Name,
                    Code = command.Code,
                    Order = command.Order,
                };
                await _unitOfWork.Repository<OrderState>().Insert(orderState);
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot create order state");
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

        public async Task<bool> DeleteOrderState(DeleteOrderStateCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var orderState = await _unitOfWork.Repository<OrderState>().GetById(command.OrderStateId);
                orderState.Status = false;
                _unitOfWork.Repository<OrderState>().Update(orderState);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete this order state");
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

        public async Task<OrderStateDto> GetOrderState(GetOrderStateByIdQuery query)
        {
            var orderState = await _unitOfWork.Repository<OrderState>().GetById(query.OrderStateId);
            return _mapper.Map<OrderStateDto>(orderState);
        }

        public async Task<PaginatedResult<OrderStateDto>> GetOrderStateList(GetAllOrderStateQuery query)
        {
            var spec = new OrderStateSpecification(query, isPaging: true);

            var orderStates = await _unitOfWork.Repository<OrderState>().ListAsync(spec);

            var countSpec = new OrderStateSpecification(query);
            var totalCount = await _unitOfWork.Repository<OrderState>().CountAsync(countSpec);

            var orderStateDto = new List<OrderStateDto>();
            foreach (var item in orderStates)
            {
                orderStateDto.Add(_mapper.Map<OrderStateDto>(item));
            }

            return new PaginatedResult<OrderStateDto>(orderStateDto, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }

        public async Task<bool> UpdateOrderState(UpdateOrderStateCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var orderState = await _unitOfWork.Repository<OrderState>().GetById(command.Id);
                orderState.Order = command.Order;
                orderState.Name = command.Name;
                orderState.Code = command.Code;
                orderState.HexColor = command.HexColor;
                orderState.Status = command.Status;
                _unitOfWork.Repository<OrderState>().Update(orderState);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete this order state");
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
    }
}