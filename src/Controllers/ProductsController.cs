using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controller
{
    [ApiController]
    //[Route("api/v1/categories/specific_category/subcategories/specific_subcategory)]
    [Route("api/v1/[controller]")]
    class ProductsController : ControllerBase
    {
        /*The standard user privileges:
        view the product/products
        search for product by name
        sort products by range/price/...etc
        add the product to the cart
         */



        public static List<Product> products = new List<Product> { };


        // view all the products in specific subcategory:
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            return Ok(products);
        }

        //view a specific product by Id
        [HttpGet("{id}")]
        public ActionResult GetProductById(Guid id)
        {
            Product? isFound = products.FirstOrDefault(x => x.ProductId == id);

            if (isFound == null)
            {
                return NotFound();
            }

            return Ok(isFound);
        }

        //add product to the cart:


        //search on a specific product byname


        /* the admin user privileges:
        view the product/products
        add a new product
        delete a product
        edit/update on the product info
*/

        //delete a specific product

        [HttpDelete("{id}")]
        public ActionResult DeleteProductById(Guid id)
        {
            Product? isFound = products.FirstOrDefault(x => x.ProductId == id);

            if (isFound == null)
            {
                return NotFound();
            }
            products.Remove(isFound);
            return NoContent();
        }

        // [HttpPut("{id}")]
        // public ActionResult UpdateProductInfo(){
 
        // }
        
       

           
        }
    }
