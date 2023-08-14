using AutoMapper;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Application.Product.Commands;
using SS_Microservice.Services.Products.Application.Product.Queries;
using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper, IUploadService uploadService)
        {
            _repository = repository;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task AddProduct(ProductCreateCommand command)
        {
            var product = _mapper.Map<Core.Entities.Product>(command);

            var now = DateTime.Now;

            product.Id = Guid.NewGuid();
            product.DateCreated = now;
            product.DateUpdated = now;
            product.Status = 1;
            if (command.Image != null)
            {
                product.MainImage = await _uploadService.UploadFile(command.Image);
            }
            foreach (var file in command.SubImages)
            {
                if (file != null)
                {
                    product.Images.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        Path = await _uploadService.UploadFile(file)
                    });
                }
            }
            await _repository.Insert(product);
        }

        public async Task<bool> DeleteProduct(ProductDeleteCommand command)
        {
            var product = await _repository.GetById(command.ProductId);
            return _repository.Delete(product);
        }

        private static IEnumerable<Core.Entities.Product> SortProduct(string column, bool isAscending, IEnumerable<Core.Entities.Product> products)
        {
            var tempFunc = column switch
            {
                "Description" => new Func<Core.Entities.Product, object>(x => x.Description),
                "Price" => new Func<Core.Entities.Product, object>(x => x.Price),
                "Origin" => new Func<Core.Entities.Product, object>(x => x.Origin),
                "Quantity" => new Func<Core.Entities.Product, object>(x => x.Quantity),
                _ => new Func<Core.Entities.Product, object>(x => x.Name),
            };
            return isAscending
                ? products.OrderBy(tempFunc)
                : products.OrderByDescending(tempFunc);
        }

        public async Task<List<ProductDTO>> GetAllProduct(ProductGetAllQuery query)
        {
            var products = SortProduct(query.ColumnName, query.TypeSort == "ASC", await _repository.GetAll());
            if (query.Keyword != null)
            {
                products = products.Where(x =>
                    x.Name.ToLower().Contains(query.Keyword.ToString().ToLower())
                    || x.Description.ToLower().Contains(query.Keyword.ToString().ToLower())
                    || x.Quantity.ToString().Contains(query.Keyword.ToString())
                    || x.Price.ToString().Contains(query.Keyword.ToString())
                    || x.Origin.ToLower().Contains(query.Keyword.ToString().ToLower()));
            }
            var result = products
                .Skip((int)query.PageIndex - 1)
                .Take((int)query.PageSize).ToList();

            return _mapper.Map<List<Core.Entities.Product>, List<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetProductById(ProductGetByIdQuery query)
        {
            var product = await _repository.GetById(query.ProductId);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> UpdateProduct(ProductUpdateCommand command)
        {
            var product = _mapper.Map<Product>(command);
            product.DateUpdated = DateTime.Now;
            if (command.Image != null)
            {
                product.MainImage = await _uploadService.UploadFile(command.Image);
            }
            foreach (var subimg in command.SubImages)
            {
                if (subimg.Image != null)
                {
                    var prdImg = product.Images.Where(x => x.Id == subimg.Id).FirstOrDefault();
                    if (prdImg != null)
                    {
                        prdImg.Path = await _uploadService.UploadFile(subimg.Image);
                    }
                }
            }
            return _repository.Update(product);
        }
    }
}