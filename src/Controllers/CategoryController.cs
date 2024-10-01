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
        public async Task<ActionResult<CategoryReadDto>> CreateOne([FromBody] CategoryCreateDto createDto)
        {
            var categoryCreated = await _categoryService.CreateOneAsync(createDto);
            // return Created(categoryCreated);
            return Created($"api/v1/category/{categoryCreated.Id}",categoryCreated);
        }
        
        // [HttpPut("{id}")]
        // public async Task<ActionResult<CategoryReadDto>> UpdateCategoryAsync(
        //     [FromRoute] Guid id,
        //     Category updateDto
        // )
        // {
        //     var updatedInfo = await _categoryService.GetByIdAsync(id);
        //     return Ok(updatedInfo);
        // }
        // [HttpPut("{id}")]
        // public async Task<ActionResult<CategoryReadDto>> UpdateOneAsync([FromRoute] Guid id, [FromBody] Category updateCategory)
        // {
        //     // Check if the category exists
        //     var category = await _categoryService.UpdateByIdAsync(id,updateCategory);
        //     return Ok(category);

        // } 


        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryReadDto>> UpdateOneAsync([FromRoute] Guid id, [FromBody] CategoryUpdateDto updateDto)
        {
            // First, check if the category exists
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID = {id} not found.");
            }

            // Perform the update
            var isUpdated = await _categoryService.UpdateOneAsync(id, updateDto);
            if (isUpdated==null)
            {
                return StatusCode(500, "An error occurred while updating the category.");
            }

            // Fetch the updated category and return it
            var updatedCategory = await _categoryService.GetByIdAsync(id);
            return Ok(updatedCategory);
        }
// [HttpPut("{id}")]
// public async Task<ActionResult<CategoryReadDto>> UpdateCategoryAsync(
//     [FromRoute] Guid id,
//     [FromBody] CategoryUpdateDto updatedCategory // Assuming you're using CategoryUpdateDto
// )
// {
//     // Step 1: Retrieve the existing category
//     var existingCategory = await _categoryService.UpdateCategoryAsync(id,updatedCategory);

//     if (existingCategory == null)
//     {
//         return NotFound($"Category with ID {id} not found.");
//     }

//     // Step 2: Perform the update
//     var updatedCategory = await _categoryService.GetByIdAsync(id);

//     // Step 3: Return the updated category
//     return Ok(updatedCategory);
// }

        
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
    
    //  POST subcategory under a category
    // [HttpPost("{id}/subcategories")]
    // public async Task<ActionResult<SubCategoryReadDto>> CreateSubCategory(Guid id, [FromBody] SubCategoryCreateDto createDto)
    // {
    //     // Ensure the subcategory is linked to the correct category
    //     var subCategory = new SubCategory
    //     {
    //         Name = createDto.Name,
    //     };
    //     var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);
    //     return Ok(subCategoryCreated);
    // }

// POST subcategory under a category
[HttpPost("{id}/subcategories")]
public async Task<ActionResult<SubCategoryReadDto>> CreateSubCategory(Guid id, [FromBody] SubCategoryCreateDto createDto)
{
    // Call the service method to create the subcategory
    var subCategoryCreated = await _subCategoryService.CreateOneAsync(id, createDto);

    if (subCategoryCreated == null)
    {
        return NotFound($"Category with ID = {id} not found.");
    }

    // Return the created subcategory with a 201 status code
    return Ok(subCategoryCreated);
}

    // [HttpPost("{id}/subcategories")]
    // public async Task<ActionResult<SubCategoryReadDto>> CreateSubCategory( [FromBody] SubCategoryCreateDto createDto)
    // {
    //     var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);
    //     return Ok(subCategoryCreated);
    // }

 


//     [HttpPost("{id}/subcategories")]
// public async Task<ActionResult<SubCategoryReadDto>> CreateSubCategory(Guid id, [FromBody] SubCategoryCreateDto createDto)
// {
//     // Assuming the SubCategoryCreateDto has a property for CategoryId
//     createDto.Id = id;

//     var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);

//     if (subCategoryCreated == null)
//     {
//         return BadRequest("Error creating subcategory");
//     }

//     return CreatedAtAction(nameof(CreateSubCategory), new { id = subCategoryCreated.SubCategoryId }, subCategoryCreated);
// }


    // DELETE subcategory under a category
    [HttpDelete("{id}/subcategories/{subCategoryId}")]
    public async Task<ActionResult> DeleteSubCategory(Guid id, Guid subCategoryId)
    {
        var result = await _subCategoryService.DeleteOneAsync(id, subCategoryId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    // UPDATE subcategory under a category
    [HttpPut("{id}/subcategories/{subCategoryId}")]
    public async Task<ActionResult> UpdateSubCategory(Guid id, Guid subCategoryId, [FromBody] SubCategoryUpdateDto updateDto)
    {
        var result = await _subCategoryService.UpdateOneAsync(id, subCategoryId, updateDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }   
    
    }
}
