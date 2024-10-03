using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.Category;
using src.Services.SubCategory;
using src.Services.product;
using static src.DTO.SubCategoryDTO;
using static src.DTO.ProductDTO;
using Microsoft.EntityFrameworkCore;
using src.Utils;
// using Microsoft.AspNetCore.Authorization;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        protected readonly ISubCategoryService _subCategoryService;
        protected readonly IProductService _productService;

        public SubCategoryController(ISubCategoryService service,IProductService productService)
        {
            _subCategoryService = service;
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<SubCategoryCreateDto>>>GetAllAsync()
        {
            var subCategoryList = await _subCategoryService.GetAllAsync();
            return Ok(subCategoryList);
        }

        [HttpGet("{subCategoryId}")]
        public async Task<ActionResult<SubCategoryReadDto>>GetSubCategoryByIdWithProductsAsync([FromRoute] Guid subCategoryId)
        {
            var subCategory = await _subCategoryService.GetSubCategoryByIdAsync (subCategoryId);
            return Ok(subCategory);
        }
        
        [HttpPost]
        // [Authorize(Roles = "Admin")]

        public async Task<ActionResult<SubCategoryReadDto>> CreateSubCategory([FromBody] SubCategoryCreateDto createDto)
        {
            var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);
            return Ok(subCategoryCreated); 
        }

      [HttpPut("{subCategoryId}")]
        public async Task<ActionResult<SubCategoryReadDto>> UpdateSubCategory( [FromRoute] Guid subCategoryId, [FromBody] SubCategoryUpdateDto updateDto)
        {
            // Optionally, return the updated SubCategory data
            var updatedSubCategory = await _subCategoryService.UpdateOneAsync(subCategoryId,updateDto);
            return Ok(updatedSubCategory);
        }

     [HttpDelete("{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCategory( Guid subCategoryId)
        {
            var result = await _subCategoryService.DeleteOneAsync(subCategoryId);
            if (!result)
            {
                return NotFound($"Subcategory with ID = {subCategoryId} not found.");
            }
            return NoContent(); 
        }

        //  view all the products
        [HttpGet("products")]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        //get product by id
        [HttpGet("products/{productId}")] //check it again
        public async Task<ActionResult<GetProductDto>> GetProductById(Guid productId)
        {
            //var isFound = await _productService.GetProductByIdAsync(productId);
            return Ok(await _productService.GetProductByIdAsync(productId));
        }

    //     // add product : it'll moved to the subcategory class
    //     [HttpPost("/products")] // had to check the endopoint
    //    // [Authorize(Roles = "Admin")] //didn't test it yet
    //     public async Task<ActionResult<GetProductDto>> CreateProductAsync([FromBody] CreateProductDto productDto)
    //     {
    //         var newProduct = await _productService.CreateProductAsync(productDto);
    //         return CreatedAtAction(
    //             nameof(CreateProductAsync),
    //             new { id = newProduct.ProductId },
    //             newProduct
    //         );
    //     }

        [HttpPost("{subCategoryId}/products")] // Updated endpoint to include subCategoryId
        public async Task<ActionResult<GetProductDto>> CreateProductAsync(Guid subCategoryId, [FromBody] CreateProductDto productDto)
        {
            // Ensure that the product is linked to the correct subcategory
            productDto.SubCategoryId = subCategoryId;

            // Create product via the service
            var newProduct = await _productService.CreateProductAsync(productDto);

            // Return the newly created product with 201 Created status
            return CreatedAtAction(nameof(CreateProductAsync), new { id = newProduct.ProductId }, newProduct);
        }

        // get all subcategories that match the search using pagination
        [HttpGet("search")]
        public async Task<ActionResult<List<SubCategoryReadDto>>> GetAllSubCategpryBySearch(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var subCategpryList = await _subCategoryService.GetAllBySearchAsync(paginationOptions);
            return Ok(subCategpryList);
        }
    }
}