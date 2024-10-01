using Microsoft.AspNetCore.Mvc;
using src.Services.review;
using static src.DTO.ReviewDTO;
namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReviewController : ControllerBase
    {
        protected IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        // get all reviews: GET api/v1/review
        [HttpGet]
        public async Task<ActionResult<List<ReadReviewDto>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }
        // get review by id: GET api/v1/review/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadReviewDto>> GetReviewById(Guid id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            return Ok(review);
        }
        // create new review: POST api/v1/review
        [HttpPost]
        public async Task<ActionResult<ReadReviewDto>> CreateReview(CreateReviewDto createDto)
        {
            var review = await _reviewService.CreateReviewAsync(createDto);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }

        // update review: PUT api/v1/review/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadReviewDto>> UpdateReview(Guid id, UpdateReviewDto updateDto)
        {
            var review = await _reviewService.UpdateReviewAsync(id, updateDto);
            return Ok(review);
        }
        // delete review: DELETE api/v1/review/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteReview(Guid id)
        {
            var isDeleted = await _reviewService.DeleteReviewAsync(id);
            return Ok(isDeleted);
        }
    }
}