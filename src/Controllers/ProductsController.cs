using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services;
using src.Services.product;
using src.Utils;
using static src.DTO.ProductDTO;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        protected readonly IProductService _productService;

        /*The standard user privileges:
        view the product/products
        search for product by name
        sort products by range/price/...etc
        add the product to the cart
         */

        /* the admin user privileges:
       view the product/products
       add a new product
       delete a product
       edit/update on the product info
*/


        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // view all the products in specific subcategory:
        // [AllowAnonymous]
        // [HttpGet]
        // public async Task<ActionResult<List<GetProductDto>>> GetAllProducts()
        // {
        //     var products = await _productService.GetAllProductsAsync();
        //     return Ok(products);
        // }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProducts(
            [FromQuery] SearchProcess to_search
        )
        {
            var products = await _productService.GetAllAsync(to_search);
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("filter")]
        public async Task<ActionResult<List<Product>>> FilterProducts(
            [FromQuery] FilterationOptions pf
        )
        {
            var products = await _productService.GetAllByFilterationAsync(pf);
            return Ok(products);
        }

        //get all products that match the search with pagination
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProductsBySearch(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var productsList = await _productService.GetAllBySearchAsync(paginationOptions);
            return Ok(productsList);
        }

        //sort
        [AllowAnonymous]
        [HttpGet("sort")]
        public async Task<ActionResult<List<GetProductDto>>> GetAllBySort(
            [FromQuery] SortOptions sortOption
        )
        {
            var products = await _productService.GetAllBySortAsync(sortOption);
            return Ok(products);
        }

        //get product by id

        [HttpGet("{productId}")] //check it again
        public async Task<ActionResult<GetProductDto>> GetProductById(Guid productId)
        {
            //var isFound = await _productService.GetProductByIdAsync(productId);
            return Ok(await _productService.GetProductByIdAsync(productId));
        }

        //add product : it'll moved to the subcategory class
        [HttpPost] // had to check the endopoint
        //[Authorize(Roles = "Admin")] //didn't test it yet
        public async Task<ActionResult<GetProductDto>> CreateProduct(CreateProductDto productDto)
        {
            var newProduct = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(
                nameof(CreateProduct),
                new { id = newProduct.ProductId },
                newProduct
            );
        }

      
        //delete a product, it'll moved to the subcategory class
        [HttpDelete("{productId}")]
       // [Authorize(Roles = "Admin")] //didn't test it yet
        public async Task<ActionResult> DeleteProductById(Guid productId)
        {
            var toDelete = await _productService.DeleteProductByIdAsync(productId);
            return Ok();
        }

        [HttpPut("{productId}")]
        // [Authorize(Roles = "Admin")] //didn't test it yet
        public async Task<ActionResult<GetProductDto>> UpdateProductInfo(
            Guid productId,
            UpdateProductInfoDto productInfoDto
        )
        {
            var updatedInfo = await _productService.UpdateProductInfoAsync(
                productId,
                productInfoDto
            );
            return Ok(updatedInfo);
        }

       
    }
}
