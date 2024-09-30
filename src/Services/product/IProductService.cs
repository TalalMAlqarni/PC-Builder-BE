using src.Entity;
using static src.DTO.ProductDTO;

namespace src.Services.product {

    public interface IProductService {


//create product
Task<GetProductDto> CreateProductAsync (CreateProductDto createProductDto);


//get all products 
Task<List<GetProductDto>> GetAllProductsAsync();

//get product by id
Task<GetProductDto> GetProductByIdAsync (Guid id);

//update product info
 Task<GetProductDto> UpdateProductInfoAsync(Guid id, UpdateProductInfoDto product);

//delete product 
Task<bool> DeleteProductByIdAsync (Guid id);

// Task<bool>UpdateProductDescAsync(Guid id , UpdateProdouctDescDto updateProductDescDto);

        
    }
}