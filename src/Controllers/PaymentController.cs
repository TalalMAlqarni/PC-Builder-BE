using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Controller;
using src.Services.Payment;
using src.Repository;
using static src.DTO.PaymentDTO;
using Microsoft.AspNetCore.Authorization;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        protected readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService service)
        {
            _paymentService = service;
        }
      
        [Authorize]     
        [HttpGet]
        public async Task<ActionResult<List< PaymentCreateDto>>>GetAllAsync()
        {
            var paymentList = await _paymentService.GetAllAsync();
            return Ok(paymentList);
        }

        [Authorize(Roles = "Admin")] // Only Admins can view specific payments
        [HttpGet("{paymentId}")]
        public async Task<ActionResult<PaymentReadDto>>GetByIdAsync([FromRoute] Guid paymentId)
        {
            var payment = await _paymentService.GetByIdAsync (paymentId);
            return Ok(payment);
        }

        [Authorize(Roles = "Admin, User")] // Only Admins or Users can make payments
        [HttpPost]
        public async Task<ActionResult<PaymentReadDto>> CreateOne([FromBody] PaymentCreateDto createDto)
        {
            var paymentCreated = await _paymentService.CreateOneAsync(createDto);
            // return Created(categoryCreated);
            return Created($"api/v1//payments/{paymentCreated.PaymentId}",paymentCreated);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{paymentId}")]
        public async Task<ActionResult<PaymentReadDto>> UpdateOneAsync([FromRoute] Guid paymentId,[FromBody] PaymentUpdateDto updateDto)
        {
            var result = await _paymentService.UpdateOneAsync(paymentId, updateDto);
            if (result == null)
            {
                return NotFound($"Payment with ID = {paymentId} not found.");
            }
            var updatedPayment = await _paymentService.GetByIdAsync(paymentId); // Assuming you have a method to fetch the updated category
            return Ok(updatedPayment);
        }
        
        [Authorize(Roles = "Admin")] 
        [HttpDelete("{paymentId}")]
        public async Task<IActionResult> DeleteOneAsync([FromRoute] Guid paymentId)
        {
            var result = await _paymentService.DeleteOneAsync(paymentId);
            if (!result)
            {
                return NotFound($"Payment with ID = {paymentId} not found.");
            }
            return NoContent(); // 204 No Content
        }
    }

}
