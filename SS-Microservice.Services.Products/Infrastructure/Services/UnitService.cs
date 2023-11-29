using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Unit.Command;
using SS_Microservice.Services.Products.Application.Features.Unit.Query;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Specification.Unit;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UnitDto>> GetListUnit(GetListUnitQuery query)
        {
            var spec = new UnitSpecification(query, isPaging: true);
            var countSpec = new UnitSpecification(query);
            var units = await _unitOfWork.Repository<Unit>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Unit>().CountAsync(countSpec);
            var unitDtos = new List<UnitDto>();
            units.ForEach(x => unitDtos.Add(_mapper.Map<UnitDto>(x)));

            return new PaginatedResult<UnitDto>(unitDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<UnitDto> GetUnit(GetUnitQuery query)
        {
            var unit = await _unitOfWork.Repository<Unit>().GetById(query.Id)
                ?? throw new NotFoundException("Cannot find current unit");

            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<long> CreateUnit(CreateUnitCommand command)
        {
            var unit = _mapper.Map<Unit>(command);
            await _unitOfWork.Repository<Unit>().Insert(unit);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot create entity");
            }

            return unit.Id;
        }

        public async Task<bool> UpdateUnit(UpdateUnitCommand command)
        {
            var unit = await _unitOfWork.Repository<Unit>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current unit");

            unit = _mapper.Map(command, unit);
            unit.Id = command.Id;

            _unitOfWork.Repository<Unit>().Update(unit);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteUnit(DeleteUnitCommand command)
        {
            var unit = await _unitOfWork.Repository<Unit>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current unit");

            unit.Status = false;
            _unitOfWork.Repository<Unit>().Update(unit);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update status of entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteListUnit(DeleteListUnitCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var id in command.Ids)
                {
                    var unit = await _unitOfWork.Repository<Unit>().GetById(id)
                        ?? throw new NotFoundException("Cannot find current unit");

                    unit.Status = false;
                    _unitOfWork.Repository<Unit>().Update(unit);
                }
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot update status of entities");
                }

                await _unitOfWork.Commit();

                return isSuccess;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}