using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
        public class PaymentController : ControllerBase
        {
            public static List<Payment> payments = new List<Payment>
            {
                new Payment
                {
                    PaymentId = Guid.NewGuid(),
                    PaymentMethod = "Credit Card",
                    PaymentDate = DateTime.Now,
                    PaymentStatus = true,
                    TotalPrice = 150.00M,
                    CartId = CartController.Carts.Count > 0 ? CartController.Carts[0].Id : Guid.Empty, // Use cart if available
                    OrderId = OrdersController.orders[0].Id
                },
                new Payment
                {
                    PaymentId = Guid.NewGuid(),
                    PaymentMethod = "PayPal",
                    PaymentDate = DateTime.Now,
                    PaymentStatus = true,
                    TotalPrice = 250.00M,
                    CartId = CartController.Carts.Count > 1 ? CartController.Carts[1].Id : Guid.Empty, // Check if second cart exists
                    OrderId = OrdersController.orders[0].Id 
                }
            };

        // GET method to retrive all payments
        [HttpGet]
        public ActionResult GetPayments()
        {
            return Ok(payments);
        }

        // GET method by payment method
        [HttpGet("{PaymentMethod}")]
        public ActionResult GetPaymentByMethodName(string paymentMethod)
        {
            Payment? foundPayment= payments.FirstOrDefault(p => p.PaymentMethod.Equals(paymentMethod, StringComparison.OrdinalIgnoreCase));
            if (foundPayment == null)
            {
                return NotFound();
            }
            return Ok(foundPayment);
        }

        // POST method to add payment methods
        [HttpPost]
        public ActionResult AddPaymentMethod([FromBody] Payment newPaymentMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check if a payment with the same name already exists
            var existingPayment = payments.FirstOrDefault(p => p.PaymentMethod.Equals(newPaymentMethod.PaymentMethod, StringComparison.OrdinalIgnoreCase));
            if (existingPayment != null)
            {
                return Conflict($"A payment method with the name '{newPaymentMethod.PaymentMethod}' already exists.");
            }
            newPaymentMethod.PaymentId = Guid.NewGuid();
            // Add the new category to the list
            payments.Add(newPaymentMethod);
            return CreatedAtAction(nameof(GetPaymentByMethodName), new { PaymentMethod = newPaymentMethod.PaymentMethod }, newPaymentMethod);
        }  


        // PUT (Update) method by OrderId
        [HttpPut("{PaymentMethod}")]
        public ActionResult UpdatePayment(Guid OrderId, [FromBody] Payment updatedPayment)
        {
            var existingPayment = payments.FirstOrDefault(p => p.OrderId == OrderId);
            if (existingPayment == null)
            {
                return NotFound($"Payment with OrderId '{OrderId}' not found.");
            }
            // Update the payment details
            existingPayment.PaymentMethod = updatedPayment.PaymentMethod;
            existingPayment.PaymentDate = updatedPayment.PaymentDate;
            existingPayment.PaymentStatus = updatedPayment.PaymentStatus;
            existingPayment.TotalPrice = updatedPayment.TotalPrice;
            existingPayment.CartId = updatedPayment.CartId;  // Assuming CartId might change
            return NoContent(); 
        }

        // DELETE method by OrderId
        [HttpDelete("{PaymentMethod}")]
        public ActionResult DeletePayment(Guid OrderId)
        {
            var paymentToDelete = payments.FirstOrDefault(p => p.OrderId == OrderId);
            if (paymentToDelete == null)
            {
                return NotFound($"Payment with OrderId '{OrderId}' not found.");
            }
            // Remove the payment from the list
            payments.Remove(paymentToDelete);
            return NoContent();
        }
    }
    
}