using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Features.ListCategory.Commands;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Specification.Category;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<PaginatedResult<CategoryDto>> GetListCategory(GetListCategoryQuery query)
        {
            var spec = new CategorySpecification(query, isPaging: true);
            var countSpec = new CategorySpecification(query);

            var categories = await _unitOfWork.Repository<Category>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Category>().CountAsync(countSpec);
            var categoryDtos = new List<CategoryDto>();
            foreach (var cate in categories)
            {
                var categoryDto = _mapper.Map<CategoryDto>(cate);
                if (cate.ParentId != null)
                {
                    var parentCategory = await _unitOfWork.Repository<Category>().GetById(cate.ParentId);
                    categoryDto.ParentName = parentCategory.Name;
                }
                categoryDtos.Add(categoryDto);
            }

            return new PaginatedResult<CategoryDto>(categoryDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<CategoryDto> GetCategory(GetCategoryQuery query)
        {
            var category = await _unitOfWork.Repository<Category>().GetById(query.Id)
                ?? throw new NotFoundException("Cannot find current product category");

            var categoryDto = _mapper.Map<CategoryDto>(category);
            if (category.ParentId != null)
            {
                var parentCategory = await _unitOfWork.Repository<Category>().GetById(category.ParentId)
                    ?? throw new NotFoundException("Cannot find current product category");

                categoryDto.ParentName = parentCategory.Name;
            }
            return categoryDto;
        }

        public async Task<CategoryDto> GetCategoryBySlug(GetCategoryBySlugQuery query)
        {
            var category = await _unitOfWork.Repository<Category>()
                .GetEntityWithSpec(new CategorySpecification(query.Slug))
                ?? throw new NotFoundException("Cannot find current product category");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<long> CreateCategory(CreateCategoryCommand command)
        {
            var category = _mapper.Map<Category>(command);
            category.Image = _uploadService.UploadFile(command.Image).Result;
            await _unitOfWork.Repository<Category>().Insert(category);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot create entity");
            }

            return category.Id;
        }

        public async Task<bool> UpdateCategory(UpdateCategoryCommand command)
        {
            var category = await _unitOfWork.Repository<Category>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current product category");

            category = _mapper.Map(command, category);
            category.Id = command.Id;

            if (command.Image != null)
            {
                category.Image = _uploadService.UploadFile(command.Image).Result;
            }

            _unitOfWork.Repository<Category>().Update(category);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteCategory(DeleteCategoryCommand command)
        {
            var category = await _unitOfWork.Repository<Category>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current product category");

            category.Status = false;
            _unitOfWork.Repository<Category>().Update(category);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update status of entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteListCategory(DeleteListCategoryCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var id in command.Ids)
                {
                    var category = await _unitOfWork.Repository<Category>().GetById(id)
                        ?? throw new NotFoundException("Cannot find current product category");

                    category.Status = false;
                    _unitOfWork.Repository<Category>().Update(category);
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