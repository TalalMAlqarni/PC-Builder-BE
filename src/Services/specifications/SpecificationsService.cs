using AutoMapper;
using src.DTO;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.SpecificationsDTO;

namespace src.Services.specifications
{
    public class SpecificationsService : ISpecificationsService
    {
        protected readonly SpecificationsRepository _specificationsRepo;
        protected readonly IMapper _mapper;

        public SpecificationsService(SpecificationsRepository specificationsRepo, IMapper mapper)
        {
            _specificationsRepo = specificationsRepo;
            _mapper = mapper;
        }

        public async Task<ReadSpecificationsDto> CreateSpecificationAsync(CreateSpecificationsDto createDto)
        {
            var specifications = _mapper.Map<Specifications>(createDto);


            var specificationCreated = await _specificationsRepo.CreateSpecificationAsync(specifications);
            return _mapper.Map<Specifications, ReadSpecificationsDto>(specificationCreated);
        }

        public async Task<bool> DeleteSpecificationAsync(Guid id)
        {
            var foundSpecification = await _specificationsRepo.GetSpecificationByIdAsync(id);

            if (foundSpecification == null)
                throw CustomException.NotFound($"Specification with ID {id} not found");

            bool isDeleted = await _specificationsRepo.DeleteSpecificationAsync(foundSpecification);
            return isDeleted;
        }

        public async Task<List<ReadSpecificationsDto>> GetAllSpecificationsAsync()
        {
            var specifications = await _specificationsRepo.GetAllSpecificationsAsync();
            return _mapper.Map<List<Specifications>, List<ReadSpecificationsDto>>(specifications);
        }

        public async Task<ReadSpecificationsDto> GetSpecificationByIdAsync(Guid id)
        {
            var foundSpecification = await _specificationsRepo.GetSpecificationByIdAsync(id);

            if (foundSpecification == null)
                throw CustomException.NotFound($"Specification with ID {id} not found");

            return _mapper.Map<Specifications, ReadSpecificationsDto>(foundSpecification);
        }

        public async Task<ReadSpecificationsDto> GetSpecificationByProductIdAsync(Guid productId)
        {
            var foundSpecification = await _specificationsRepo.GetSpecificationByProductIdAsync(productId);

            if (foundSpecification == null)
                throw CustomException.NotFound($"Specification with Product ID {productId} not found");

            return _mapper.Map<Specifications, ReadSpecificationsDto>(foundSpecification);
        }

        public async Task<ReadSpecificationsDto> UpdateSpecificationAsync(Guid id, UpdateSpecificationsDto updateDto)
        {
            var foundSpecification = await _specificationsRepo.GetSpecificationByIdAsync(id);

            if (foundSpecification == null)
                throw CustomException.NotFound("Specification not found");

            _mapper.Map(updateDto, foundSpecification);

            var specificationUpdated = await _specificationsRepo.UpdateSpecificationAsync(foundSpecification);
            return _mapper.Map<Specifications, ReadSpecificationsDto>(specificationUpdated);
        }
    }
}
