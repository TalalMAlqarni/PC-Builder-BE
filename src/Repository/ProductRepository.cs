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

        public async Task<List<Product>> GetAllBySortAsync(SortOptions sortOption)
        {
            IQueryable<Product> query = _products;

            if (!string.IsNullOrEmpty(sortOption.SortBy))
            {
                if (sortOption.SortBy.Equals("price", StringComparison.OrdinalIgnoreCase))
                {
                    query =
                        sortOption.SortOrder == SortOrder.Descending
                            ? query.OrderByDescending(x => x.ProductPrice)
                            : query.OrderBy(x => x.ProductPrice);
                }
                else if (sortOption.SortBy.Equals("sku", StringComparison.OrdinalIgnoreCase))
                {
                    query =
                        sortOption.SortOrder == SortOrder.Descending
                            ? query.OrderByDescending(x => x.SKU)
                            : query.OrderBy(x => x.SKU);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetAllByFilteringAsync(FilterationOptions criteria)
        {
            IQueryable<Product> query = _products;
            // var result = await _products.ToListAsync();
            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(x => x.ProductName.ToLower() == criteria.Name.ToLower());
                // result = result.Where(x => x.ProductColor.ToLower() == criteria.Color.ToLower());
            }

            if (!string.IsNullOrEmpty(criteria.Color))
            {
                query = query.Where(x => x.ProductColor.ToLower() == criteria.Color.ToLower());
            }

            if (criteria.MinPrice.HasValue)
            {
                query = query.Where(x => x.ProductPrice >= criteria.MinPrice.Value);
            }

            if (criteria.MaxPrice.HasValue)
            {
                query = query.Where(x => x.ProductPrice <= criteria.MaxPrice.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetAllAsync(SearchProcess to_search)
        {
            //implement search



            var search_result = _products.Where(x =>
                x.ProductName.ToLower().Contains(to_search.Search.ToLower())
            );

            //implement filter
            IQueryable<Product> query = search_result;

            if (!string.IsNullOrEmpty(to_search.Name))
            {
                query = query.Where(x => x.ProductName.ToLower() == to_search.Name.ToLower());
            }

            if (!string.IsNullOrEmpty(to_search.Color))
            {
                query = query.Where(x => x.ProductColor.ToLower() == to_search.Color.ToLower());
            }

            if (to_search.MinPrice.HasValue)
            {
                query = query.Where(x => x.ProductPrice >= to_search.MinPrice.Value);
            }

            if (to_search.MaxPrice.HasValue)
            {
                query = query.Where(x => x.ProductPrice <= to_search.MaxPrice.Value);
            }

            //implement sort
            if (!string.IsNullOrEmpty(to_search.SortBy))
            {
                if (to_search.SortBy.Equals("price", StringComparison.OrdinalIgnoreCase))
                {
                    query =
                        to_search.SortOrder == SortOrder.Descending
                            ? query.OrderByDescending(x => x.ProductPrice)
                            : query.OrderBy(x => x.ProductPrice);
                }
                //if not it will be sku:
                else if (to_search.SortBy.Equals("sku", StringComparison.OrdinalIgnoreCase))
                {
                    query =
                        to_search.SortOrder == SortOrder.Descending
                            ? query.OrderByDescending(x => x.SKU)
                            : query.OrderBy(x => x.SKU);
                }
            }

            //implement pagination

            query = query.Skip(to_search.Offset).Take(to_search.Limit);

            return await query.ToListAsync();
        }
    }
}
