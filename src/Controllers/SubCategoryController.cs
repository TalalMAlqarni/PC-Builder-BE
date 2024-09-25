using Microsoft.AspNetCore.Mvc;
using src.Entity;
using System.Collections.Generic;
using System.Linq;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    
    public class SubCategoryController : ControllerBase
    {
        private readonly List<Category> categories;
        public static List<SubCategory> subCategories = new List<SubCategory>
        {

            // Subcategories for "Living Room"
            new SubCategory { Id = Guid.Parse("a4f7bbba-2463-4b1e-b7d3-f2b93700c61e"), subId = Guid.NewGuid(), Name = "Sofas" },
            new SubCategory { Id = Guid.Parse("a4f7bbba-2463-4b1e-b7d3-f2b93700c61e"), subId = Guid.NewGuid(), Name = "Coffee Tables" },
            new SubCategory { Id = Guid.Parse("a4f7bbba-2463-4b1e-b7d3-f2b93700c61e"), subId = Guid.NewGuid(), Name = "TV Stands" },

            // Subcategories for "Bedroom"
            new SubCategory { Id = Guid.Parse("984bc6e1-6607-449b-9420-11be64d90a4d"), subId = Guid.NewGuid(), Name = "Beds" },
            new SubCategory { Id = Guid.Parse("984bc6e1-6607-449b-9420-11be64d90a4d"), subId = Guid.NewGuid(), Name = "Dressers" },

            // Subcategories for "Office"
            new SubCategory { Id = Guid.Parse("73f5af76-444d-41aa-b7a7-9ea0dd6b9455"), subId = Guid.NewGuid(), Name = "Desks" },
            new SubCategory { Id = Guid.Parse("73f5af76-444d-41aa-b7a7-9ea0dd6b9455"), subId = Guid.NewGuid(), Name = "Office Chairs" },
            new SubCategory { Id = Guid.Parse("73f5af76-444d-41aa-b7a7-9ea0dd6b9455"), subId = Guid.NewGuid(), Name = "Bookshelves" },

            // Subcategories for "Dining Room"
            new SubCategory { Id = Guid.Parse("92a4bbd2-2fb1-4c98-909f-17ff132a5170"), subId = Guid.NewGuid(), Name = "Dining Tables" },
            new SubCategory { Id = Guid.Parse("92a4bbd2-2fb1-4c98-909f-17ff132a5170"), subId = Guid.NewGuid(), Name = "Dining Chairs" },
            new SubCategory { Id = Guid.Parse("92a4bbd2-2fb1-4c98-909f-17ff132a5170"), subId = Guid.NewGuid(), Name = "Buffets" },

            // Subcategories for "Home Accessories"
            new SubCategory { Id = Guid.Parse("d90f3b75-b9e5-4a1f-a10f-e11228f39a8a"), subId = Guid.NewGuid(), Name = "Rugs" },
            new SubCategory { Id = Guid.Parse("d90f3b75-b9e5-4a1f-a10f-e11228f39a8a"), subId = Guid.NewGuid(), Name = "Lights" },
            new SubCategory { Id = Guid.Parse("d90f3b75-b9e5-4a1f-a10f-e11228f39a8a"), subId = Guid.NewGuid(), Name = "Wall Art" },
            new SubCategory { Id = Guid.Parse("d90f3b75-b9e5-4a1f-a10f-e11228f39a8a"), subId = Guid.NewGuid(), Name = "Curtains" }
        };

        // GET method to retrieve all subcategories 
        [HttpGet]
        public ActionResult GetSubCategories()
        {
            return Ok(subCategories);
        }

        // GET method to retrieve all subcategories for a specific category
        [HttpGet("{categoryName}")]
        public ActionResult GetSubCategoriesByCategory(string categoryName)
        {
            var category = categories.FirstOrDefault(c => c.categoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                return NotFound($"Category '{categoryName}' not found.");
            }

            var subs = subCategories.Where(sc => sc.Id == category.Id).ToList();
            if (subs.Count == 0)
            {
                return NotFound($"No subcategories found for category '{categoryName}'.");
            }
            return Ok(subs);
        }

        // POST method to add a new subcategory
        [HttpPost("{categoryName}")]
        public ActionResult AddSubCategory(string categoryName, [FromBody] SubCategory newSubCategory)
        {
            newSubCategory.Id = Guid.NewGuid();
            newSubCategory.Name = categoryName;
            subCategories.Add(newSubCategory);
            return CreatedAtAction(nameof(GetSubCategoriesByCategory), new { categoryName = categoryName }, newSubCategory);
        }

        // PUT method to update a subcategory
        [HttpPut("{categoryName}/{subCategoryName}")]
        public ActionResult UpdateSubCategory(string categoryName, string subCategoryName, [FromBody] SubCategory updatedSubCategory)
        {
            var existingSubCategory = subCategories.FirstOrDefault(sc => sc.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase) && sc.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));
            if (existingSubCategory == null)
            {
                return NotFound($"SubCategory '{subCategoryName}' not found under category '{categoryName}'.");
            }
            existingSubCategory.Name = updatedSubCategory.Name;
            return NoContent();
        }

        // DELETE method to remove a subcategory
        [HttpDelete("{categoryName}/{subCategoryName}")]
        public ActionResult DeleteSubCategory(string categoryName, string subCategoryName)
        {
            var subCategory = subCategories.FirstOrDefault(sc => sc.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase) && sc.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));
            if (subCategory == null)
            {
                return NotFound($"SubCategory '{subCategoryName}' not found under category '{categoryName}'.");
            }
            subCategories.Remove(subCategory);
            return NoContent();
        }
    }
}
