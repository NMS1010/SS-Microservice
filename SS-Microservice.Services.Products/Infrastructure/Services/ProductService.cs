using AutoMapper;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
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
            var product = new Product();

            var now = DateTime.Now;

            product.Status = command.Status;
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Origin = command.Origin;
            product.Quantity = command.Quantity;
            product.Id = Guid.NewGuid().ToString();
            product.DateCreated = now;
            product.DateUpdated = now;
            product.Status = 1;
            if (command.Image != null)
            {
                product.MainImage = await _uploadService.UploadFile(command.Image);
            }
            if (command.SubImages != null)
            {
                foreach (var file in command.SubImages)
                {
                    if (file != null)
                    {
                        product.Images.Add(new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Path = await _uploadService.UploadFile(file)
                        });
                    }
                }
            }
            await _repository.Insert(product);
        }

        public async Task<bool> DeleteProduct(ProductDeleteCommand command)
        {
            var product = await _repository.GetById(command.ProductId);
            return _repository.Delete(product);
        }

        private static List<Core.Entities.Product> SortProduct(string column, bool isAscending, List<Core.Entities.Product> products)
        {
            var tempFunc = column switch
            {
                "Description" => new Func<Core.Entities.Product, object>(x => x.Description),
                "Price" => new Func<Core.Entities.Product, object>(x => x.Price),
                "Origin" => new Func<Core.Entities.Product, object>(x => x.Origin),
                "Quantity" => new Func<Core.Entities.Product, object>(x => x.Quantity),
                _ => new Func<Core.Entities.Product, object>(x => x.Name),
            };
            return (isAscending
                ? products.OrderBy(tempFunc)
                : products.OrderByDescending(tempFunc)).ToList();
        }

        public async Task<List<ProductDTO>> GetAllProduct(ProductGetAllQuery query)
        {
            var products = SortProduct(query.ColumnName, query.TypeSort == "ASC", (await _repository.GetAll()).ToList());
            if (query.Keyword != null)
            {
                products = products.Where(x =>
                    x.Name.ToLower().Contains(query.Keyword.ToString().ToLower())
                    || x.Description.ToLower().Contains(query.Keyword.ToString().ToLower())
                    || x.Quantity.ToString().Contains(query.Keyword.ToString())
                    || x.Price.ToString().Contains(query.Keyword.ToString())
                    || x.Origin.ToLower().Contains(query.Keyword.ToString().ToLower()))
                    .ToList();
            }
            var result = products
                .Skip((int)query.PageIndex - 1)
                .Take((int)query.PageSize).ToList();
            var productDtos = new List<ProductDTO>();
            foreach (var item in result)
            {
                productDtos.Add(_mapper.Map<Product, ProductDTO>(item));
            }
            return productDtos;
        }

        public async Task<ProductDTO> GetProductById(ProductGetByIdQuery query)
        {
            var product = await _repository.GetById(query.ProductId);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> UpdateProduct(ProductUpdateCommand command)
        {
            var product = await _repository.GetById(command.Id);
            if (product == null)
                throw new NotFoundException("Cannot find this product");

            product.Status = command.Status;
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Origin = command.Origin;
            product.Quantity = command.Quantity;
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