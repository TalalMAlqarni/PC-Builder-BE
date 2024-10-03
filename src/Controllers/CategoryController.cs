using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.Category;
using src.Services.SubCategory;
using static src.DTO.CategoryDTO;
using static src.DTO.SubCategoryDTO;

namespace src.Controller
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        protected readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public CategoryController(ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

       [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetAllCategories()
        {
            var category_list = await _categoryService.GetAllAsync();
            return Ok(category_list);
        }

        //get cart by id: GET api/v1/Category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateCategory(CategoryCreateDto createDto)
        {
            var createdCategory = await _categoryService.CreateOneAsync(createDto);
            return Ok(createdCategory);
        }
        

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryReadDto>> UpdateOneAsync([FromRoute] Guid id, [FromBody] CategoryUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return validation errors
            }

            var categoryExists = await _categoryService.GetByIdAsync(id);
            if (categoryExists == null)
            {
                return NotFound($"Category with ID = {id} not found.");
            }

            var isUpdated = await _categoryService.UpdateOneAsync(id, updateDto);
            if (!isUpdated)
            {
                return StatusCode(500, "An error occurred while updating the category.");
            }

            var updatedCategory = await _categoryService.GetByIdAsync(id);
            return Ok(updatedCategory);
        }

                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneAsync([FromRoute] Guid id)
        {
            var result = await _categoryService.DeleteOneAsync(id);
            if (!result)
            {
                return NotFound($"Category with ID = {id} not found.");
            }
            return NoContent(); // 204 No Content
        }
    
    }
}
