using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Delivery.Commands;
using SS_Microservice.Services.Order.Application.Features.Delivery.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public DeliveryService(IUnitOfWork unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<bool> CreateDelivery(CreateDeliveryCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var delivery = new Delivery()
                {
                    Name = command.Name,
                    Description = command.Description,
                    Image = await _uploadService.UploadFile(command.Image),
                    Price = command.Price
                };

                await _unitOfWork.Repository<Delivery>().Insert(delivery);
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot create delivery");
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

        public async Task<bool> DeleteDelivery(DeleteDeliveryCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var delivery = await _unitOfWork.Repository<Delivery>().GetById(command.Id);
                delivery.Status = false;
                _unitOfWork.Repository<Delivery>().Update(delivery);
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete this delivery");
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

        public async Task<DeliveryDto> GetDelivery(GetDeliveryByIdQuery query)
        {
            var delivery = await _unitOfWork.Repository<Delivery>().GetById(query.Id);

            return _mapper.Map<DeliveryDto>(delivery);
        }

        public async Task<PaginatedResult<DeliveryDto>> GetDeliveryList(GetAllDeliveryQuery query)
        {
            var spec = new DeliverySpecification(query, isPaging: true);

            var deliveries = await _unitOfWork.Repository<Delivery>().ListAsync(spec);
            var deliveryDtos = new List<DeliveryDto>();
            deliveries.ForEach(x =>
            {
                deliveryDtos.Add(_mapper.Map<DeliveryDto>(x));
            });

            var countSpec = new DeliverySpecification(query);

            var totalCount = await _unitOfWork.Repository<Delivery>().CountAsync(countSpec);

            return new PaginatedResult<DeliveryDto>(deliveryDtos, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }

        public async Task<bool> UpdateDelivery(UpdateDeliveryCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var delivery = await _unitOfWork.Repository<Delivery>().GetById(command.Id);
                delivery.Status = command.Status;
                delivery.Price = command.Price;
                delivery.Description = command.Description;
                delivery.Name = command.Name;
                _unitOfWork.Repository<Delivery>().Update(delivery);
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot update this delivery");
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