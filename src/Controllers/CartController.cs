using Microsoft.AspNetCore.Mvc;

namespace sda_3_online_Backend_Teamwork.src.Entity
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        public List<Product> Products = new List<Product>{
            new Product { Name = "P1", Price = 100, Quantity = 10},
            new Product { Name = "P2", Price = 200, Quantity = 20},
            new Product { Name = "P3", Price = 300, Quantity = 30},
        };
        public static List<Cart> Carts = new List<Cart>();

//         [HttpGet]
//         public ActionResult GetCarts()
//         {
//             return Ok(Carts);
//         }
//         [HttpPost]
//         public ActionResult CreateCart(Cart newCart)
//         {
//             newCart.Id = Guid.NewGuid();
//             Carts.Add(newCart);
//             return CreatedAtAction(nameof(GetCarts), new { id = newCart.Id }, newCart);
//         }
//     }
// }