using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace scr.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        public static List<Order> orders = new List<Order>() {
            // Testing instance
            new Order
            {
            Id = Guid.NewGuid(),
            CartId = Guid.NewGuid(),
            PaymentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            OrderDate = DateTime.Now,
            ShipDate = DateTime.Now.AddDays(deliveryDays),
            OrderStatus="Ordered",
            Address = "some address",
            City = "some city",
            State="some state",
            PostalCode=12345
            }
         };
        public static int deliveryDays = 2;

        [HttpGet]
        public ActionResult GetOrders()
        {
            return Ok(orders.OrderByDescending(o => o.OrderDate));
        }

        [HttpGet("{id}")]
        public ActionResult GetOrdersByUserID(Guid id)
        {
            List<Order> userOrders = orders.FindAll(o => o.UserId == id);
            return Ok(userOrders.OrderByDescending(o => o.OrderDate));
        }

        [HttpPost("checkout")]
        public ActionResult CreateOrder(Order newOrder)
        {
            // validate entries
            if (newOrder.UserId == Guid.Empty)
                return BadRequest("Empty userId");
            if (newOrder.CartId == Guid.Empty)
                return BadRequest("Empty cartId");
            if (newOrder.PaymentId == Guid.Empty)
                return BadRequest("Empty paymentId");
            if (string.IsNullOrEmpty(newOrder.Address))
                return BadRequest("Empty address");
            if (string.IsNullOrEmpty(newOrder.City))
                return BadRequest("Empty city");
            if (string.IsNullOrEmpty(newOrder.State))
                return BadRequest("Empty state");
            if (newOrder.PostalCode == 0)
                return BadRequest("Empty postalCode");

            newOrder.Id = Guid.NewGuid();
            newOrder.OrderDate = DateTime.Now;
            newOrder.ShipDate = DateTime.Now.AddDays(deliveryDays);
            newOrder.OrderStatus = "Ordered";
            newOrder.IsDelivered = false;
            orders.Add(newOrder);
            return CreatedAtAction(nameof(GetOrders), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id}/orderstatus/{orderStatus}")]
        public ActionResult UpdateOrderStatus(Guid id, string orderStatus)
        {
            Order? foundOrder = orders.FirstOrDefault(o => o.Id == id);
            if (foundOrder == null)
                return BadRequest("Invalid ID instance");

            foundOrder.OrderStatus = orderStatus;
            return NoContent();
        }

        [HttpPut("{id}/shipdate/{shipDate:datetime}")]
        public ActionResult UpdateShipDate(Guid id, DateTime shipDate)
        {
            Order? foundOrder = orders.FirstOrDefault(o => o.Id == id);
            if (foundOrder == null)
                return NotFound("Invalid ID instance");

            if (shipDate < DateTime.Now)
                return BadRequest("Invalid ship date");

            foundOrder.ShipDate = shipDate;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult CancelOrder(Guid id)
        {
            Order? foundOrder = orders.FirstOrDefault(o => o.Id == id);
            if (foundOrder == null)
                return BadRequest("Invalid ID instance");
            orders.Remove(foundOrder);

            return NoContent();
        }
    }
}