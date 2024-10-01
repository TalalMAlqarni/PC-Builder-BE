using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;
namespace src.Repository
{
    public class ReviewRepository
    {
        protected DbSet<Review> _review;
        protected DatabaseContext _databaseContext;

        public ReviewRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _review = databaseContext.Set<Review>();
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            await _review.AddAsync(review);
            await _databaseContext.SaveChangesAsync();
            return review;
        }


        public async Task<Review?> GetReviewByIdAsync(Guid id)
        {
            return await _review.FirstOrDefaultAsync(x => x.ReviewId == id);
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _review.ToListAsync();
        }


        public async Task<bool> DeleteReviewAsync(Review review)
        {
            _review.Remove(review);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Review?> UpdateReviewAsync(Review review)
        {
            _review.Update(review);
            await _databaseContext.SaveChangesAsync();
            return review;
        }


    }
}