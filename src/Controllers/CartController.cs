using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace sda_3_online_Backend_Teamwork.src.Entity
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        public List<Product> Products = new List<Product>{
            new Product { ProductName = "P1", ProductPrice = 100, Quantity = 10},
            new Product { ProductName = "P2", ProductPrice = 200, Quantity = 20},
            new Product { ProductName = "P3", ProductPrice = 300, Quantity = 30},
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