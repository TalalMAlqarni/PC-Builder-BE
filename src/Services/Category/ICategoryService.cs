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

        Task<CategoryReadDto> GetByIdAsynac(Guid Id);

        Task<bool> DeleteOneAsync(Guid Id, string categoryName);
        Task<bool> UpdateOneAsync(Guid Id, string categoryName,CategoryUpdateDto updateDto);
        Task<bool> DeleteOneAsync(string categoryName);
    }
}