using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Commands;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;
using SS_Microservice.Services.UserOperation.Application.Specifications.UserFollowProduct;
using SS_Microservice.Services.UserOperation.Domain.Entities;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.UserOperation.Application.Services
{
    public class UserFollowProductService : IUserFollowProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserFollowProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductDto>> GetListFollowProduct(GetFollowProductPagingRequest request)
        {
            var list = await _unitOfWork.Repository<UserFollowProduct>().ListAsync(new UserFollowProductSpecification(request, isPaging: true));
            var count = await _unitOfWork.Repository<UserFollowProduct>().CountAsync(new UserFollowProductSpecification(request));

            var productDtos = new List<ProductDto>();
            foreach (var item in list)
            {
                productDtos.Add(new ProductDto()
                {
                    Id = item.ProductId,
                });
            }

            return new PaginatedResult<ProductDto>(productDtos, request.PageIndex, count, request.PageSize);
        }

        public async Task<bool> FollowProduct(FollowProductCommand command)
        {

            var res = await _unitOfWork.Repository<UserFollowProduct>()
                .GetEntityWithSpec(new UserFollowProductSpecification(command.UserId, command.ProductId));

            if (res != null)
            {
                throw new InvalidRequestException("User has already followed this product");
            }

            UserFollowProduct userFollowProduct = new()
            {
                ProductId = command.ProductId,
                UserId = command.UserId
            };
            await _unitOfWork.Repository<UserFollowProduct>().Insert(userFollowProduct);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot save of entities");
            }

            return isSuccess;
        }

        public async Task<bool> UnFollowProduct(UnFollowProductCommand command)
        {
            var userFollowProduct = await _unitOfWork.Repository<UserFollowProduct>()
                .GetEntityWithSpec(new UserFollowProductSpecification(command.UserId, command.ProductId))
                ?? throw new NotFoundException("Cannot find this product in following list of user");

            _unitOfWork.Repository<UserFollowProduct>().Delete(userFollowProduct);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot save of entities");
            }

            return isSuccess;
        }
    }
}