﻿using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Commands;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Queries;

namespace SS_Microservice.Services.UserOperation.Application.Interfaces
{
    public interface IReviewService
    {
        Task<PaginatedResult<ReviewDto>> GetListReview(GetListReviewQuery query);

        Task<List<long>> CountReview(CountReviewQuery query);

        Task<List<ReviewDto>> GetTopReviewLatest(GetTopReviewLatestQuery query);

        Task<ReviewDto> GetReview(GetReviewQuery query);

        Task<ReviewDto> GetReviewByOrderItem(GetReviewByOrderItemQuery query);

        Task<long> CreateReview(CreateReviewCommand command);

        Task<bool> UpdateReview(UpdateReviewCommand command);

        Task<bool> ReplyReview(ReplyReviewCommand command);

        Task<bool> DeleteReview(DeleteReviewCommand command);

        Task<bool> ToggleReview(ToggleReviewCommand command);

        Task<bool> DeleteListReview(DeleteListReviewCommand command);
    }
}