using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.Category;
using src.Services.SubCategory;
using src.Services.product;
using static src.DTO.SubCategoryDTO;
using static src.DTO.ProductDTO;
using Microsoft.EntityFrameworkCore;
using src.Utils;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Authorization;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubCategoriesController : ControllerBase
    {
        protected readonly ISubCategoryService _subCategoryService;
        protected readonly IProductService _productService;

        public SubCategoriesController(ISubCategoryService service,IProductService productService)
        {
            _subCategoryService = service;
            _productService = productService;
        }
        
        [AllowAnonymous]
        [HttpGet] // Get all subcategories
        public async Task<ActionResult<List<SubCategoryCreateDto>>>GetAllAsync()
        {
            var subCategoryList = await _subCategoryService.GetAllAsync();
            return Ok(subCategoryList);
        }

        [AllowAnonymous] 
        [HttpGet("{subCategoryId}")] // Get a specific category by Id
        public async Task<ActionResult<SubCategoryReadDto>>GetSubCategoryByIdWithProductsAsync([FromRoute] Guid subCategoryId)
        {
            var subCategory = await _subCategoryService.GetSubCategoryByIdAsync (subCategoryId);
            return Ok(subCategory);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost] // Add a subcategory
        public async Task<ActionResult<SubCategoryReadDto>> CreateSubCategory([FromBody] SubCategoryCreateDto createDto)
        {
            var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);
            return Ok(subCategoryCreated); 
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{subCategoryId}")] // Update a specific subcategory using its Id
        public async Task<ActionResult<SubCategoryReadDto>> UpdateSubCategory( [FromRoute] Guid subCategoryId, [FromBody] SubCategoryUpdateDto updateDto)
        {
            var updatedSubCategory = await _subCategoryService.UpdateOneAsync(subCategoryId,updateDto);
            return Ok(updatedSubCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{subCategoryId}")] // Delete a specific subcategory using its Id
        public async Task<IActionResult> DeleteSubCategory( Guid subCategoryId)
        {
           // var result = 
            await _subCategoryService.DeleteOneAsync(subCategoryId);
            // if (!result)
            // {
            //     return NotFound($"Subcategory with ID = {subCategoryId} not found.");
            // }
            return NoContent(); 
        }

        //  view all the products
        [AllowAnonymous]
        [HttpGet("products")] // Gat all products inside subcategories
        public async Task<ActionResult<List<GetProductDto>>> GetAllProductsAsync([FromQuery] SearchProcess to_search)
        {
          //  var products = await _productService.GetAllProductsAsync();
          var products = await _productService.GetAllAsync(to_search);
            return Ok(products);
        }

        // get product by id
        [AllowAnonymous] // Get product details by using its id
        [HttpGet("products/{productId}")] // Get all products of all subcategories
        public async Task<ActionResult<GetProductDto>> GetProductById(Guid productId)
        {
            //var isFound = await _productService.GetProductByIdAsync(productId);
            return Ok(await _productService.GetProductByIdAsync(productId));
        }

        // [Authorize(Roles = "Admin")] 
        [HttpPost("{subCategoryId}/products")] // Add products under a subcategory
        public async Task<ActionResult<GetProductDto>> CreateProductAsync(Guid subCategoryId, [FromBody] CreateProductDto productDto)
        {
            // Ensure that the product is linked to the correct subcategory
            productDto.SubCategoryId = subCategoryId;

            // Create product via the service
            var newProduct = await _productService.CreateProductAsync(productDto);

            return Ok(newProduct);
        }

        // [Authorize(Roles = "Admin")]
        [HttpPut("products/{productId}")] // Update a product under a subcategroy by using prodcut id
        public async Task<ActionResult<GetProductDto>> UpdateProductInfo(
        Guid productId,
        UpdateProductInfoDto productInfoDto)
        {
            var updatedInfo = await _productService.UpdateProductInfoAsync(
                productId,
                productInfoDto
            );
            return Ok(updatedInfo);
        }

        // [Authorize(Roles = "Admin")]
        [HttpDelete("products/{productId}")] // Delete a product under a subcategroy by using prodcut id
            public async Task<ActionResult<bool>> DeleteProductByIdAsync(Guid productId)
        {
            var isDeleted= await _productService.DeleteProductByIdAsync(productId);
            return Ok(isDeleted);
        }

        [AllowAnonymous]
        [HttpGet("search")] // Get all subcategories that match the search using pagination
        public async Task<ActionResult<List<SubCategoryReadDto>>> GetAllSubCategpryBySearch( [FromQuery] PaginationOptions paginationOptions)
        {
            var subCategpryList = await _subCategoryService.GetAllBySearchAsync(paginationOptions);
            return Ok(subCategpryList);
        }
    }
} 