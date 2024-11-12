using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;

namespace src.Repository
{
    public class SpecificationsRepository
    {
        protected DbSet<Specifications> _specifications;
        protected DbSet<Product> _products;
        protected DatabaseContext _databaseContext;

        public SpecificationsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _specifications = databaseContext.Set<Specifications>();
            _products = databaseContext.Set<Product>();
        }

        public async Task<Specifications> CreateSpecificationAsync(Specifications specification)
        {
            await _specifications.AddAsync(specification);
            await _databaseContext.SaveChangesAsync();
            return specification;
        }

        public async Task<Specifications?> GetSpecificationByIdAsync(Guid id)
        {
            return await _specifications.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Specifications>> GetAllSpecificationsAsync()
        {
            return await _specifications.ToListAsync();
        }

        public async Task<Specifications?> GetSpecificationByProductIdAsync(Guid id)
        {
            return await _specifications.FirstOrDefaultAsync(x => x.ProductId == id);

        }

        public async Task<bool> DeleteSpecificationAsync(Specifications specification)
        {
            _specifications.Remove(specification);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Specifications?> UpdateSpecificationAsync(Specifications specification)
        {
            _specifications.Update(specification);
            await _databaseContext.SaveChangesAsync();
            return specification;
        }
    }
}
