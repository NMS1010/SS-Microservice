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
using SS_Microservice.Services.Inventory.Infrastructure.Consumers.Commands.OrderingSaga;
using SS_Microservice.Services.Inventory.Infrastructure.Consumers.Events.Order;

namespace SS_Microservice.Services.Inventory.Application.Services
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

        public async Task<bool> ExportInventory(ExportInventoryCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var item in command.Stocks)
                {
                    var docket = new Docket
                    {
                        Type = DOCKET_TYPE.EXPORT,
                        Code = string.Empty.GenerateUniqueCode(),
                        Quantity = item.Quantity,
                        ProductId = item.ProductId,
                        OrderId = command.OrderId
                    };

                    await _unitOfWork.Repository<Docket>().Insert(docket);
                }

                var res = await _unitOfWork.Save() > 0;

                await _unitOfWork.Commit();

                return res;
            }
            catch
            {
                await _unitOfWork.Rollback();
                return false;
            }
        }

        public async Task<List<DocketDto>> GetListDocketByProduct(GetListDocketQuery query)
        {
            var dockets = await _unitOfWork.Repository<Docket>().ListAsync(new DocketSpecification(query.ProductId));
            List<DocketDto> docketDtos = new();
            dockets.ForEach(x => docketDtos.Add(_mapper.Map<DocketDto>(x)));

            return docketDtos;
        }

        public async Task<long> ImportProduct(ImportProductCommand command)
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

                await _unitOfWork.Save();

                await _unitOfWork.Commit();

                return docket.Id;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task ImportInventory(ImportInventoryCommand command)
        {
            try
            {
                foreach (var item in command.Products)
                {
                    var docket = new Docket
                    {
                        Type = DOCKET_TYPE.IMPORT,
                        Code = string.Empty.GenerateUniqueCode(),
                        Quantity = item.Quantity,
                        ProductId = item.ProductId,
                        OrderId = command.OrderId,
                    };

                    await _unitOfWork.Repository<Docket>().Insert(docket);
                }

                await _unitOfWork.Save();
            }
            catch
            {

            }
        }

        public async Task RollBackInventory(RollBackInventoryCommand command)
        {
            var dockets = await _unitOfWork.Repository<Docket>().ListAsync(new DocketSpecification(command.OrderId, true));

            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var docket in dockets)
                {
                    _unitOfWork.Repository<Docket>().Delete(docket);
                }

                await _unitOfWork.Save();

                await _unitOfWork.Commit();
            }
            catch
            {

            }
        }

        public async Task<List<DocketDto>> GetListDocketByType(GetListDocketByTypeQuery query)
        {
            var dockets = await _unitOfWork.Repository<Docket>().ListAsync(new DocketSpecification(query.Type));
            List<DocketDto> docketDtos = new();
            dockets.ForEach(x => docketDtos.Add(_mapper.Map<DocketDto>(x)));

            return docketDtos;
        }

        public async Task<List<List<DocketDto>>> GetListDocketByDate(GetListDocketByDateQuery query)
        {
            var res = new List<List<DocketDto>>();
            foreach (var item in query.Items)
            {
                var dockets = await _unitOfWork.Repository<Docket>().ListAsync(new DocketSpecification(item.Type, item.FirstDate, item.LastDate));
                List<DocketDto> docketDtos = new();
                dockets.ForEach(x => docketDtos.Add(_mapper.Map<DocketDto>(x)));
                res.Add(docketDtos);
            }

            return res;
        }
    }
}
