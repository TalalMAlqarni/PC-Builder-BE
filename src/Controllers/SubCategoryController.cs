using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.Category;
using src.Services.SubCategory;
using static src.DTO.SubCategoryDTO;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        protected readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService Service)
        {
            _subCategoryService = Service;
        }

       [HttpGet]
        public async Task<ActionResult<List< SubCategoryCreateDto>>>GetAll()
        {
            var subCategoryList = await _subCategoryService.GetAllAsynac();
            return Ok(subCategoryList);
        }
        
        [HttpGet("{subCategoryId}")]
        public async Task<ActionResult<SubCategoryReadDto>>GetById([FromRoute] Guid subCategoryId)
        {
            var subCategory = await _subCategoryService.GetByIdAsynac (subCategoryId);
            return Ok(subCategory);
        }

        [HttpDelete("{subCategoryId}")]
        public async Task<IActionResult> DeleteOne([FromRoute] Guid subCategoryId)
        {
            var result = await _subCategoryService.DeleteOneAsync(subCategoryId);
            
            if (!result)
            {
                return NotFound($"Subcategory with Id = {subCategoryId} not found.");
            }
            
            return NoContent(); // 204 No Content
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoryReadDto>> CreateOne([FromBody] SubCategoryCreateDto createDto)
        {
            var subCategoryCreated = await _subCategoryService.CreateOneAsync(createDto);
            return Created($"api/v1/subcategories/{subCategoryCreated.SubCategoryId}", subCategoryCreated);
        }


        // GET method to retrieve all subcategories 
        // [HttpGet]
        // public ActionResult GetSubCategories()
        // {
        //     return Ok(subCategories);
        // }

        // // GET method to retrieve all subcategories for a specific category
        // [HttpGet("{categoryName}")]
        // public ActionResult GetSubCategoriesByCategory(string categoryName)
        // {
        //     var category = categories.FirstOrDefault(c => c.categoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        //     if (category == null)
        //     {
        //         return NotFound($"Category '{categoryName}' not found.");
        //     }

        //     var subs = subCategories.Where(sc => sc.Id == category.Id).ToList();
        //     if (subs.Count == 0)
        //     {
        //         return NotFound($"No subcategories found for category '{categoryName}'.");
        //     }
        //     return Ok(subs);
        // }

        // // POST method to add a new subcategory
        // [HttpPost("{categoryName}")]
        // public ActionResult AddSubCategory(string categoryName, [FromBody] SubCategory newSubCategory)
        // {
        //     newSubCategory.Id = Guid.NewGuid();
        //     newSubCategory.Name = categoryName;
        //     subCategories.Add(newSubCategory);
        //     return CreatedAtAction(nameof(GetSubCategoriesByCategory), new { categoryName = categoryName }, newSubCategory);
        // }

        // // PUT method to update a subcategory
        // [HttpPut("{categoryName}/{subCategoryName}")]
        // public ActionResult UpdateSubCategory(string categoryName, string subCategoryName, [FromBody] SubCategory updatedSubCategory)
        // {
        //     var existingSubCategory = subCategories.FirstOrDefault(sc => sc.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase) && sc.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));
        //     if (existingSubCategory == null)
        //     {
        //         return NotFound($"SubCategory '{subCategoryName}' not found under category '{categoryName}'.");
        //     }
        //     existingSubCategory.Name = updatedSubCategory.Name;
        //     return NoContent();
        // }

        // // DELETE method to remove a subcategory
        // [HttpDelete("{categoryName}/{subCategoryName}")]
        // public ActionResult DeleteSubCategory(string categoryName, string subCategoryName)
        // {
        //     var subCategory = subCategories.FirstOrDefault(sc => sc.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase) && sc.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));
        //     if (subCategory == null)
        //     {
        //         return NotFound($"SubCategory '{subCategoryName}' not found under category '{categoryName}'.");
        //     }
        //     subCategories.Remove(subCategory);
        //     return NoContent();
        // }
    }
}
