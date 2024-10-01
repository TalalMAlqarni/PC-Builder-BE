using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using src.Database;
using src.Entity;
using src.Utils;

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
        public async Task<Product> AddProductAsync(Product newProduct)
        {
            await _products.AddAsync(newProduct);
            await _databaseContext.SaveChangesAsync();
            return newProduct;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _products.ToListAsync();
        }

        //get product by Id:
        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            return await _products.FindAsync(productId);
        }

        //delete a product
        public async Task<bool> DeleteProductAsync(Product product)
        {
            _products.Remove(product);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        //edit on a product
        public async Task<Product?> UpdateProductInfoAsync(Product product)
        {
            _products.Update(product);
            await _databaseContext.SaveChangesAsync();
            return product;
        }

        //get all with applying the pagination & search
        public async Task<List<Product>> GetAllResults(PaginationOptions paginationOptions)
        { // check the naming convention
            var result = _products.Where(x =>
                x.ProductName.ToLower().Contains(paginationOptions.Search.ToLower())
            );
            return await result
                .Skip(paginationOptions.Offset)
                .Take(paginationOptions.Limit)
                .ToListAsync();
        }

        //get all with using sort
        public async Task<List<Product>> GetAllByAscSortAsync(SortOptions sortOption)
        {
            var result = await _products.ToListAsync();
            if (sortOption.Equals(SortOption.PriceAsc))
            {
                result = await _products.OrderBy(x => x.ProductPrice).ToListAsync();
            }
            else if (sortOption.Equals(SortOption.SKUAsc))
            {
                result = await _products.OrderBy(x => x.SKU).ToListAsync();
            }

            return result;
        }

        public async Task<List<Product?>> GetAllByDescSortAsync(SortOptions sortOption)
        {
            return await _products.ToListAsync();
        }
        // public async Task<List<Product>> GetResultsBySortAsync (SortOptions sortOptions){

        //     var result = _products.Where(x => x.ProductPrice).
        // }
    }
}
