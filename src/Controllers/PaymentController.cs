using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Controller;
using src.Services.Payment;
using src.Repository;
using static src.DTO.PaymentDTO;

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
        // GET method to retrive all payments
        // [HttpGet]
        // public ActionResult GetPayments()
        // {
        //     return Ok(payments);
        // }

        // // GET method by payment method
        // [HttpGet("{PaymentMethod}")]
        // public ActionResult GetPaymentByMethodName(string paymentMethod)
        // {
        //     Payment? foundPayment = payments.FirstOrDefault(p => p.PaymentMethod.Equals(paymentMethod, StringComparison.OrdinalIgnoreCase));
        //     if (foundPayment == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(foundPayment);
        // }
        [HttpGet]
        public async Task<ActionResult<List< PaymentCreateDto>>>GetAllAsync()
        {
        
            var paymentList = await _paymentService.GetAllAsync();
            return Ok(paymentList);
        }
             
        [HttpGet("{paymentId}")]
        public async Task<ActionResult<PaymentReadDto>>GetByIdAsync([FromRoute] Guid paymentId)
        {
            var payment = await _paymentService.GetByIdAsync (paymentId);
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentReadDto>> CreateOne([FromBody] PaymentCreateDto createDto)
        {
            var paymentCreated = await _paymentService.CreateOneAsync(createDto);
            // return Created(categoryCreated);
            return Created($"api/v1//payments/{paymentCreated.PaymentId}",paymentCreated);
        }

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

        // POST method to add payment methods
        // [HttpPost]
        // public ActionResult AddPaymentMethod([FromBody] Payment newPaymentMethod)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     // Check if a payment with the same name already exists
        //     var existingPayment = payments.FirstOrDefault(p => p.PaymentMethod.Equals(newPaymentMethod.PaymentMethod, StringComparison.OrdinalIgnoreCase));
        //     if (existingPayment != null)
        //     {
        //         return Conflict($"A payment method with the name '{newPaymentMethod.PaymentMethod}' already exists.");
        //     }
        //     newPaymentMethod.PaymentId = Guid.NewGuid();
        //     // Add the new category to the list
        //     payments.Add(newPaymentMethod);
        //     return CreatedAtAction(nameof(GetPaymentByMethodName), new { PaymentMethod = newPaymentMethod.PaymentMethod }, newPaymentMethod);
        // }

        // PUT (Update) method by OrderId
        // [HttpPut("{PaymentMethod}")]
        // public ActionResult UpdatePayment(Guid OrderId, [FromBody] Payment updatedPayment)
        // {
        //     var existingPayment = payments.FirstOrDefault(p => p.OrderId == OrderId);
        //     if (existingPayment == null)
        //     {
        //         return NotFound($"Payment with OrderId '{OrderId}' not found.");
        //     }
        //     // Update the payment details
        //     existingPayment.PaymentMethod = updatedPayment.PaymentMethod;
        //     existingPayment.PaymentDate = updatedPayment.PaymentDate;
        //     existingPayment.PaymentStatus = updatedPayment.PaymentStatus;
        //     existingPayment.TotalPrice = updatedPayment.TotalPrice;
        //     existingPayment.CartId = updatedPayment.CartId;  // Assuming CartId might change
        //     return NoContent();
        // }

        // DELETE method by OrderId
        // [HttpDelete("{PaymentMethod}")]
        // public ActionResult DeletePayment(Guid OrderId)
        // {
        //     var paymentToDelete = payments.FirstOrDefault(p => p.OrderId == OrderId);
        //     if (paymentToDelete == null)
        //     {
        //         return NotFound($"Payment with OrderId '{OrderId}' not found.");
        //     }
        //     // Remove the payment from the list
        //     payments.Remove(paymentToDelete);
        //     return NoContent();
        // }
    }

}
