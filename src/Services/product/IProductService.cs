using src.Entity;
using src.Utils;
using static src.DTO.ProductDTO;

namespace src.Services.product {

    public interface IProductService {


//create product
Task<GetProductDto> CreateProductAsync (CreateProductDto createProductDto);


//get all products 
Task<List<GetProductDto>> GetAllProductsAsync();

//search for a product with pagination 
Task<List<GetProductDto>>GetAllBySearchAsync(PaginationOptions paginationOptions);

//sort functionality test: 

Task<List<GetProductDto>>GetAllBySortAsync(SortOptions sortOptions);

//get product by id
Task<GetProductDto> GetProductByIdAsync (Guid id);

//update product info
 Task<GetProductDto> UpdateProductInfoAsync(Guid id, UpdateProductInfoDto product);

//delete product 
Task<bool> DeleteProductByIdAsync (Guid id);

// Task<bool>UpdateProductDescAsync(Guid id , UpdateProdouctDescDto updateProductDescDto);

        
    }
}