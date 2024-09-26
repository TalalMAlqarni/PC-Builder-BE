using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Repository;

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
        public readonly static string[] orderStatuses = { "ordered", "shipped", "on delivery", "delivered" };

        // Gets all available orders.
        [HttpGet]
        public ActionResult GetOrders()
        {
            return Ok(orders.OrderByDescending(o => o.OrderDate));
        }


        // Gets a user's orders by its ID in ascending.
        [HttpGet("{id}")]
        public ActionResult GetOrdersByUserID(Guid id)
        {
            List<Order> userOrders = orders.FindAll(o => o.UserId == id);
            return Ok(userOrders.OrderBy(o => o.OrderDate));
        }

        // Gets a user's old orders by its ID in descending.
        [HttpGet("{id}/ordershistory")]
        public ActionResult GetOrdersHistoryByUserID(Guid id)
        {
            List<Order> userOrders = orders.FindAll(o => o.UserId == id && o.IsDelivered);
            return Ok(userOrders.OrderByDescending(o => o.OrderDate));
        }


        // Post new order to the order list
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



            // initialize new entry
            newOrder.Id = Guid.NewGuid();
            newOrder.OrderDate = DateTime.Now;
            newOrder.ShipDate = DateTime.Now.AddDays(deliveryDays);
            newOrder.OrderStatus = "Ordered";
            newOrder.IsDelivered = false;
            //orders.Add(newOrder);

            return CreatedAtAction(nameof(GetOrders), new { id = newOrder.Id }, newOrder);
        }

        // Update current order status into ("shipped", "on delivery", "delivered")
        [HttpPut("{id}/orderstatus/{orderStatus}")]
        public ActionResult UpdateOrderStatus(Guid id, string orderStatus)
        {
            Order? foundOrder = orders.FirstOrDefault(o => o.Id == id);
            if (foundOrder == null)
                return NotFound("Invalid ID instance");

            bool foundOrderStatus = false;
            foreach (string status in orderStatuses)
            {
                if (orderStatus.Equals(status, StringComparison.OrdinalIgnoreCase))
                {
                    foundOrderStatus = true;
                    break;
                }
            }

            if (!foundOrderStatus)
                return NotFound("Invalid order status");

            // if order is delivered to the user
            if (orderStatus.Equals("delivered", StringComparison.OrdinalIgnoreCase))
                foundOrder.IsDelivered = true;

            foundOrder.OrderStatus = orderStatus;
            return NoContent();
        }

        // Updates the ship date into new one
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


        // Cancel the current order by deleting it from orders list
        [HttpDelete("{id}")]
        public ActionResult CancelOrder(Guid id)
        {
            Order? foundOrder = orders.FirstOrDefault(o => o.Id == id);
            if (foundOrder == null)
                return NotFound("Invalid ID instance");
            orders.Remove(foundOrder);

            return NoContent();
        }
    }
}