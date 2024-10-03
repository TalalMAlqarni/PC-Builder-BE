using System;
using System.Globalization;
using AutoMapper;
using src.Repository;
using src.Utils;
using src.Services;

using static src.DTO.SubCategoryDTO;
using static src.DTO.ProductDTO;
using src.Database;

namespace src.Services.SubCategory
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly SubCategoryRepository _subCategoryRepo;
        private readonly CategoryRepository _categoryRepo;
        private readonly ProductRepository _productRepository;

        private readonly IMapper _mapper;

        public SubCategoryService(ProductRepository productRepository,SubCategoryRepository subCategoryRepo, CategoryRepository categoryRepo, IMapper mapper)
        { 
             _productRepository = productRepository;     
            _subCategoryRepo = subCategoryRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<SubCategoryReadDto> CreateOneAsync(SubCategoryCreateDto createDto)
        {
            // Check if the category exists
            var category = await _categoryRepo.GetByIdAsync(createDto.CategoryId);
            if (category == null)
            {
                throw new Exception("Category does not exist."); //TRYING TO USE CUSTOME Ex
            }

            // Create a new SubCategory entity
            var newSubCategory = new src.Entity.SubCategory
            {
                Name = createDto.Name,
                CategoryId = createDto.CategoryId,
                Products= createDto.Products
            };

            // Save the new subcategory to the repository
            var savedSubCategory = await _subCategoryRepo.AddAsync(newSubCategory);

            // Map the entity to SubCategoryReadDto
            var subCategoryReadDto = new SubCategoryReadDto
            {
                SubCategoryId = savedSubCategory.SubCategoryId,
                Name = savedSubCategory.Name,
                CategoryId = savedSubCategory.CategoryId,
                CategoryName = category.CategoryName 
            };

            return subCategoryReadDto;
        }

        public async Task<List<SubCategoryReadDto>> GetAllAsync()
        {
            var subCategoryList = await _subCategoryRepo.GetAllAsync();

            // Manually map the CategoryName
            var subCategoryReadDtoList = subCategoryList.Select(subCategory => new SubCategoryReadDto
            {
                SubCategoryId = subCategory.SubCategoryId,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                CategoryName = subCategory.Category.CategoryName, // Map the Category Name
                Products = subCategory.Products.Select(product => new GetProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductColor = product.ProductColor,
                    Description = product.Description,
                    SKU = product.SKU,
                    ProductPrice = product.ProductPrice,
                    Weight = product.Weight,
                    // SubCategoryId = product.SubCategoryId
                }).ToList() // Map products here
            }).ToList();

            return subCategoryReadDtoList;
        }
        public async Task<SubCategoryReadDto?> GetSubCategoryByIdAsync(Guid subCategoryId)
        {
            var subCategory = await _subCategoryRepo.GetByIdAsync(subCategoryId);


            if (subCategory == null)
            {
                return null; // Handle the case where subcategory is not found
            }
            // Map subcategory to SubCategoryReadDto and include the products
            return new SubCategoryReadDto
            {
                SubCategoryId = subCategory.SubCategoryId,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                CategoryName = subCategory.Category?.CategoryName, // Safeguard null references
                Products = subCategory.Products.Select(p => new GetProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductColor = p.ProductColor,
                    Description = p.Description,
                    SKU = p.SKU,
                    ProductPrice = p.ProductPrice,
                    Weight = p.Weight,
                }).ToList()
            };
        }

        // public async Task<List<SubCategoryReadDto>> GetAllBySearchAsync(PaginationOptions paginationOptions)
        // {
        //     var subCategoryList = await _subCategoryRepo.GetAllResults(paginationOptions);
        //     if (subCategoryList.Count == 0)
        //     {
        //         throw CustomException.NotFound($"No results found");
        //     }

        //     // Mapping to SubCategoryReadDto
        //     return subCategoryList.Select(sc => new SubCategoryReadDto
        //     {
        //         SubCategoryId = sc.SubCategoryId,
        //         CategoryId = sc.CategoryId,
        //         Name = sc.Name,
        //         CategoryName = sc.Category?.CategoryName // Ensure Category is not null
        //     }).ToList();
        // }

        public async Task<bool> DeleteOneAsync(Guid subCategoryId)
        {
            // Retrieve the subcategory by ID
            var foundSubCategory = await _subCategoryRepo.GetByIdAsync(subCategoryId);

            // Check if the subcategory exists
            if (foundSubCategory == null)
            {
                return false; // Subcategory not found
            }

            // Perform the deletion
            return await _subCategoryRepo.DeleteOneAsync(foundSubCategory);
        }

        public async Task<bool> UpdateOneAsync(Guid subCategoryId, SubCategoryUpdateDto updateDto)
        {
            // Retrieve the existing subcategory by its ID
            var foundSubCategory = await _subCategoryRepo.GetByIdAsync(subCategoryId);
            if (foundSubCategory == null)
            {
                // If the subcategory doesn't exist, return false (not found)
                return false;
            }
            // Update the subcategory in the repository
            _mapper.Map(updateDto, foundSubCategory);

            return await _subCategoryRepo.UpdateOneAsync(foundSubCategory); // Just pass the updated entity
        }

        public Task<List<SubCategoryReadDto>> GetAllBySearchAsync(PaginationOptions paginationOptions)
        {
            throw new NotImplementedException();
        }
    }
}