using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Products.Application.Common.Enums;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Sale.Commands;
using SS_Microservice.Services.Products.Application.Features.Sale.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Specification.Product;
using SS_Microservice.Services.Products.Application.Specification.Sale;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        //private readonly INotificationService _notificationService;

        public SaleService(IUnitOfWork unitOfWork, IMapper mapper, IUploadService uploadService/*, INotificationService notificationService*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            //_notificationService = notificationService;
        }

        public async Task<PaginatedResult<SaleDto>> GetListSale(GetListSaleQuery query)
        {
            var spec = new SaleSpecification(query, isPaging: true);
            var countSpec = new SaleSpecification(query);
            var sales = await _unitOfWork.Repository<Sale>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Sale>().CountAsync(countSpec);

            var saleDtos = new List<SaleDto>();
            foreach (var sale in sales)
            {
                HashSet<Category> categories = new();
                var categoryDtos = new List<CategoryDto>();
                if (!sale.All)
                {
                    foreach (var p in sale.Products)
                    {
                        Product product =
                            await _unitOfWork.Repository<Product>().GetEntityWithSpec(new ProductSpecification(p.Id));
                        categories.Add(product.Category);
                    }
                    foreach (var category in categories)
                    {
                        categoryDtos.Add(_mapper.Map<CategoryDto>(category));
                    }
                }
                var saleDto = _mapper.Map<SaleDto>(sale);
                saleDto.ProductCategories = categoryDtos;
                saleDtos.Add(saleDto);
            }

            return new PaginatedResult<SaleDto>(saleDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<SaleDto> GetSale(GetSaleQuery query)
        {
            var sale = await _unitOfWork.Repository<Sale>().GetEntityWithSpec(new SaleSpecification(query.Id));
            HashSet<Category> categories = new();
            var categoryDtos = new List<CategoryDto>();
            if (!sale.All)
            {
                foreach (var p in sale.Products)
                {
                    Product product =
                        await _unitOfWork.Repository<Product>().GetEntityWithSpec(new ProductSpecification(p.Id));
                    categories.Add(product.Category);
                }
                foreach (var category in categories)
                {
                    categoryDtos.Add(_mapper.Map<CategoryDto>(category));
                }
            }
            var saleDto = _mapper.Map<SaleDto>(sale);
            saleDto.ProductCategories = categoryDtos;

            return saleDto;
        }

        public async Task<SaleDto> GetSaleLatest(GetLatestSaleQuery query)
        {
            var sale = await _unitOfWork.Repository<Sale>().GetEntityWithSpec(new SaleSpecification(true));
            return _mapper.Map<SaleDto>(sale);
        }

        public async Task<long> CreateSale(CreateSaleCommand command)
        {
            var sale = _mapper.Map<Sale>(command);
            sale.Image = _uploadService.UploadFile(command.Image).Result;
            sale.Status = SALE_STATUS.INACTIVE;

            var products = new List<Product>();
            if (command.All)
            {
                products = await _unitOfWork.Repository<Product>().ListAsync(new ProductSpecification());
            }
            else
            {
                foreach (long categoryId in command.CategoryIds)
                {
                    var listProduct = await _unitOfWork.Repository<Product>().ListAsync(new ProductSpecification(categoryId, true));
                    listProduct.ForEach(p => products.Add(p));
                }
            }

            sale.Products = products;

            await _unitOfWork.Repository<Sale>().Insert(sale);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot create entity");
            }

            return sale.Id;
        }

        public async Task<bool> UpdateSale(UpdateSaleCommand command)
        {
            var sale = await _unitOfWork.Repository<Sale>().GetById(command.Id);
            sale = _mapper.Map(command, sale);
            sale.Id = command.Id;

            if (command.Image != null)
            {
                sale.Image = _uploadService.UploadFile(command.Image).Result;
            }

            var products = new List<Product>();
            if (command.All)
            {
                products = await _unitOfWork.Repository<Product>().ListAsync(new ProductSpecification());
            }
            else
            {
                var listAllProduct = await _unitOfWork.Repository<Product>()
                    .ListAsync(new ProductSpecification(command.Id, true, true));

                foreach (long categoryId in command.CategoryIds)
                {
                    var listProduct = await _unitOfWork.Repository<Product>()
                        .ListAsync(new ProductSpecification(categoryId, true));
                    listProduct.ForEach(p => products.Add(p));
                }

                foreach (var product in listAllProduct.Except(products).ToArray())
                {
                    product.Sale = null;
                    _unitOfWork.Repository<Product>().Update(product);
                }
            }

            sale.Products = products;

            _unitOfWork.Repository<Sale>().Update(sale);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteSale(DeleteSaleCommand command)
        {
            var sale = await _unitOfWork.Repository<Sale>().GetById(command.Id);
            sale.Status = SALE_STATUS.INACTIVE;
            _unitOfWork.Repository<Sale>().Update(sale);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update status of entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteListSale(DeleteListSaleCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var id in command.Ids)
                {
                    var sale = await _unitOfWork.Repository<Sale>().GetById(id);
                    sale.Status = SALE_STATUS.INACTIVE;
                    _unitOfWork.Repository<Sale>().Update(sale);
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

        public async Task<bool> ApplySale(ApplySaleCommand command)
        {
            var sale = await _unitOfWork.Repository<Sale>().GetEntityWithSpec(new SaleSpecification(command.Id))
                ?? throw new NotFoundException("Cannot find current sale");

            if (sale.Status == SALE_STATUS.ACTIVE)
            {
                throw new InvalidRequestException("Sale has applied");
            }

            if (DateTime.Compare(sale.StartDate, DateTime.Now) > 0)
            {
                throw new InvalidRequestException("Sale date is not yet");
            }

            var products = sale.Products;
            if (products.Count == 0)
                throw new Exception("Cannot find any product applied this sale");

            foreach (var p in products)
            {
                Product product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(new ProductSpecification(p.Id));
                foreach (var variant in product.Variants)
                {
                    variant.PromotionalItemPrice = variant.ItemPrice - variant.ItemPrice * (decimal)sale.PromotionalPercent / 100;
                    variant.TotalPromotionalPrice = variant.PromotionalItemPrice * variant.Quantity;
                }
                _unitOfWork.Repository<Product>().Update(product);
            }

            sale.Status = SALE_STATUS.ACTIVE;
            _unitOfWork.Repository<Sale>().Update(sale);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update status of entities");
            }
            //await _notificationService.CreateSaleNotification(new Application.Model.Notification.CreateNotificationRequest()
            //{
            //	Content = $"Đợt khuyến mãi hiện đang có mặt tại cửa hàng, giảm giá lên tới {sale.PromotionalPercent}%",
            //	Type = NOTIFICATION_TYPE.SALE,
            //	Image = sale.Image,
            //	Status = false,
            //	Title = $"{sale.Name}",
            //	Anchor = "#"
            //});
            return isSuccess;
        }

        public async Task<bool> CancelSale(CancelSaleCommand command)
        {
            var sale = await _unitOfWork.Repository<Sale>().GetEntityWithSpec(new SaleSpecification(command.Id))
                ?? throw new NotFoundException("Cannot find current sale");

            if (sale.Status == SALE_STATUS.INACTIVE || sale.Status == SALE_STATUS.EXPIRED)
            {
                throw new InvalidRequestException("Sale has not apply");
            }

            var products = sale.Products;
            if (products.Count == 0) return true;

            foreach (var p in products)
            {
                Product product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(new ProductSpecification(p.Id));
                product.Sale = null;
                foreach (var variant in product.Variants)
                {
                    variant.PromotionalItemPrice = null;
                    variant.TotalPromotionalPrice = null;
                }
                _unitOfWork.Repository<Product>().Update(product);
            }

            sale.Status = SALE_STATUS.INACTIVE;
            _unitOfWork.Repository<Sale>().Update(sale);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update status of entities");
            }

            return isSuccess;
        }
    }
}