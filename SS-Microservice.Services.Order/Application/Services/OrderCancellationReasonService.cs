using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications.OrderCancellationReason;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Services
{
    public class OrderCancellationReasonService : IOrderCancellationReasonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderCancellationReasonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<long> CreateOrderCancellationReason(CreateOrderCancellationReasonCommand command)
        {
            var orderCancellationReason = _mapper.Map<OrderCancellationReason>(command);
            orderCancellationReason.Status = true;
            if (orderCancellationReason.Note == null)
            {
                orderCancellationReason.Note = string.Empty;
            }

            await _unitOfWork.Repository<OrderCancellationReason>().Insert(orderCancellationReason);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to create orderCancellationReason, an error has occured");
            }

            return orderCancellationReason.Id;
        }

        public async Task<bool> DeleteOrderCancellationReason(DeleteOrderCancellationReasonCommand command)
        {
            var orderCancellationReason = await _unitOfWork.Repository<OrderCancellationReason>().GetById(command.Id);

            orderCancellationReason.Status = false;

            _unitOfWork.Repository<OrderCancellationReason>().Update(orderCancellationReason);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to delete orderCancellationReason, an error has occured");
            }

            return true;
        }

        public async Task<bool> DeleteListOrderCancellationReason(DeleteListOrderCancellationReasonCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                foreach (var id in command.Ids)
                {
                    var orderCancellationReason = await _unitOfWork.Repository<OrderCancellationReason>().GetById(id);
                    orderCancellationReason.Status = false;

                    _unitOfWork.Repository<OrderCancellationReason>().Update(orderCancellationReason);
                }
                var isSuccess = await _unitOfWork.Save() > 0;
                await _unitOfWork.Commit();
                if (!isSuccess)
                {
                    throw new Exception("Cannot handle to delete list of orderCancellationReason, an error has occured");
                }

                return true;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<OrderCancellationReasonDto> GetOrderCancellationReason(GetOrderCancellationReasonQuery query)
        {
            var orderCancellationReason = await _unitOfWork.Repository<OrderCancellationReason>().GetById(query.Id);

            return _mapper.Map<OrderCancellationReasonDto>(orderCancellationReason);
        }

        public async Task<PaginatedResult<OrderCancellationReasonDto>> GetListOrderCancellationReason(GetListOrderCancellationReasonQuery query)
        {
            var orderCancellationReasons = await _unitOfWork.Repository<OrderCancellationReason>()
                .ListAsync(new OrderCancellationReasonSpecification(query, isPaging: true));

            var totalCount = await _unitOfWork.Repository<OrderCancellationReason>()
                .CountAsync(new OrderCancellationReasonSpecification(query));

            var orderCancellationReasonDtos = new List<OrderCancellationReasonDto>();
            orderCancellationReasons.ForEach(x => orderCancellationReasonDtos.Add(_mapper.Map<OrderCancellationReasonDto>(x)));

            return new PaginatedResult<OrderCancellationReasonDto>(orderCancellationReasonDtos, query.PageIndex,
                totalCount, query.PageSize);
        }

        public async Task<bool> UpdateOrderCancellationReason(UpdateOrderCancellationReasonCommand command)
        {
            var orderCancellationReason = await _unitOfWork.Repository<OrderCancellationReason>()
                .GetById(command.Id);

            _mapper.Map(command, orderCancellationReason);

            _unitOfWork.Repository<OrderCancellationReason>().Update(orderCancellationReason);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to update orderCancellationReason, an error has occured");
            }

            return true;
        }
    }
}