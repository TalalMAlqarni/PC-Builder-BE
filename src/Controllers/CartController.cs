using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    //Todo: every product must be replaced with cartDetails and chang the logic to use sub totaland quantity of cart details instead of product
    {
        public static List<Cart> Carts = new List<Cart>(){
    new Cart
    {
        Id = Guid.NewGuid(),  // Use a new GUID for the cart
        UserId = Guid.NewGuid(),  // Use a new GUID for the user (or assign a specific user ID)
        Products = new List<Product>
        {
            new Product
            {
                ProductId = Guid.NewGuid(),  // Use a new GUID for the product
                ProductName = "Chair",
                ProductColor = "Red",
                SKU = 15,
                Quantity = 1,
                ProductPrice = 75.00m,
                Subtotal = 75.00m,
                Weight = 2
            },
            new Product
            {
                ProductId = Guid.NewGuid(),  // Use a new GUID for the second product
                ProductName = "Table",
                ProductColor = "Brown",
                SKU = 10,
                Quantity = 1,
                ProductPrice = 150.00m,
                Subtotal = 150.00m,
                Weight = 10
            }
        },
        CartQuantity = 2,  // Total quantity of products in the cart
        TotalPrice = 225.00m  // Total price of products in the cart
    }
};

        /*
        testing
        {
            "userId": "d7b5d5c8-7f75-4e8f-bfc4-90c3d50d2a11",
            "products": [
                {
                    "ProductId": "e8a13c1b-8431-47e0-8a5f-e75df2a01e3a",  
                    "ProductName": "Chair",
                    "ProductColor": "Red",
                    "SKU": 15,
                    "Quantity": 1,
                    "ProductPrice": 75.00,
                    "Subtotal": 75.00,
                    "Weight": 2
                },
                {
                    "ProductId": "a5f3b6c4-12a3-4d67-8b5f-e89df7a34f92",  
                    "ProductName": "Table",
                    "ProductColor": "Brown",
                    "SKU": 10,
                    "Quantity": 1,
                    "ProductPrice": 150.00,
                    "Subtotal": 150.00,
                    "Weight": 5
                }
            ]
        }

        */



        //get all carts: GET api/v1/cart
        [HttpGet]
        public ActionResult GetCarts()
        {
            return Ok(Carts);
        }
        //find cart by id: GET api/v1/cart/{id}
        [HttpGet("{id:guid}")]
        public ActionResult GetCartById(Guid id)
        {
            Cart? cartById = Carts.FirstOrDefault(c => c.Id == id);

            if (cartById == null)
            {
                return NotFound();
            }

            return Ok(cartById);
        }

        //create cart: POST api/v1/cart          
        [HttpPost]
        public ActionResult CreateCart(Cart newCart)
        {
            // check if for SKU
            var lowStockProduct = newCart.Products.FirstOrDefault(p => p.SKU < p.Quantity);
            if (lowStockProduct != null)
            {
                return BadRequest($"Not enough stock for {lowStockProduct.ProductName}");
            }
            // check if user already has an active cart
            var existingCart = Carts.FirstOrDefault(c => c.UserId == newCart.UserId);
            if (existingCart != null)
                return BadRequest("User already has an active cart.");//TODO: as improvement we can add the list of products to the existing cart

            newCart.Id = Guid.NewGuid();
            newCart.CartQuantity = newCart.Products.Sum(p => p.Quantity);
            newCart.TotalPrice = newCart.Products.Sum(p => p.Subtotal);//Sum(p => p.ProductPrice * p.Quantity);
            Carts.Add(newCart);
            return CreatedAtAction(nameof(GetCarts), new { id = newCart.Id }, newCart);
        }

        //delete cart: DELETE api/v1/cart/{id}
        [HttpDelete("{cartId:guid}")]
        public ActionResult DeleteCartById(Guid cartId)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            Carts.Remove(cart);
            return NoContent();
        }

        //add list of product to cart: PUT api/v1/cart/{id}/addListProducts
        [HttpPut("{cartId:guid}/addListProducts")]
        public ActionResult AddListProductsToCart(Guid cartId, List<Product> newProducts)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == cartId);//find cart
            if (cart == null)
                return NotFound("Cart not found.");

            // check if product is already in cart update quantity and subtotal
            List<Product> productsInCart = cart.Products;

            // check for SKU
            foreach (var product in newProducts)
            {
                var existingProduct = productsInCart.FirstOrDefault(p => p.ProductId == product.ProductId);// maybe chang from ID to name
                if (existingProduct != null)
                {
                    if (existingProduct.SKU < existingProduct.Quantity + product.Quantity)
                    {
                        return BadRequest($"Not enough stock for {existingProduct.ProductName}");
                    }
                }
                else
                {
                    if (product.SKU < product.Quantity)
                    {
                        return BadRequest($"Not enough stock for {product.ProductName}");
                    }
                }
            }
            // add product to cart
            foreach (var product in newProducts)
            {
                var existingProduct = productsInCart.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existingProduct != null)// update quantity and subtotal if product is already in cart
                {
                    existingProduct.Quantity += product.Quantity;
                    existingProduct.Subtotal += product.Subtotal;
                }
                else // add product to cart if it dose not exist
                {
                    cart.Products.Add(product);
                }
                cart.CartQuantity += product.Quantity;
                cart.TotalPrice += product.Subtotal;
            }
            return Ok(cart);
        }
        //add single product to cart: PUT api/v1/cart/{id}/addSingleProduct
        [HttpPut("{cartId:guid}/addSingleProduct")]
        public ActionResult AddSingleProductToCart(Guid cartId, Product newProduct)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == cartId);//find cart
            if (cart == null)
                return NotFound("Cart not found.");
            // check if product is already in cart update quantity and subtotal
            var existingProduct = cart.Products.FirstOrDefault(p => p.ProductId == newProduct.ProductId);// maybe chang from ID to name
            if (existingProduct != null)
            {
                if (existingProduct.SKU < existingProduct.Quantity + newProduct.Quantity)
                {
                    return BadRequest($"Not enough stock for {existingProduct.ProductName}");
                }
                existingProduct.Quantity += newProduct.Quantity;
                existingProduct.Subtotal += newProduct.Subtotal;
            }
            else
            {
                if (newProduct.SKU < newProduct.Quantity)
                {
                    return BadRequest($"Not enough stock for {newProduct.ProductName}");
                }
                cart.Products.Add(newProduct);
            }
            // add product to cart
            cart.CartQuantity += newProduct.Quantity;
            cart.TotalPrice += newProduct.Subtotal;
            return Ok(cart);
        }
        //remove product from cart: DELETE api/v1/cart/{id}/product/{productId}
        [HttpDelete("{cartId:guid}/product/{productId:guid}")]
        public ActionResult RemoveProductFromCart(Guid cartId, Guid productId)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == cartId);//find cart
            if (cart == null)
                return NotFound("Cart not found.");
            var product = cart.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
                return NotFound("Product not found.");
            cart.CartQuantity -= product.Quantity;
            cart.TotalPrice -= product.Subtotal;
            cart.Products.Remove(product);
            return NoContent();
        }
        // in this method we insert the value of quantity in the product in cart (SET)
        //update quantity: PUT api/v1/cart/{id}/product/{productId}/setQuantity/{quantity}
        [HttpPut("{cartId:guid}/product/{productId:guid}/setQuantity/{quantity:int}")]
        public ActionResult UpdateQuantity(Guid cartId, Guid productId, int quantity)
        {
            if (quantity <= 0)//check if quantity is greater than zero
                return BadRequest("Quantity must be greater than zero.");//TODO: if quantity is 0 we should delete the product

            var cart = Carts.FirstOrDefault(c => c.Id == cartId);//find cart
            if (cart == null)//check if cart exists
                return NotFound("Cart not found.");

            var product = cart.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)//check if product exists
                return NotFound("Product not found.");

            if (product.SKU < quantity)//check if there is enough stock
                return BadRequest($"Not enough stock for {product.ProductName}");

            product.Quantity = quantity;
            product.Subtotal = quantity * product.ProductPrice;
            cart.CartQuantity = cart.Products.Sum(p => p.Quantity);
            cart.TotalPrice = cart.Products.Sum(p => p.Subtotal);
            return Ok(cart);
        }
        // in this method we increase or decrease the value of quantity in the product in cart
        //update quantity: PUT api/v1/cart/{id}/product/{productId}/modifyQuantity/{amount}
        [HttpPut("{cartId:guid}/product/{productId:guid}/modifyQuantity/{amount:int}")]
        public ActionResult ModifyQuantity(Guid cartId, Guid productId, int amount)
        {

            var cart = Carts.FirstOrDefault(c => c.Id == cartId);//find cart
            if (cart == null)//check if cart exists
                return NotFound("Cart not found.");

            var product = cart.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)//check if product exists
                return NotFound("Product not found.");

            if (product.SKU < product.Quantity + amount)//check if there is enough stock
                return BadRequest($"Not enough stock for {product.ProductName}");

            if (product.Quantity + amount <= 0)
                return BadRequest("Quantity must be greater than zero.");//TODO: if quantity is 0 we should delete the product

            product.Quantity += amount;
            product.Subtotal = product.Quantity * product.ProductPrice;
            cart.CartQuantity = cart.Products.Sum(p => p.Quantity);
            cart.TotalPrice = cart.Products.Sum(p => p.Subtotal);
            return Ok(cart);
        }
    }
}

