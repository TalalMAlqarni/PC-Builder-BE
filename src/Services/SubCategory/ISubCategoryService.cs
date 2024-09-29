using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static src.DTO.SubCategoryDTO;

namespace src.Services.SubCategory
{
    public interface ISubCategoryService
    {
        Task<SubCategoryReadDto> CreateOneAsync(SubCategoryCreateDto createDto);
        Task<List<SubCategoryReadDto>> GetAllAsynac();

        Task<SubCategoryReadDto> GetByIdAsynac(Guid subCategoryId);

        Task<bool> DeleteOneAsync(Guid subCategoryId);
        Task<bool> UpdateOneAsync(Guid subCategoryId, SubCategoryUpdateDto updateDto);
        Task<bool> DeleteOneAsync(string name);
    }
}