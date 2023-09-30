using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Brand.Commands;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;
using SS_Microservice.Services.Products.Application.Interfaces.Repositories;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IUploadService uploadService, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _uploadService = uploadService;
            _mapper = mapper;
        }

        public async Task<bool> AddBrand(CreateBrandCommand command)
        {
            var brand = new Brand
            {
                Name = command.Name,
                Description = command.Description,
                Id = Guid.NewGuid().ToString(),
                Code = command.Code
            };

            return await _brandRepository.Insert(brand);
        }

        public async Task<bool> DeleteBrand(DeleteBrandCommand command)
        {
            var brand = await _brandRepository.GetById(command.Id)
                ?? throw new NotFoundException("Cannot find this brand");
            brand.IsDeleted = true;
            var isDeleteSuccess = _brandRepository.Update(brand); ;
            return isDeleteSuccess;
        }

        public async Task<PaginatedResult<BrandDto>> GetAllBrand(GetAllBrandQuery query)
        {
            var result = await _brandRepository.FilterBrand(query);
            var brandDtos = new List<BrandDto>();
            foreach (var item in result.Items)
            {
                brandDtos.Add(_mapper.Map<BrandDto>(item));
            }
            return new PaginatedResult<BrandDto>(brandDtos, (int)query.PageIndex, result.TotalPages, (int)query.PageSize);
        }

        public async Task<BrandDto> GetBrandById(GetBrandByIdQuery query)
        {
            var brand = await _brandRepository.GetById(query.Id)
                ?? throw new NotFoundException("Cannot find this brand");

            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<bool> UpdateBrand(UpdateBrandCommand command)
        {
            var brand = await _brandRepository.GetById(command.Id)
                ?? throw new NotFoundException("Cannot find this brand");

            brand.Name = command.Name;
            brand.Description = command.Description;
            brand.Code = command.Code;

            return _brandRepository.Update(brand);
        }
    }
}