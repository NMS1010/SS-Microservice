using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Commands;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Queries;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Specifications.Review;
using SS_Microservice.Services.UserOperation.Domain.Entities;

namespace SS_Microservice.Services.UserOperation.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<PaginatedResult<ReviewDto>> GetListReview(GetListReviewQuery query)
        {
            var spec = new ReviewSpecification(query, isPaging: true);
            var countSpec = new ReviewSpecification(query);

            var reviews = await _unitOfWork.Repository<Review>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Review>().CountAsync(countSpec);

            var reviewDtos = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                var dto = _mapper.Map<ReviewDto>(review);

                reviewDtos.Add(dto);
            }

            return new PaginatedResult<ReviewDto>(reviewDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<List<ReviewDto>> GetTopReviewLatest(GetTopReviewLatestQuery query)
        {
            var reviews = await _unitOfWork.Repository<Review>().ListAsync(new ReviewSpecification(query.Top));

            var reviewDtos = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                var dto = _mapper.Map<ReviewDto>(review);

                reviewDtos.Add(dto);
            }

            return reviewDtos;
        }

        public async Task<ReviewDto> GetReview(GetReviewQuery query)
        {
            var review = await _unitOfWork.Repository<Review>().GetEntityWithSpec(new ReviewSpecification(query.Id)) ??
            throw new InvalidRequestException("Unexpected reviewId");

            var dto = _mapper.Map<ReviewDto>(review);

            return dto;
        }

        public async Task<long> CreateReview(CreateReviewCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                var reviewTemp = await _unitOfWork.Repository<Review>()
                    .GetEntityWithSpec(new ReviewSpecification(command.OrderItemId, command.UserId));

                if (reviewTemp != null)
                    throw new InvalidRequestException("Unexpected orderItemId, this order item has been reviewed");

                var review = _mapper.Map<Review>(command);
                if (command.Image != null)
                    review.Image = await _uploadService.UploadFile(command.Image);

                //var product = await _unitOfWork.Repository<Product>().GetById(command.ProductId) ??
                //    throw new InvalidRequestException("Unexpected productId");
                //var user = await _unitOfWork.Repository<AppUser>().GetById(command.UserId) ??
                //    throw new NotFoundException("Cannot found current user");
                //var orderItem = await _unitOfWork.Repository<OrderItem>().GetEntityWithSpec(new OrderItemSpecification(command.OrderItemId, ORDER_STATUS.DELIVERED)) ??
                //    throw new InvalidRequestException("Unexpected orderItemId, order has not been deliveried");

                review.ProductId = command.ProductId;
                review.UserId = command.UserId;
                review.OrderItemId = command.OrderItemId;

                await _unitOfWork.Repository<Review>().Insert(review);
                await _unitOfWork.Save();

                //await CalculateProductReview(review.ProductId);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot create entity");
                }
                await _unitOfWork.Commit();

                return review.Id;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> ReplyReview(ReplyReviewCommand command)
        {
            var review = await _unitOfWork.Repository<Review>().GetById(command.Id);
            review.Reply = command.Reply;
            _unitOfWork.Repository<Review>().Update(review);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteReview(DeleteReviewCommand command)
        {
            try
            {
                var review = await _unitOfWork.Repository<Review>().GetEntityWithSpec(new ReviewSpecification(command.Id)) ??
                throw new InvalidRequestException("Unexpected reviewId");

                review.Status = false;

                _unitOfWork.Repository<Review>().Update(review);
                await _unitOfWork.Save();

                //await CalculateProductReview(review.ProductId);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot update status of entity");
                }

                return isSuccess;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteListReview(DeleteListReviewCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var id in command.Ids)
                {
                    var review = await _unitOfWork.Repository<Review>().GetEntityWithSpec(new ReviewSpecification(id)) ??
                        throw new InvalidRequestException("Unexpected reviewId");
                    review.Status = false;

                    _unitOfWork.Repository<Review>().Update(review);
                    await _unitOfWork.Save();

                    //await CalculateProductReview(review.ProductId);
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

        public async Task<bool> UpdateReview(UpdateReviewCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var review = await _unitOfWork.Repository<Review>().GetEntityWithSpec(new ReviewSpecification(command.UserId, command.Id))
                ?? throw new InvalidRequestException("Unexpected reviewId");

                review.Title = command.Title;
                review.Content = command.Content;
                review.Rating = command.Rating;
                if (command.Image != null)
                    review.Image = await _uploadService.UploadFile(command.Image);
                else if (command.IsDeleteImage)
                    review.Image = null;

                _unitOfWork.Repository<Review>().Update(review);
                await _unitOfWork.Save();

                //await CalculateProductReview(review.ProductId);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot update review");
                }
                await _unitOfWork.Commit();

                return true;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<ReviewDto> GetReviewByOrderItem(GetReviewByOrderItemQuery query)
        {
            var review = await _unitOfWork.Repository<Review>().GetEntityWithSpec(new ReviewSpecification(query.OrderItemId, query.UserId));
            if (review == null)
                return null;

            var res = _mapper.Map<ReviewDto>(review);
            res.ProductId = review.ProductId;

            return res;
        }

        public async Task<bool> ToggleReview(ToggleReviewCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var review = await _unitOfWork.Repository<Review>().GetEntityWithSpec(new ReviewSpecification(command.Id)) ??
                throw new InvalidRequestException("Unexpected reviewId");

                review.Status = !review.Status;
                _unitOfWork.Repository<Review>().Update(review);
                await _unitOfWork.Save();

                //await CalculateProductReview(review.Product);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot toggle status of entity");
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

        public async Task<List<long>> CountReview(CountReviewQuery query)
        {
            var listProductReviews = await _unitOfWork.Repository<Review>().ListAsync(new ReviewSpecification(query.ProductId, true));
            var listReviews = new List<long>()
            {
                listProductReviews.Count,
                listProductReviews.Count(x => x.Rating == 5),
                listProductReviews.Count(x => x.Rating == 4),
                listProductReviews.Count(x => x.Rating == 3),
                listProductReviews.Count(x => x.Rating == 2),
                listProductReviews.Count(x => x.Rating == 1)
            };

            return listReviews;
        }

        public async Task<ProductReviewDto> GetProductReview(long reviewId)
        {
            var review = await _unitOfWork.Repository<Review>().GetEntityWithSpec(new ReviewSpecification(reviewId)) ??
                throw new InvalidRequestException("Unexpected reviewId");

            var reviews = await _unitOfWork.Repository<Review>().ListAsync(new ReviewSpecification(review.ProductId, true));
            double temp = reviews.Count == 0 ? 0 : reviews.Average(x => x.Rating);
            var productRating = Math.Round(temp, 1);

            return new ProductReviewDto() { ProductId = review.ProductId, Rating = productRating };

        }
    }
}