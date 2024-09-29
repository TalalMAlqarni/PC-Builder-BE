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
           bool IsDeleted = await _categoryRepo.DeleteOneAsync(foundCategory);

           if(IsDeleted)
           {    
            return true;
           }
           return false;

        }

        public async Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            var isUpdated = await _categoryRepo.UpdateOneAsync(foundCategory);

            if (foundCategory==null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundCategory);
            return await _categoryRepo.UpdateOneAsync(foundCategory);
            
        }

        public Task<List<CategoryReadDto>> GetAllAsynac()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryReadDto> GetByIdAsynac(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneAsync(Guid id, string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync(Guid id, string categoryName, CategoryUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneAsync(string categoryName)
        {
            throw new NotImplementedException();
        }
    }
}

