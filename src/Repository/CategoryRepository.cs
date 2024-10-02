using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

using src.Utils;
namespace src.Repository
{
    public class CategoryRepository
    {
        protected DbSet<Category> _categories;
        protected DatabaseContext _databaseContext;

        public CategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _categories =databaseContext.Set<Category>();
        }
        public async Task<Category> CreateOneAsync(Category newCategory)
        {
            await _categories.AddAsync(newCategory);
            await _databaseContext.SaveChangesAsync();
            return newCategory;
        }
        
        public async Task<List<Category>> GetAllAsync()
        {
            return await _categories.ToListAsync();
        }
         public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _categories.FindAsync(id);
        }
//TRYING TO ADD SUBCATEGORIES UNDER A CATEGORY 
// public async Task<List<Category>> GetAllAsync()
// {
//     // Include subcategories when retrieving categories
//     return await _categories.Include(c => c.SubCategory).ToListAsync();
// }

// public async Task<Category> GetByIdAsync(Guid id)
// {
//     // Include subcategories when retrieving a category by ID
//     return await _categories.Include(c => c.SubCategory)
//                             .FirstOrDefaultAsync(c => c.Id == id);
// }



        public async Task<bool> DeleteOneAsync(Category category)
        {
            _categories.Remove(category);
            await _databaseContext.SaveChangesAsync();
            return true;
        }  
        
        public async Task<bool> UpdateOneAsync(Guid id,Category updateDto)
        {
            _categories.Update(updateDto);
            await _databaseContext.SaveChangesAsync();
            return true;
        }



    }
}   