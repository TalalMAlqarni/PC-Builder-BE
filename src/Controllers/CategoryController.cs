using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        public static List<Category> categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Living Room" },
            new Category { Id = Guid.NewGuid(), Name = "Bedroom" },
            new Category { Id = Guid.NewGuid(), Name = "Office" },
            new Category { Id = Guid.NewGuid(), Name = "Dining Room" },
            new Category { Id = Guid.NewGuid(), Name = "Home Accessories" }
        };

        public static List<SubCategory> subCategories = new List<SubCategory>
        {
            // Subcategories for "Living Room"
            new SubCategory { Id = Guid.NewGuid(), subId = categories[0].Id, Name = "Sofas" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[0].Id, Name = "Coffee Tables" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[0].Id, Name = "TV Stands" },

            // Subcategories for "Bed Room"
            new SubCategory { Id = Guid.NewGuid(), subId = categories[1].Id, Name = "Beds" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[1].Id, Name = "Dressers" },

            // Subcategories for "Office"
            new SubCategory { Id = Guid.NewGuid(), subId = categories[2].Id, Name = "Desks" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[2].Id, Name = "Office Chairs" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[2].Id, Name = "Bookshelves" },

            // Subcategories for "Dining Room"
            new SubCategory { Id = Guid.NewGuid(), subId = categories[3].Id, Name = "Dining Tables" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[3].Id, Name = "Dining Chairs" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[3].Id, Name = "Buffets" },

            // Subcategories for "Home Accessories"
            new SubCategory { Id = Guid.NewGuid(), subId = categories[4].Id, Name = "Rugs" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[4].Id, Name = "Lights" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[4].Id, Name = "Wall Art" },
            new SubCategory { Id = Guid.NewGuid(), subId = categories[4].Id, Name = "Curtains" }
        };

        // GET method to retrive all categories
        [HttpGet]
        public ActionResult GetCategories()
        {
            return Ok(categories);
        }

        // GET method by a specific category name
        [HttpGet("{name}")]
        public ActionResult GetCategoryByName(string name)
        {
            Category? foundCategory = categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (foundCategory == null)
            {
                return NotFound();
            }
            return Ok(foundCategory);
        }
    // POST method by category id
    //     [HttpPost]
    //     public ActionResult AddCategory([FromBody] Category newCategory)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }

    //     // Generate a new ID for the category if not provided
    //     newCategory.Id = Guid.NewGuid();

    //     // Add the new category to the list
    //     categories.Add(newCategory);

    //  return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, newCategory);
    // }

        // POST (Add) method by category's name
        [HttpPost]
        public ActionResult AddCategory([FromBody] Category newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check if a category with the same name already exists
            var existingCategory = categories.FirstOrDefault(c => c.Name.Equals(newCategory.Name, StringComparison.OrdinalIgnoreCase));
            if (existingCategory != null)
            {
                return Conflict($"A category with the name '{newCategory.Name}' already exists.");
            }
            newCategory.Id = Guid.NewGuid();
            // Add the new category to the list
            categories.Add(newCategory);
            return CreatedAtAction(nameof(GetCategoryByName), new { name = newCategory.Name }, newCategory);
        }

        // PUT (Update) method by category's name
        [HttpPut("{name}")]
        public ActionResult UpdateCategory(string name, [FromBody] Category updatedCategory)
        {
            var existingCategory = categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingCategory == null)
            {
                return NotFound();
            }  
            // Update the existing category's details
            existingCategory.Name = updatedCategory.Name;
            return NoContent(); 
        }

        // DELETE method by category's name
        [HttpDelete("{name}")]
        public ActionResult DeleteCategory(string name)
        {
            Category? foundCategory = categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if(foundCategory == null)
            {
                return NotFound();
            }
            categories.Remove(foundCategory);
            return NoContent();
        }

        // CRUD for SubCategories
        
        [HttpGet("{categoryName}/subcategories")]
        public ActionResult GetSubCategoriesByCategory(string categoryName)
        {
            var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                return NotFound($"Category '{categoryName}' not found.");
            }

            var subs = subCategories.Where(sc => sc.subId == category.Id).ToList();
            return Ok(subs);
        }

        [HttpPost("{categoryName}/subcategories")]
        public ActionResult AddSubCategory(string categoryName, [FromBody] SubCategory newSubCategory)
        {
            var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                return NotFound($"Category '{categoryName}' not found.");
            }

            newSubCategory.Id = Guid.NewGuid();
            newSubCategory.subId = category.Id;
            subCategories.Add(newSubCategory);
            return CreatedAtAction(nameof(GetSubCategoriesByCategory), new { categoryName = categoryName }, newSubCategory);
        }

        [HttpPut("{categoryName}/subcategories/{subCategoryName}")]
        public ActionResult UpdateSubCategory(string categoryName, string subCategoryName, [FromBody] SubCategory updatedSubCategory)
        {
            var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                return NotFound($"Category '{categoryName}' not found.");
            }

            var existingSubCategory = subCategories.FirstOrDefault(sc => sc.subId == category.Id && sc.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));
            if (existingSubCategory == null)
            {
                return NotFound($"SubCategory '{subCategoryName}' not found under Category '{categoryName}'.");
            }

            existingSubCategory.Name = updatedSubCategory.Name;
            return NoContent();
        }

        [HttpDelete("{categoryName}/subcategories/{subCategoryName}")]
        public ActionResult DeleteSubCategory(string categoryName, string subCategoryName)
        {
            var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                return NotFound($"Category '{categoryName}' not found.");
            }

            var subCategory = subCategories.FirstOrDefault(sc => sc.subId == category.Id && sc.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));
            if (subCategory == null)
            {
                return NotFound($"SubCategory '{subCategoryName}' not found under Category '{categoryName}'.");
            }

            subCategories.Remove(subCategory);
            return NoContent();
        }
    }

}