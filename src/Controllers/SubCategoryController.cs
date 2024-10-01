using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.Category;
using src.Services.SubCategory;
using src.Services.product;
using static src.DTO.SubCategoryDTO;
using static src.DTO.ProductDTO;
using Microsoft.EntityFrameworkCore;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        protected readonly ISubCategoryService _subCategoryService;
        protected readonly IProductService _productService;
        //public List<SubCategory> subCategories = new List<SubCategory>();
     //   public static List<Product> products { get; set; }

        public SubCategoryController(ISubCategoryService service,IProductService productService)
        {
            _subCategoryService = service;
            _productService = productService;
        }

       
    //    [HttpGet]
    //     public async Task<ActionResult<List<SubCategoryCreateDto>>>GetAllAsync()
    //     {
    //         var subCategoryList = await _subCategoryService.GetAllAsync();
    //         return Ok(subCategoryList);
    //     }
        
    //     [HttpGet("{subCategoryId}")]
    //     public async Task<ActionResult<SubCategoryReadDto>>GetByIdAsync([FromRoute] Guid subCategoryId)
    //     {
    //         var subCategory = await _subCategoryService.GetByIdAsync (subCategoryId);
    //         return Ok(subCategory);
    //     }

        // [HttpGet("{id}/subcategories")]
        // public async Task<ActionResult<List<SubCategoryReadDto>>> GetSubCategoriesByCategory(Guid id)
        // {
        //     var subcategories = await _subCategoryService.GetByIdAsync(id);
        //     return Ok(subcategories);
        // }

        // [HttpPost]
        // public async Task<ActionResult<SubCategoryReadDto>> CreateSubCategory([FromBody] SubCategoryCreateDto createDto)
        // {
        //     var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);
        //     return Ok(subCategoryCreated);
        //     // return Created($"api/v1/category/{subCategoryCreated.SubCategoryId}",subCategoryCreated);
        // }

        // [HttpPut("{id}/subcategories/{subCategoryId}")]
        // public async Task<ActionResult<SubCategoryReadDto>> UpdateSubCategory(Guid id, Guid subCategoryId, [FromBody] SubCategoryUpdateDto updateDto)
        // {
        //     var result = await _subCategoryService.UpdateOneAsync(id, subCategoryId, updateDto);
        //     if (!result)
        //     {
        //         return NotFound($"Subcategory with ID = {subCategoryId} not found.");
        //     }
            
        //     var updatedSubCategory = await _subCategoryService.GetByIdAsync(subCategoryId);
        //     return Ok(updatedSubCategory);
        // }

        [HttpDelete("{id}/subcategories/{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCategory(Guid id, Guid subCategoryId)
        {
            var result = await _subCategoryService.DeleteOneAsync(id, subCategoryId);
            if (!result)
            {
                return NotFound($"Subcategory with ID = {subCategoryId} not found.");
            }
            return NoContent();
        }

         // view all the products
        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        //get product by id

        // [HttpGet("{productId}")] //check it again
        // public async Task<ActionResult<GetProductDto>> GetProductById(Guid productId)
        // {
        //     //var isFound = await _productService.GetProductByIdAsync(productId);
        //     return Ok(await _productService.GetProductByIdAsync(productId));
        // }

        //add product : it'll moved to the subcategory class
    //     [HttpPost] // had to check the endopoint
    //    // [Authorize(Roles = "Admin")] //didn't test it yet
    //     public async Task<ActionResult<GetProductDto>> CreateProductAsync([FromBody] CreateProductDto productDto)
    //     {
    //         var newProduct = await _productService.CreateProductAsync(productDto);
    //         return CreatedAtAction(
    //             nameof(CreateProductAsync),
    //             new { id = newProduct.ProductId },
    //             newProduct
    //         );
        }
    }