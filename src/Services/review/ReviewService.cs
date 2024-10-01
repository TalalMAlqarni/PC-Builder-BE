using AutoMapper;
using src.DTO;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.ReviewDTO;

namespace src.Services.review
{
    public class ReviewService : IReviewService
    {
        protected readonly ReviewRepository _reviewRepo;
        protected readonly IMapper _mapper;
        public async Task<ReadReviewDto> CreateReviewAsync(ReviewDTO.CreateReviewDto createDto)
        {
            var review = _mapper.Map<Review>(createDto);
            var reviewCreated = await _reviewRepo.CreateReviewAsync(review);
            return _mapper.Map<Review, ReadReviewDto>(reviewCreated);
        }

        public async Task<bool> DeleteReviewAsync(Guid id)
        {
            var foundReview = await _reviewRepo.GetReviewByIdAsync(id);

            if (foundReview == null)
                throw CustomException.NotFound($"Review with ID {id} not found");

            bool isDeleted = await _reviewRepo.DeleteReviewAsync(foundReview);
            return isDeleted;

        }

        public async Task<List<ReadReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepo.GetAllReviewsAsync();
            return _mapper.Map<List<Review>, List<ReadReviewDto>>(reviews);
        }

        public async Task<ReadReviewDto> GetReviewByIdAsync(Guid id)
        {
            var foundReview = await _reviewRepo.GetReviewByIdAsync(id);

            if (foundReview == null)
                throw CustomException.NotFound($"Review with ID {id} not found");

            return _mapper.Map<Review, ReadReviewDto>(foundReview);
        }

        public async Task<ReadReviewDto> UpdateReviewAsync(Guid id, UpdateReviewDto updateDto)
        {
            var foundReview = await _reviewRepo.GetReviewByIdAsync(id);

            if (foundReview == null)
                throw CustomException.NotFound("Review not found");

            _mapper.Map(updateDto, foundReview);//! this was causing an error in cart need testing
            var reviewUpdated = await _reviewRepo.UpdateReviewAsync(foundReview);
            return _mapper.Map<Review, ReadReviewDto>(reviewUpdated);
        }
    }
}