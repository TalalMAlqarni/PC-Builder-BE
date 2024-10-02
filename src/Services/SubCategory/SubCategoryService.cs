using System;
using System.Globalization;
using AutoMapper;
using src.Repository;
using src.Utils;
using static src.DTO.SubCategoryDTO;

namespace src.Services.SubCategory
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly SubCategoryRepository _subCategoryRepo;
        private readonly CategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public SubCategoryService(SubCategoryRepository subCategoryRepo, CategoryRepository categoryRepo, IMapper mapper)
        {
            _subCategoryRepo = subCategoryRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

   

        // public async Task<SubCategoryReadDto> CreateOneAsync(SubCategoryReadDto subCategory)
        // {
        //     // Create a new SubCategory entity
        //     var newSubCategory = new src.Entity.SubCategory
        //     {
        //         Name = subCategory.Name,
        //     // Assuming you're passing the CategoryId
        //     };

        //     // Save the new subcategory to the repository
        //     await _subCategoryRepo.AddAsync(newSubCategory);

        //     // Manually map the SubCategory entity to SubCategoryReadDto
        //     var subCategoryReadDto = new SubCategoryReadDto
        //     {
        //         SubCategoryId = newSubCategory.SubCategoryId,
        //         Name = newSubCategory.Name,
        //     };
        //     return subCategoryReadDto;
//         // }
// public async Task<SubCategoryReadDto> CreateOneAsync(SubCategoryCreateDto createDto)
// {
//     var category = await _categoryRepo.GetByIdAsync(createDto.CategoryId); // Make sure this works
//     if (category == null)
//     {
//         return null; // Category doesn't exist
//     }

//     var newSubCategory = new src.Entity.SubCategory
//     {
//         Name = createDto.Name,
//         CategoryId = createDto.CategoryId,
//     };

//     await _subCategoryRepo.AddAsync(newSubCategory);

//     return new SubCategoryReadDto
//     {
//         SubCategoryId = newSubCategory.SubCategoryId,
//         Name = newSubCategory.Name,
//         CategoryId = newSubCategory.CategoryId,

//     };
// } THIS ONE WORKED W/O CATEGORY NAME
public async Task<SubCategoryReadDto> CreateOneAsync(SubCategoryCreateDto createDto)
{
    // Check if the category exists
    var category = await _categoryRepo.GetByIdAsync(createDto.CategoryId);
    if (category == null)
    {
        return null; // Handle this case appropriately
    }

    // Create a new SubCategory entity
    var newSubCategory = new src.Entity.SubCategory
    {
        Name = createDto.Name,
        CategoryId = createDto.CategoryId,
    };

    // Save the new subcategory to the repository
    await _subCategoryRepo.AddAsync(newSubCategory);

    // Map the SubCategory entity to SubCategoryReadDto
    var subCategoryReadDto = new SubCategoryReadDto
    {
        SubCategoryId = newSubCategory.SubCategoryId,
        Name = newSubCategory.Name,
        CategoryId = newSubCategory.CategoryId,
        CategoryName = category.CategoryName // Include the main category name here
    };

    return subCategoryReadDto;
}


//         public async Task<SubCategoryReadDto> CreateOneAsync(SubCategoryCreateDto createDto)
// {
//     // Check if the category exists
//     var category = await _subCategoryRepo.GetByIdAsync(createDto.CategoryId);
//     if (category == null)
//     {
//         // Handle the case where the category does not exist
//         return null; // You can handle this more gracefully with an error response in the controller.
//     }

//     // Create a new SubCategory entity
//     var newSubCategory = new src.Entity.SubCategory
//     {
//         Name = createDto.Name,
//         CategoryId = createDto.CategoryId, // Associate with the existing category
//     };

//     // Save the new subcategory to the repository
//     await _subCategoryRepo.AddAsync(newSubCategory);

//     // Return the created subcategory as a SubCategoryReadDto
//     var subCategoryReadDto = new SubCategoryReadDto
//     {
//         SubCategoryId = newSubCategory.SubCategoryId,
//         Name = newSubCategory.Name,
//         CategoryId = newSubCategory.CategoryId,
//         // Category = category // Optional, include this only if you need category details in the response
//     };

//     return subCategoryReadDto;
// }


//         public async Task<SubCategoryReadDto> CreateOneAsync(SubCategoryCreateDto createDto)
// {
//     // Create a new SubCategory entity
//     var newSubCategory = new src.Entity.SubCategory
//     {
//         Name = createDto.Name,
//  // Assuming you're passing the CategoryId
//     };

//     // Save the new subcategory to the repository
//     await _subCategoryRepo.AddAsync(newSubCategory); 

//     // Map the SubCategory entity to SubCategoryReadDto
//     return _mapper.Map<SubCategoryReadDto>(newSubCategory);
// }

public async Task<List<SubCategoryReadDto>> GetAllAsync()
{
    var subCategoryList = await _subCategoryRepo.GetAllAsync();

    // Manually map the CategoryName
    var subCategoryReadDtoList = subCategoryList.Select(subCategory => new SubCategoryReadDto
    {
        SubCategoryId = subCategory.SubCategoryId,
        Name = subCategory.Name,
        CategoryId = subCategory.CategoryId,
        CategoryName = subCategory.Category.CategoryName // Map the Category Name
    }).ToList();

    return subCategoryReadDtoList;
}


        
        // public async Task<List<SubCategoryReadDto>> GetAllAsync()
        // {
        //     var subCategoryList = await _subCategoryRepo.GetAllAsync();
        //     return _mapper.Map<List<src.Entity.SubCategory>, List<SubCategoryReadDto>>(subCategoryList);
        // }

   public async Task<SubCategoryReadDto> GetByIdAsync(Guid subCategoryId)
{
    // Fetch the single subcategory by its ID, including the related Category
    var subCategory = await _subCategoryRepo.GetByIdAsync(subCategoryId);

    // If the subcategory is not found, handle that case (e.g., return null or throw an exception)
    if (subCategory == null)
    {
        return null; // Or throw an exception, depending on how you want to handle it
    }

    // Manually map the CategoryName to SubCategoryReadDto
    var subCategoryReadDto = new SubCategoryReadDto
    {
        SubCategoryId = subCategory.SubCategoryId,
        Name = subCategory.Name,
        CategoryId = subCategory.CategoryId,
        CategoryName = subCategory.Category.CategoryName // Map the Category's name
    };

    return subCategoryReadDto;
}

  public async Task<List<SubCategoryReadDto>> GetAllBySearchAsync( 
            PaginationOptions paginationOptions
        )
        {
            var subCategoryList = await _subCategoryRepo.GetAllResults(paginationOptions);
            if (subCategoryList.Count ==0)
            {
                throw CustomException.NotFound($"No results found");
            }
            return _mapper.Map<List<src.Entity.SubCategory>, List<SubCategoryReadDto>>(subCategoryList);
        }

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
            var isUpdated = await _subCategoryRepo.UpdateOneAsync(foundSubCategory);

            if (foundSubCategory == null)
            {
                // If the subcategory doesn't exist, return false (not found)
                return false;
            }

            // // Update the found subcategory with new values from updateDto
            // foundSubCategory.Name = updateDto.Name;
            // foundSubCategory.CategoryId = updateDto.Id;

            // Update the subcategory in the repository
            _mapper.Map(updateDto, foundSubCategory);

            return await _subCategoryRepo.UpdateOneAsync(foundSubCategory); // Just pass the updated entity
        }



        public Task<List<SubCategoryReadDto>> GetAllAsynac()
        {
            throw new NotImplementedException();
        }

        public Task<SubCategoryReadDto> GetByIdAsynac(Guid subCategoryId)
        {
            throw new NotImplementedException();
        }

     

        public Task CreateOneAsync(Entity.SubCategory subCategory)
        {
            throw new NotImplementedException();
        }

    }
}