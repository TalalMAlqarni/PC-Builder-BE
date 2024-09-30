using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services;
using src.Services.product;
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
        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        //get product by id

        [HttpGet("{id}")]   //check it again 
        public async Task<ActionResult<GetProductDto>> GetProductById([FromRoute] Guid productId)
        {
            var isFound = await _productService.GetProductByIdAsync(productId);
            return Ok(isFound);
        }

        //add product : it'll moved to the subcategory class
        [HttpPost] // had to check the endopoint
        public async Task<ActionResult<GetProductDto>> CreateProduct(CreateProductDto productDto)
        {
            var newProduct = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(
                nameof(newProduct),
                new { id = newProduct.ProductId },
                newProduct
            );
            
        }

        // //search on a specific product byname
        // [HttpGet("{name}")] //?
        // public ActionResult GetProductByName(string name)
        // {
        //     List<Product>? result = products
        //          .Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase))
        //          .ToList();

        //     if (result is null)
        //     {
        //         return NotFound("No Results Found");
        //     }

        //     return Ok(result);
        // }


        //delete a product, it'll moved to the subcategory class
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductById(Guid productId)
        {
            var toDelete = await _productService.DeleteProductByIdAsync(productId);
            return Ok();
        }

        //edit on the product info , should it move to the subcategory class? also, how to add authorization here

        [HttpPut("{id}")]
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
        // public ActionResult UpdateProductInfo(
        //     string attributeName,
        //     string newValue,
        //     Product product,
        //     Guid id
        // )
        // {
        //     // Add the condition (if user_role is admin, otherwise it will not be allowed)
        //     Product? isFound = products.FirstOrDefault(x => x.ProductId == id);

        //     if (isFound == null)
        //     {
        //         return NotFound();
        //     }

        //     // is switch case a good choice here? like what info you want to update?
        //     switch (attributeName)
        //     {
        //         case "Name": // I want to add ignore case
        //             isFound.ProductName = newValue;
        //             product.ProductName = isFound.ProductName;
        //             break;
        //         case "Color":
        //             isFound.ProductColor = newValue;
        //             product.ProductColor = isFound.ProductColor;
        //             break;
        //         case "Price":
        //             isFound.ProductPrice = Convert.ToDecimal(newValue);
        //             product.ProductPrice = isFound.ProductPrice;
        //             break;
        //         case "Weight":
        //             isFound.Weight = Convert.ToDecimal(newValue);
        //             product.Weight = isFound.Weight;
        //             break;
        //     }
        //     return Ok(isFound);
        // }


    }
}
