using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Brand.Commands;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Specification.Brand;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<PaginatedResult<BrandDto>> GetListBrand(GetListBrandQuery query)
        {
            var spec = new BrandSpecification(query, isPaging: true);
            var countSpec = new BrandSpecification(query);

            var brands = await _unitOfWork.Repository<Brand>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Brand>().CountAsync(countSpec);
            var brandDtos = new List<BrandDto>();
            brands.ForEach(x => brandDtos.Add(_mapper.Map<BrandDto>(x)));

            return new PaginatedResult<BrandDto>(brandDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<BrandDto> GetBrand(GetBrandQuery query)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetById(query.Id)
                 ?? throw new NotFoundException("Cannot find current brand");

            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<long> CreateBrand(CreateBrandCommand command)
        {
            var brand = _mapper.Map<Brand>(command);
            brand.Image = _uploadService.UploadFile(command.Image).Result;
            await _unitOfWork.Repository<Brand>().Insert(brand);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot create entity");
            }

            return brand.Id;
        }

        public async Task<bool> UpdateBrand(UpdateBrandCommand command)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current brand");

            brand = _mapper.Map(command, brand);
            brand.Id = command.Id;
            if (command.Image != null)
            {
                brand.Image = _uploadService.UploadFile(command.Image).Result;
            }

            _unitOfWork.Repository<Brand>().Update(brand);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteBrand(DeleteBrandCommand command)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current brand");

            brand.Status = false;
            _unitOfWork.Repository<Brand>().Update(brand);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update status of entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteListBrand(DeleteListBrandCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var id in command.Ids)
                {
                    var brand = await _unitOfWork.Repository<Brand>().GetById(id)
                        ?? throw new NotFoundException("Cannot find current brand");

                    brand.Status = false;
                    _unitOfWork.Repository<Brand>().Update(brand);
                }
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot update status of entities");
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