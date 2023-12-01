using AutoMapper;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.StringUtil;
using SS_Microservice.Services.Inventory.Application.Common.Constants;
using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Commands;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Queries;
using SS_Microservice.Services.Inventory.Application.Interfaces;
using SS_Microservice.Services.Inventory.Application.Specifications.Docket;
using SS_Microservice.Services.Inventory.Domain.Entities;

namespace SS_Microservice.Services.Inventory.Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DocketDto>> GetListDocketByProduct(GetListDocketQuery query)
        {
            var dockets = await _unitOfWork.Repository<Docket>().ListAsync(new DocketSpecification(query.ProductId));
            List<DocketDto> docketDtos = new();
            dockets.ForEach(x => docketDtos.Add(_mapper.Map<DocketDto>(x)));

            return docketDtos;
        }

        public async Task<bool> ImportProduct(ImportProductCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                var docket = new Docket
                {
                    Type = DOCKET_TYPE.IMPORT,
                    Code = string.Empty.GenerateUniqueCode(),
                    Quantity = command.Quantity,
                    Note = command.Note,
                    ProductId = command.ProductId
                };

                await _unitOfWork.Repository<Docket>().Insert(docket);

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
