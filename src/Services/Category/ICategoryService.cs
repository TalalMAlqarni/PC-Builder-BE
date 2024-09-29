using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static src.DTO.CategoryDTO;

namespace src.Services.Category
{
    public interface ICategoryService
    {
        Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto);
        Task<List<CategoryReadDto>> GetAllAsynac();

        Task<CategoryReadDto> GetByIdAsynac(Guid id);

        Task<bool> DeleteOneAsync(Guid id, string categoryName);
        Task<bool> UpdateOneAsync(Guid id, string categoryName,CategoryUpdateDto updateDto);
    }
}