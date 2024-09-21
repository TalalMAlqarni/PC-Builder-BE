using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace sda_3_online_Backend_Teamwork.src.Entity
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        public List<Product> Products = new List<Product>
        {
            new Product
            {
                ProductId = new Guid(),
                ProductName = "Sofa",
                ProductColor = "Black",
                SKU = 10,
                Quantity = 1,
                ProductPrice = 150.00m,
                Subtotal = 150.00m,
                Weight = 3,
            },
            new Product
            {
                ProductId = new Guid(),
                ProductName = "Sofa2",
                ProductColor = "Gray",
                SKU = 12,
                Quantity = 2,
                ProductPrice = 160.00m,
                Subtotal = 160.00m,
                Weight = 3,
            },
        };
        public static List<Cart> Carts = new List<Cart>();

        [HttpGet]
        public ActionResult GetCarts()
        {
            return Ok(Carts);
        }

        [HttpPost]
        public ActionResult CreateCart(Cart newCart)
        {
            newCart.Id = Guid.NewGuid();
            Carts.Add(newCart);
            return CreatedAtAction(nameof(GetCarts), new { id = newCart.Id }, newCart);
        }
    }
}
