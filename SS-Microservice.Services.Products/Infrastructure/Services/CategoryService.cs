using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Common.StringUtil;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Interfaces.Repositories;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IUploadService uploadService, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _uploadService = uploadService;
            _mapper = mapper;
        }

        public async Task<bool> AddCategory(CreateCategoryCommand command)
        {
            var category = new Category
            {
                Name = command.Name,
                Description = command.Description,
                Id = Guid.NewGuid().ToString(),
                ParentId = command.ParentId,
                Slug = command.Name.Slugify()
            };
            if (command.Image != null)
            {
                category.Image = await _uploadService.UploadFile(command.Image);
            }
            return await _categoryRepository.Insert(category);
        }

        public async Task<bool> DeleteCategory(DeleteCategoryCommand command)
        {
            var category = await _categoryRepository.GetById(command.Id);
            category.IsDeleted = true;
            var isDeleteSuccess = _categoryRepository.Update(category); ;
            //var isDeleteSuccess = _categoryRepository.Delete(category);
            //if (isDeleteSuccess)
            //{
            //    await _uploadService.DeleteFile(category.Image);
            //}
            return isDeleteSuccess;
        }

        public async Task<PaginatedResult<CategoryDto>> GetAllCategory(GetAllCategoryQuery query)
        {
            var result = await _categoryRepository.FilterCategory(query);
            var categoryDtos = new List<CategoryDto>();
            foreach (var item in result.Items)
            {
                categoryDtos.Add(_mapper.Map<CategoryDto>(item));
            }
            return new PaginatedResult<CategoryDto>(categoryDtos, (int)query.PageIndex, result.TotalPages, (int)query.PageSize);
        }

        public async Task<CategoryDto> GetCategoryById(GetCategoryByIdQuery query)
        {
            var category = await _categoryRepository.GetById(query.Id)
                ?? throw new NotFoundException("Cannot find this category");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategory(UpdateCategoryCommand command)
        {
            var category = await _categoryRepository.GetById(command.Id)
                ?? throw new NotFoundException("Cannot find this category");

            category.Name = command.Name;
            category.Description = command.Description;
            category.ParentId = command.ParentId;
            category.Slug = command.Name.Slugify();
            var image = "";
            if (command.Image != null)
            {
                image = category.Image;
                category.Image = await _uploadService.UploadFile(command.Image);
            }
            var res = _categoryRepository.Update(category);

            if (res && !string.IsNullOrEmpty(image))
            {
                await _uploadService.DeleteFile(image);
            }
            return res;
        }
    }
}