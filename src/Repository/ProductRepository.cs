using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public class ProductRepository
    {
        protected DbSet<Product> _products;
        protected DatabaseContext _databaseContext;

        public ProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _products = databaseContext.Set<Product>();
        }

        // add a new product:
        public async Task AddProductAsync(Product newProduct)
        {
            await _products.AddAsync(newProduct);
            await _databaseContext.SaveChangesAsync();
        }

        //get product by Id:
        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            return await _products.FindAsync(productId);
        }

        //delete a product
        public async Task<bool> DeleteProductAsync(Product product)
        {
            _products.Remove(product); //?
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        //edit on a product
        public async Task<bool> UpdateProductInfoAsync(Product product)
        {
            _products.Update(product);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
