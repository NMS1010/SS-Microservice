using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public PaymentMethodService(IMapper mapper, IUnitOfWork unitOfWork, IUploadService uploadService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
        }

        public async Task<bool> CreatePaymentMethod(CreatePaymentMethodCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var paymentMethod = new PaymentMethod()
                {
                    Name = command.Name,
                    Code = command.Code,
                    Status = 1,
                    Image = await _uploadService.UploadFile(command.Image),
                };
                await _unitOfWork.Repository<PaymentMethod>().Insert(paymentMethod);
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot create payment method");
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

        public async Task<bool> DeletePaymentMethod(DeletePaymentMethodCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(command.Id);
                paymentMethod.Status = 0;
                _unitOfWork.Repository<PaymentMethod>().Update(paymentMethod);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete this payment method");
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

        public async Task<PaymentMethodDto> GetPaymentMethod(GetPaymentMethodByIdQuery query)
        {
            var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(query.Id);
            return _mapper.Map<PaymentMethodDto>(paymentMethod);
        }

        public async Task<PaginatedResult<PaymentMethodDto>> GetPaymentMethodList(GetAllPaymentMethodQuery query)
        {
            var spec = new PaymentMethodSpecification(query, isPaging: true);

            var paymentMethods = await _unitOfWork.Repository<PaymentMethod>().ListAsync(spec);

            var countSpec = new PaymentMethodSpecification(query);
            var totalCount = await _unitOfWork.Repository<PaymentMethod>().CountAsync(countSpec);

            var paymentMethodDto = new List<PaymentMethodDto>();
            foreach (var item in paymentMethods)
            {
                paymentMethodDto.Add(_mapper.Map<PaymentMethodDto>(item));
            }

            return new PaginatedResult<PaymentMethodDto>(paymentMethodDto, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }

        public async Task<bool> UpdatePaymentMethod(UpdatePaymentMethodCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(command.Id);

                paymentMethod.Code = command.Code;
                paymentMethod.Name = command.Name;
                paymentMethod.Status = command.Status;
                var image = "";
                if (command.Image != null)
                {
                    image = paymentMethod.Image;
                    paymentMethod.Image = await _uploadService.UploadFile(command.Image);
                }
                _unitOfWork.Repository<PaymentMethod>().Update(paymentMethod);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete this payment method");
                }
                await _unitOfWork.Commit();
                if (!string.IsNullOrEmpty(image))
                {
                    await _uploadService.DeleteFile(image);
                }
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