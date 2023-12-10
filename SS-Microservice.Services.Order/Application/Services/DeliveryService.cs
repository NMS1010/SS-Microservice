using AutoMapper;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Delivery.Commands;
using SS_Microservice.Services.Order.Application.Features.Delivery.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications.Delivery;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Services
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

        public async Task<long> CreateDelivery(CreateDeliveryCommand command)
        {
            var delivery = _mapper.Map<Delivery>(command);
            delivery.Status = true;
            if (command.Image != null)
            {
                delivery.Image = await _uploadService.UploadFile(command.Image);
            }
            await _unitOfWork.Repository<Delivery>().Insert(delivery);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to create delivery, an error has occured");
            }

            return delivery.Id;
        }

        public async Task<bool> DeleteDelivery(DeleteDeliveryCommand command)
        {
            var delivery = await _unitOfWork.Repository<Delivery>().GetById(command.Id);

            delivery.Status = false;

            _unitOfWork.Repository<Delivery>().Update(delivery);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to delete delivery, an error has occured");
            }

            return true;
        }

        public async Task<bool> DeleteListDelivery(DeleteListDeliveryCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                foreach (var id in command.Ids)
                {
                    var delivery = await _unitOfWork.Repository<Delivery>().GetById(id);
                    delivery.Status = false;

                    _unitOfWork.Repository<Delivery>().Update(delivery);
                }
                var isSuccess = await _unitOfWork.Save() > 0;
                await _unitOfWork.Commit();
                if (!isSuccess)
                {
                    throw new Exception("Cannot handle to delete list of deliveries, an error has occured");
                }

                return true;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<DeliveryDto> GetDelivery(GetDeliveryQuery query)
        {
            var delivery = await _unitOfWork.Repository<Delivery>().GetById(query.Id);

            return _mapper.Map<DeliveryDto>(delivery);
        }

        public async Task<PaginatedResult<DeliveryDto>> GetListDelivery(GetListDeliveryQuery query)
        {
            var deliveries = await _unitOfWork.Repository<Delivery>().ListAsync(new DeliverySpecification(query, isPaging: true));
            var totalCount = await _unitOfWork.Repository<Delivery>().CountAsync(new DeliverySpecification(query));
            var deliveryDtos = new List<DeliveryDto>();
            deliveries.ForEach(x => deliveryDtos.Add(_mapper.Map<DeliveryDto>(x)));

            return new PaginatedResult<DeliveryDto>(deliveryDtos, query.PageIndex, totalCount, query.PageSize);
        }

        public async Task<bool> UpdateDelivery(UpdateDeliveryCommand command)
        {
            var delivery = await _unitOfWork.Repository<Delivery>().GetById(command.Id);
            var image = delivery.Image;
            var url = "";
            _mapper.Map(command, delivery);
            delivery.Image = image;
            if (command.Image != null)
            {
                url = delivery.Image;
                delivery.Image = await _uploadService.UploadFile(command.Image);
            }

            _unitOfWork.Repository<Delivery>().Update(delivery);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to update delivery, an error has occured");
            }
            if (!string.IsNullOrEmpty(url))
            {
                _uploadService.DeleteFile(url);
            }

            return true;
        }
    }
}