using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace scr.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private List<Order> _orders = new List<Order>() {
            new Order
            {
            Id = Guid.NewGuid(),
            PaymentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            OrderDate = DateTime.Now,
            ShipDate = DateTime.Now.AddDays(7),
            OrderStatus="Ordered",
            Address = "some address",
            City = "some city",
            State="some state",
            PostalCode=12345
            }
         };
        private int _deliveryDays = 7;
        [HttpGet]
        public ActionResult GetOrders()
        {
            return Ok(_orders);
        }
        [HttpPost("checkout")]
        public ActionResult CreateOrder(Order newOrder)
        {
            newOrder.Id = Guid.NewGuid();
            newOrder.OrderDate = DateTime.Now;
            newOrder.ShipDate = DateTime.Now.AddDays(_deliveryDays);
            newOrder.OrderStatus = "Ordered";
            _orders.Add(newOrder);
            return CreatedAtAction(nameof(GetOrders), new { id = newOrder.Id }, newOrder);
        }
        /*[HttpDelete]
        public ActionResult CancelOrder(Guid id)
        {
            var delOrder = _orders.FirstOrDefault(x => x.Id.)
            return "";    
        }*/
    }
}