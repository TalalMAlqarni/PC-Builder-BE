 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using src.Repository;
using src.Database;
using src.Entity;
using static src.DTO.CategoryDTO;

namespace src.Services.Category
{
    public class CategoryService : ICategoryService
    {
        protected readonly CategoryRepository _categoryRepo;
        protected readonly IMapper _mapper;
        public CategoryService (CategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task <CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto)
        {
            var category = _mapper.Map<CategoryCreateDto, src.Entity.Category>(createDto);
            var categoryCreated = await _categoryRepo.CreateOneAsync(category);
            return _mapper.Map<src.Entity.Category,CategoryReadDto>(categoryCreated);
        }

        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var categoryList= await _categoryRepo.GetAllAsync();
            return _mapper.Map<List<src.Entity.Category>, List<CategoryReadDto>>(categoryList);
        }

        public async Task<CategoryReadDto> GetByIdAsync(Guid id)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            return _mapper.Map<src.Entity.Category, CategoryReadDto> (foundCategory);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            
            if (foundCategory == null)
            {
                return false; // Category not found, return false
            }
            
            return await _categoryRepo.DeleteOneAsync(foundCategory); // Proceed to delete
        }

        public async Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto)
        {
            // Retrieve the category by ID from the repository
            var foundCategory = await _categoryRepo.GetByIdAsync(id);

            if (foundCategory == null)
            {
                return false; // Category not found
            }

            // Map the update DTO fields to the existing category entity
            _mapper.Map(updateDto, foundCategory);

            // Save the updated category in the repository
            return await _categoryRepo.UpdateOneAsync(foundCategory);
        }
        
    }
}

