using src.Entity;
using static src.DTO.SpecificationsDTO;

namespace src.Services.specifications
{
    public interface ISpecificationsService
    {
        // Create new specification
        Task<ReadSpecificationsDto> CreateSpecificationAsync(CreateSpecificationsDto createDto);

        // Get all specifications
        Task<List<ReadSpecificationsDto>> GetAllSpecificationsAsync();

        // Get specification by id
        Task<ReadSpecificationsDto> GetSpecificationByIdAsync(Guid id);

        // Get specification by product id
        Task<ReadSpecificationsDto> GetSpecificationByProductIdAsync(Guid productId);

        // Delete specification
        Task<bool> DeleteSpecificationAsync(Guid id);

        // Update specification
        Task<ReadSpecificationsDto> UpdateSpecificationAsync(Guid id, UpdateSpecificationsDto updateDto);
    }
}
