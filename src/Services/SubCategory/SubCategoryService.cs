using System;
using AutoMapper;
using src.Repository;
using static src.DTO.SubCategoryDTO;

namespace src.Services.SubCategory
{
    public class SubCategoryService : ISubCategoryService
    {
        protected readonly SubCategoryRepository _subCategoryRepo;
        protected readonly IMapper _mapper;
        public SubCategoryService (SubCategoryRepository subCategoryRepo, IMapper mapper)
        {
            _subCategoryRepo = subCategoryRepo;
            _mapper = mapper;
        }

        public async Task <SubCategoryReadDto> CreateOneAsync(SubCategoryCreateDto createDto)
        {
            var subCategory = _mapper.Map <SubCategoryCreateDto, src.Entity.SubCategory>(createDto);
            var subCategoryCreated = await _subCategoryRepo.CreateOneAsync(subCategory);
            return _mapper.Map <src.Entity.SubCategory,SubCategoryReadDto> (subCategoryCreated);
        }

        public async Task<List<SubCategoryReadDto>> GetAllAsync()
        {
            var subCategoryList = await _subCategoryRepo.GetAllAsync();
            return _mapper.Map<List<src.Entity.SubCategory>, List<SubCategoryReadDto>>(subCategoryList);
        }

        public async Task<SubCategoryReadDto> GetByIdAsync(Guid subCategoryId)
        {
            var foundSubCategory = await _subCategoryRepo.GetByIdAsync(subCategoryId);
            return _mapper.Map<src.Entity.SubCategory, SubCategoryReadDto> (foundSubCategory);
        }

        public async Task<bool> DeleteOneAsync(Guid subCategoryId)
        {
            var foundSubCategory = await _subCategoryRepo.GetByIdAsync(subCategoryId);
           bool IsDeleted =await _subCategoryRepo.DeleteOneAsync(foundSubCategory);

           if(IsDeleted)
           {    
            return true;
           }
           return false;

        }

        public async Task<bool> UpdateOneAsync(Guid subCategoryId, SubCategoryUpdateDto updateDto)
        {
            var foundSubCategory = await _subCategoryRepo.GetByIdAsync(subCategoryId);
            var isUpdated = await _subCategoryRepo.UpdateOneAsync(foundSubCategory);

            if (foundSubCategory==null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundSubCategory);
            return await _subCategoryRepo.UpdateOneAsync(foundSubCategory); 
        }

        public Task<List<SubCategoryReadDto>> GetAllAsynac()
        {
            throw new NotImplementedException();
        }

        public Task<SubCategoryReadDto> GetByIdAsynac(Guid subCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}