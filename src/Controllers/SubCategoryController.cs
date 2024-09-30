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

        [HttpGet("{id}/subcategories")]
        public async Task<ActionResult<List<SubCategoryReadDto>>> GetSubCategoriesByCategory(Guid id)
        {
            var subcategories = await _subCategoryService.GetByIdAsync(id);
            return Ok(subcategories);
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoryReadDto>> CreateOne([FromBody] SubCategoryCreateDto createDto)
        {
            var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);
            // return Created(categoryCreated);
            return Created($"api/v1/category/{subCategoryCreated.SubCategoryId}",subCategoryCreated);
        }

        [HttpPut("{id}/subcategories/{subCategoryId}")]
        public async Task<ActionResult<SubCategoryReadDto>> UpdateSubCategory(Guid id, Guid subCategoryId, [FromBody] SubCategoryUpdateDto updateDto)
        {
            var result = await _subCategoryService.UpdateOneAsync(id, subCategoryId, updateDto);
            if (!result)
            {
                return NotFound($"Subcategory with ID = {subCategoryId} not found.");
            }
            
            var updatedSubCategory = await _subCategoryService.GetByIdAsync(subCategoryId);
            return Ok(updatedSubCategory);
        }

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

        // [HttpDelete("{subCategoryId}")]
        // public async Task<IActionResult> DeleteOneAsync([FromRoute] Guid subCategoryId)
        // {
        //     var result = await _subCategoryService.DeleteOneAsync(subCategoryId);
            
        //     if (!result)
        //     {
        //         return NotFound($"Subcategory with Id = {subCategoryId} not found.");
        //     }
            
        //     return NoContent(); // 204 No Content
        // }

        // [HttpPost]
        // public async Task<ActionResult<SubCategoryReadDto>> CreateOneAsync([FromBody] SubCategoryCreateDto createDto)
        // {
        //     var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);

        //     // Ensure the route and action match the expected ones.
        //     return Created($"api/v1/subCategory/{subCategoryCreated.SubCategoryId}",subCategoryCreated);
        // }
        [HttpPost("{id}/subcategories")]
        public async Task<ActionResult<SubCategoryReadDto>> CreateSubCategory(Guid id, [FromBody] SubCategoryCreateDto createDto)
        {
            createDto.Id = id;  // Assign the main category
            var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);
            return CreatedAtAction(nameof(GetSubCategoriesByCategory), new { categoryId = subCategoryCreated.Id }, subCategoryCreated);
        }
    }
}