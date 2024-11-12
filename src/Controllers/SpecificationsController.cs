using Microsoft.AspNetCore.Mvc;
using src.Services.specifications;
using static src.DTO.SpecificationsDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpecificationsController : ControllerBase
    {
        protected ISpecificationsService _specificationsService;

        public SpecificationsController(ISpecificationsService specificationsService)
        {
            _specificationsService = specificationsService;
        }

        // Get all specifications: GET api/v1/specifications
        [HttpGet]
        public async Task<ActionResult<List<ReadSpecificationsDto>>> GetAllSpecifications()
        {
            var specifications = await _specificationsService.GetAllSpecificationsAsync();
            return Ok(specifications);
        }

        // Get specification by id: GET api/v1/specifications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadSpecificationsDto>> GetSpecificationById(Guid id)
        {
            var specification = await _specificationsService.GetSpecificationByIdAsync(id);
            return Ok(specification);
        }

        // Get specification by product id: GET api/v1/specifications/product/{id}
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ReadSpecificationsDto>> GetSpecificationByProductId(Guid id)
        {
            var specification = await _specificationsService.GetSpecificationByProductIdAsync(id);
            return Ok(specification);
        }

        // Create new specification: POST api/v1/specifications
        [HttpPost]
        public async Task<ActionResult<ReadSpecificationsDto>> CreateSpecification(CreateSpecificationsDto createDto)
        {
            var specification = await _specificationsService.CreateSpecificationAsync(createDto);
            return CreatedAtAction(nameof(GetSpecificationById), new { id = specification.Id }, specification);
        }

        // Update specification: PUT api/v1/specifications/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadSpecificationsDto>> UpdateSpecification(Guid id, UpdateSpecificationsDto updateDto)
        {
            var specification = await _specificationsService.UpdateSpecificationAsync(id, updateDto);
            return Ok(specification);
        }

        // Delete specification: DELETE api/v1/specifications/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteSpecification(Guid id)
        {
            var isDeleted = await _specificationsService.DeleteSpecificationAsync(id);
            return Ok(isDeleted);
        }
    }
}
