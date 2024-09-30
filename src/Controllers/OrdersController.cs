using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services;
using src.Services.cart;
using src.Services.product;
using static src.DTO.OrderDTO;

namespace scr.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        protected IOrderService _orderService;
        protected ICartService _cartService;
        protected IProductService _productService;
        public static int deliveryDays = 2;
        public readonly static string[] orderStatuses = { "ordered", "shipped", "on delivery", "delivered" };

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // Gets all available orders.
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<OrderReadDTO>>> GetAllOrders()
        {
            var ordersList = await _orderService.GetAllAsync();
            return Ok(ordersList.OrderByDescending(o => o.OrderDate));
        }

        // Gets a specific order by it's ID
        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderReadDTO>> GetOrderById([FromRoute] Guid orderId)
        {
            var foundOrder = await _orderService.GetByIdAsync(orderId);
            if (foundOrder == null)
                return NotFound("order not found");
            return Ok(foundOrder);
        }
        // Gets a user's orders by its ID in ascending.
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<OrderReadDTO>>> GetOrdersByUserID([FromRoute] Guid userId)
        {
            var userOrders = await _orderService.GetByUserIdAsync(userId);
            return Ok(userOrders.OrderBy(o => o.OrderDate));
        }

        // Gets a user's old orders by its ID in descending.
        [Authorize]
        [HttpGet("user/{userId}/ordershistory")]
        public async Task<ActionResult<List<OrderReadDTO>>> GetOrdersHistoryByUserID([FromRoute] Guid userId)
        {
            var userOrders = await _orderService.GetHistoryByUserIdAsync(userId);
            return Ok(userOrders.OrderByDescending(o => o.OrderDate));
        }


        // Post new order to the order list
        [Authorize]
        [HttpPost("checkout")]
        public async Task<ActionResult<OrderReadDTO>> CreateOrder([FromBody] OrderCreateDTO newOrder)
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
            newOrder.OrderDate = DateTime.Now.ToUniversalTime();
            newOrder.ShipDate = DateTime.Now.AddDays(deliveryDays).ToUniversalTime();
            newOrder.OrderStatus = "Ordered";
            newOrder.IsDelivered = false;
            var createdOrder = await _orderService.CreateOneAsync(newOrder);

            //var cart = await _cartService.GetCartByIdAsync(createdOrder.CartId);

            return Created($"api/v1/orders/{createdOrder.Id}", createdOrder);
        }

        // Update current order status into ("shipped", "on delivery", "delivered")
        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/orderstatus")]
        public async Task<ActionResult> UpdateOrderStatus(Guid orderId, OrderUpdateDTO updatedOrder)
        {
            bool foundOrderStatus = false;
            foreach (string status in orderStatuses)
            {
                if (updatedOrder.OrderStatus.Equals(status, StringComparison.OrdinalIgnoreCase))
                {
                    foundOrderStatus = true;
                    break;
                }
            }

            if (!foundOrderStatus)
                return NotFound("Invalid order status");

            // if order is delivered to the user
            if (updatedOrder.OrderStatus.Equals("delivered", StringComparison.OrdinalIgnoreCase))
                updatedOrder.IsDelivered = true;

            bool isUpdated = await _orderService.UpdateOneAsync(orderId, updatedOrder);

            return isUpdated ? NoContent() : NotFound("Order ID not found");

        }

        // Updates the ship date into new one
        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/shipdate")]
        public async Task<ActionResult> UpdateShipDate(Guid orderId, OrderUpdateDTO updatedOrder)
        {
            if (updatedOrder.ShipDate < DateTime.Now)
                return BadRequest("Invalid ship date");

            bool isUpdated = await _orderService.UpdateOneAsync(orderId, updatedOrder);

            return isUpdated ? NoContent() : NotFound("Order ID not found");
        }


        // Cancel the current order by deleting it from orders list
        [Authorize]
        [HttpDelete("{orderId}")]
        public async Task<ActionResult> CancelOrder(Guid orderId)
        {
            var isDeleted = await _orderService.DeleteOneAsync(orderId);
            return isDeleted ? NoContent() : NotFound("Order ID not found");
        }
    }
}