using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications.PaymentMethod;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public PaymentMethodService(IUnitOfWork unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<long> CreatePaymentMethod(CreatePaymentMethodCommand command)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(command);
            paymentMethod.Status = true;
            if (command.Image != null)
            {
                paymentMethod.Image = await _uploadService.UploadFile(command.Image);
            }
            await _unitOfWork.Repository<PaymentMethod>().Insert(paymentMethod);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to create payment method, an error has occured");
            }

            return paymentMethod.Id;
        }

        public async Task<bool> DeletePaymentMethod(DeletePaymentMethodCommand command)
        {
            var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(command.Id);

            paymentMethod.Status = false;

            _unitOfWork.Repository<PaymentMethod>().Update(paymentMethod);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to delete payment method, an error has occured");
            }

            return true;
        }

        public async Task<bool> DeleteListPaymentMethod(DeleteListPaymentMethodCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                foreach (var id in command.Ids)
                {
                    var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(id);
                    paymentMethod.Status = false;

                    _unitOfWork.Repository<PaymentMethod>().Update(paymentMethod);
                }
                var isSuccess = await _unitOfWork.Save() > 0;
                await _unitOfWork.Commit();
                if (!isSuccess)
                {
                    throw new Exception("Cannot handle to delete list of payment method, an error has occured");
                }

                return true;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<PaymentMethodDto> GetPaymentMethod(GetPaymentMethodQuery query)
        {
            var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(query.Id);

            return _mapper.Map<PaymentMethodDto>(paymentMethod);
        }

        public async Task<PaginatedResult<PaymentMethodDto>> GetListPaymentMethod(GetListPaymentMethodQuery query)
        {
            var paymentMethods = await _unitOfWork.Repository<PaymentMethod>().ListAsync(new PaymentMethodSpecification(query, isPaging: true));
            var totalCount = await _unitOfWork.Repository<PaymentMethod>().CountAsync(new PaymentMethodSpecification(query));

            var paymentMethodDtos = new List<PaymentMethodDto>();
            paymentMethods.ForEach(x => paymentMethodDtos.Add(_mapper.Map<PaymentMethodDto>(x)));

            return new PaginatedResult<PaymentMethodDto>(paymentMethodDtos, query.PageIndex, totalCount, query.PageSize);
        }

        public async Task<bool> UpdatePaymentMethod(UpdatePaymentMethodCommand command)
        {
            var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(command.Id);
            var image = paymentMethod.Image;
            var url = "";
            _mapper.Map(command, paymentMethod);
            paymentMethod.Image = image;

            if (command.Image != null)
            {
                url = paymentMethod.Image;
                paymentMethod.Image = await _uploadService.UploadFile(command.Image);
            }

            _unitOfWork.Repository<PaymentMethod>().Update(paymentMethod);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot handle to update payment method, an error has occured");
            }
            if (!string.IsNullOrEmpty(url))
            {
                _uploadService.DeleteFile(url);
            }

            return true;
        }
    }
}