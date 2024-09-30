using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.Category;
using static src.DTO.CategoryDTO;

namespace src.Controller
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        protected readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
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


        // public static List<Category> categories = new List<Category>
        // {
        //     new Category { Id = Guid.NewGuid(), categoryName = "LivingRoom" },
        //     new Category { Id = Guid.NewGuid(), categoryName = "BedRoom" },
        //     new Category { Id = Guid.NewGuid(), categoryName = "Office" },
        //     new Category { Id = Guid.NewGuid(), categoryName = "DiningRoom" },
        //     new Category { Id = Guid.NewGuid(), categoryName = "HomeAccessories" }
        // };

        // GET method to retrive all categories
        // [HttpGet]
        // public ActionResult GetCategories()
        // {
        //     return Ok(categories);
        // }

        // GET method by a specific category name
        // [HttpGet("{categoryName}")]
        // public ActionResult GetCategoryByName(string categoryName)
        // {
        //     Category? foundCategory = categories.FirstOrDefault(c => c.categoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        //     if (foundCategory == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(foundCategory);
        // }

        // POST (Add) method
        // [HttpPost]
        // public ActionResult AddCategory([FromBody] Category newCategory)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     // Check if a category with the same name already exists
        //     var existingCategory = categories.FirstOrDefault(c => c.categoryName.Equals(newCategory.categoryName, StringComparison.OrdinalIgnoreCase));
        //     if (existingCategory != null)
        //     {
        //         return Conflict($"A category with the name '{newCategory.categoryName}' already exists.");
        //     }
        //     newCategory.Id = Guid.NewGuid();
        //     categories.Add(newCategory);
        //     return CreatedAtAction(nameof(GetCategories), new { categoryName = newCategory.categoryName }, newCategory);
        // }

        // PUT (Update) method by category's name
        // [HttpPut("{categoryName}")]
        // public ActionResult UpdateCategory(string categoryName, [FromBody] Category updatedCategory)
        // {
        //     var existingCategory = categories.FirstOrDefault(c => c.categoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        //     if (existingCategory == null)
        //     {
        //         return NotFound();
        //     }  
        //     existingCategory.categoryName = updatedCategory.categoryName;
        //     return NoContent(); 
        // }

        // // DELETE method by category's name
        // [HttpDelete("{categoryName}")]
        // public ActionResult DeleteCategory(string categoryName)
        // {
        //     var foundCategory = categories.FirstOrDefault(c => c.categoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        //     if(foundCategory == null)
        //     {
        //         return NotFound();
        //     }
        //     categories.Remove(foundCategory);
        //     return NoContent();
        // }

        // CRUD for SubCategories
        
        // [HttpGet("{categoryName}/subcategories")]
        // public ActionResult GetSubCategoriesByCategory(string categoryName)
        // {
        //     var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        //     if (category == null)
        //     {
        //         return NotFound($"Category '{categoryName}' not found.");
        //     }

        //     var subs = subCategories.Where(sc => sc.subId == category.Id).ToList();
        //     return Ok(subs);
        // }

        // [HttpPost("{categoryName}/subcategories")]
        // public ActionResult AddSubCategory(string categoryName, [FromBody] SubCategory newSubCategory)
        // {
        //     var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        //     if (category == null)
        //     {
        //         return NotFound($"Category '{categoryName}' not found.");
        //     }

        //     newSubCategory.Id = Guid.NewGuid();
        //     newSubCategory.subId = category.Id;
        //     subCategories.Add(newSubCategory);
        //     return CreatedAtAction(nameof(GetSubCategoriesByCategory), new { categoryName = categoryName }, newSubCategory);
        // }

        // [HttpPut("{categoryName}/subcategories/{subCategoryName}")]
        // public ActionResult UpdateSubCategory(string categoryName, string subCategoryName, [FromBody] SubCategory updatedSubCategory)
        // {
        //     var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        //     if (category == null)
        //     {
        //         return NotFound($"Category '{categoryName}' not found.");
        //     }

        //     var existingSubCategory = subCategories.FirstOrDefault(sc => sc.subId == category.Id && sc.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));
        //     if (existingSubCategory == null)
        //     {
        //         return NotFound($"SubCategory '{subCategoryName}' not found under Category '{categoryName}'.");
        //     }

        //     existingSubCategory.Name = updatedSubCategory.Name;
        //     return NoContent();
        // }

        // [HttpDelete("{categoryName}/subcategories/{subCategoryName}")]
        // public ActionResult DeleteSubCategory(string categoryName, string subCategoryName)
        // {
        //     var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        //     if (category == null)
        //     {
        //         return NotFound($"Category '{categoryName}' not found.");
        //     }

        //     var subCategory = subCategories.FirstOrDefault(sc => sc.subId == category.Id && sc.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));
        //     if (subCategory == null)
        //     {
        //         return NotFound($"SubCategory '{subCategoryName}' not found under Category '{categoryName}'.");
        //     }

        //     subCategories.Remove(subCategory);
        //     return NoContent();
        // }
    }

}