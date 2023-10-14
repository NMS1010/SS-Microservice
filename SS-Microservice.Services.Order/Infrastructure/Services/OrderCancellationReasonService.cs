using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Services
{
    public class OrderCancellationReasonService : IOrderCancellationReasonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderCancellationReasonService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateOrderCancellationReason(CreateOrderCancellationReasonCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var orderCancellationReason = new OrderCancellationReason()
                {
                    Name = command.Name,
                    Note = command.Note
                };
                await _unitOfWork.Repository<OrderCancellationReason>().Insert(orderCancellationReason);
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot create order cancellation reason");
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

        public async Task<bool> DeleteOrderCancellationReason(DeleteOrderCancellationReasonCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var orderCancellationReason = await _unitOfWork.Repository<OrderCancellationReason>().GetById(command.Id);
                orderCancellationReason.Status = false;
                _unitOfWork.Repository<OrderCancellationReason>().Update(orderCancellationReason);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete this order cancellation reason");
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

        public async Task<OrderCancellationReasonDto> GetOrderCancellationReason(GetOrderCancellationReasonByIdQuery query)
        {
            var orderCancellationReason = await _unitOfWork.Repository<OrderCancellationReason>().GetById(query.Id);
            return _mapper.Map<OrderCancellationReasonDto>(orderCancellationReason);
        }

        public async Task<PaginatedResult<OrderCancellationReasonDto>> GetOrderCancellationReasonList(GetAllOrderCancellationReasonQuery query)
        {
            var spec = new OrderCancellationReasonSpecification(query, isPaging: true);

            var orderCancellationReasons = await _unitOfWork.Repository<OrderCancellationReason>().ListAsync(spec);

            var countSpec = new OrderCancellationReasonSpecification(query);
            var totalCount = await _unitOfWork.Repository<OrderCancellationReason>().CountAsync(countSpec);

            var orderCancellationReasonDto = new List<OrderCancellationReasonDto>();
            foreach (var item in orderCancellationReasons)
            {
                orderCancellationReasonDto.Add(_mapper.Map<OrderCancellationReasonDto>(item));
            }

            return new PaginatedResult<OrderCancellationReasonDto>(orderCancellationReasonDto, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }

        public async Task<bool> UpdateOrderCancellationReason(UpdateOrderCancellationReasonCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var orderCancellationReason = await _unitOfWork.Repository<OrderCancellationReason>().GetById(command.Id);

                orderCancellationReason.Note = command.Note;
                orderCancellationReason.Name = command.Name;
                orderCancellationReason.Status = command.Status;

                _unitOfWork.Repository<OrderCancellationReason>().Update(orderCancellationReason);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete this order cancellation reason");
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