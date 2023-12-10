using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Common.Enums;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Variant.Commands;
using SS_Microservice.Services.Products.Application.Features.Variant.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Specification.Variant;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Services
{
    public class VariantService : IVariantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VariantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<VariantDto>> GetListVariant(GetListVariantQuery query)
        {
            var spec = new VariantSpecification(query, isPaging: true);
            var countSpec = new VariantSpecification(query);
            var variants = await _unitOfWork.Repository<Variant>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Variant>().CountAsync(countSpec);
            var variantDtos = new List<VariantDto>();
            variants.ForEach(variant =>
            {
                var variantDto = _mapper.Map<VariantDto>(variant);
                variantDtos.Add(variantDto);
            });

            return new PaginatedResult<VariantDto>(variantDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<List<VariantDto>> GetListVariantByProductId(GetListVariantByProductQuery query)
        {
            var variants = await _unitOfWork.Repository<Variant>().ListAsync(new VariantSpecification(query.ProductId, true));
            var variantDtos = new List<VariantDto>();
            variants.ForEach(variant => variantDtos.Add(_mapper.Map<VariantDto>(variant)));

            return variantDtos;
        }

        public async Task<VariantDto> GetVariant(GetVariantQuery query)
        {
            var spec = new VariantSpecification(query.Id);
            var variant = await _unitOfWork.Repository<Variant>().GetEntityWithSpec(spec)
                ?? throw new NotFoundException("Cannot find current variant");

            var variantDto = _mapper.Map<VariantDto>(variant);

            return variantDto;
        }

        public async Task<long> CreateVariant(CreateVariantCommand command)
        {
            var variant = _mapper.Map<Variant>(command);
            variant.Product = await _unitOfWork.Repository<Product>().GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find current product");

            variant.Status = VARIANT_STATUS.ACTIVE;
            await _unitOfWork.Repository<Variant>().Insert(variant);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot create entity");
            }

            return variant.Id;
        }

        public async Task<bool> UpdateVariant(UpdateVariantCommand command)
        {
            var variant = await _unitOfWork.Repository<Variant>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current variant");

            variant = _mapper.Map(command, variant);
            variant.Id = command.Id;
            variant.Status = variant.Status switch
            {
                VARIANT_STATUS.ACTIVE => VARIANT_STATUS.ACTIVE,
                VARIANT_STATUS.INACTIVE => VARIANT_STATUS.INACTIVE,
                _ => throw new InvalidRequestException("Unexpected variant status: " + command.Status),
            };
            _unitOfWork.Repository<Variant>().Update(variant);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteVariant(DeleteVariantCommand command)
        {
            var variant = await _unitOfWork.Repository<Variant>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current variant");

            variant.Status = VARIANT_STATUS.INACTIVE;
            _unitOfWork.Repository<Variant>().Update(variant);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update status of entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteListVariant(DeleteListVariantCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var id in command.Ids)
                {
                    var variant = await _unitOfWork.Repository<Variant>().GetById(id)
                        ?? throw new NotFoundException("Cannot find current variant");

                    variant.Status = VARIANT_STATUS.INACTIVE;
                    _unitOfWork.Repository<Variant>().Update(variant);
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