using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        /*The standard user privileges:
        view the product/products
        search for product by name
        sort products by range/price/...etc
        add the product to the cart
         */

        /* the admin user privileges:
       view the product/products
       add a new product
       delete a product
       edit/update on the product info
*/



        public static List<Product> products = new List<Product>
        {
            new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Sofa",
                ProductColor = "Black",
                SKU = 10,
                ProductPrice = 150.00m,
                Weight = 3,
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Sofa2",
                ProductColor = "Gray",
                SKU = 12,
                ProductPrice = 160.00m,
                Weight = 3,
            },
        };

        // view all the products in specific subcategory:
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            return Ok(products);
        }

        //view a specific product by Id
        [HttpGet("{id:guid}")]
        public ActionResult GetProductById(Guid id)
        {
            Product? isFound = products.FirstOrDefault(x => x.ProductId == id);

            if (isFound == null)
            {
                return NotFound();
            }

            return Ok(isFound);
        }

        //search on a specific product byname
        [HttpGet("{name}")] //?
        public ActionResult GetProductByName(string name)
        {
            List<Product>? result = products
                 .Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase))
                 .ToList();

            if (result is null)
            {
                return NotFound("No Results Found");
            }

            return Ok(result);
        }

        //delete a specific product

        [HttpDelete("{id}")]
        public ActionResult DeleteProductById(Guid id)
        {
            //Add the condition (if user_role is admin, otherwise it will not be allowed)
            Product? isFound = products.FirstOrDefault(x => x.ProductId == id);

            if (isFound == null)
            {
                return NotFound();
            }
            products.Remove(isFound);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProductInfo(
            string attributeName,
            string newValue,
            Product product,
            Guid id
        )
        {
            // Add the condition (if user_role is admin, otherwise it will not be allowed)
            Product? isFound = products.FirstOrDefault(x => x.ProductId == id);

            if (isFound == null)
            {
                return NotFound();
            }

            // is switch case a good choice here? like what info you want to update?
            switch (attributeName)
            {
                case "Name": // I want to add ignore case
                    isFound.ProductName = newValue;
                    product.ProductName = isFound.ProductName;
                    break;
                case "Color":
                    isFound.ProductColor = newValue;
                    product.ProductColor = isFound.ProductColor;
                    break;
                case "Price":
                    isFound.ProductPrice = Convert.ToDecimal(newValue);
                    product.ProductPrice = isFound.ProductPrice;
                    break;
                case "Weight":
                    isFound.Weight = Convert.ToDecimal(newValue);
                    product.Weight = isFound.Weight;
                    break;
            }
            return Ok(isFound);
        }

        //add product to the cart:
        /*
        public ActionResult AddProduct (Product product) {

        //if all the conditions are met, add the product to cart list:

        carts.Add(product);
        }

        */
    }
}
