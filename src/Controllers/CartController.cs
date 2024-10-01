using Microsoft.AspNetCore.Mvc;

using src.Entity;
using src.Services.cart;
using src.Utils;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    //Todo: if we have time we must use CartUtils and make this controller as short and clean as possible
    {
        protected readonly ICartService _cartService;
        public CartController(ICartService service)
        {
            _cartService = service;
        }
        //get all carts: GET api/v1/cart
    //     [HttpGet]
    //     public async Task<ActionResult<List<CartReadDto>>> GetAllCarts()
    //     {
    //         var cartRead = await _cartService.GetCartsAsync();
    //         return Ok(cartRead);
    //     }

    //     //get cart by id: GET api/v1/cart/{id}
    //     [HttpGet("{id}")]
    //     public async Task<ActionResult<CartReadDto>> GetCartById(Guid id)
    //     {
    //         var cartRead = await _cartService.GetCartByIdAsync(id);
    //         return Ok(cartRead);
    //     }

    //     //create new cart: POST api/v1/cart
    //     [HttpPost]
    //     public async Task<ActionResult<CartReadDto>> CreateCart([FromBody] CartCreateDto createDto)
    //     {
    //         var cartRead = await _cartService.CreateCartAsync(createDto);

    //         return CreatedAtAction(nameof(GetCartById), new { id = cartRead.Id }, cartRead);
    //     }

    //     //update cart: PUT api/v1/cart/{id}
    //     [HttpPut("{id}")]
    //     public async Task<ActionResult<CartReadDto>> UpdateCart(Guid id, CartUpdateDto updateDto)
    //     {
    //         var cartRead = await _cartService.UpdateCartAsync(id, updateDto);
    //         return Ok(cartRead);
    //     }

    //     //delete cart: DELETE api/v1/cart/{id}
    //     [HttpDelete("{id}")]
    //     public async Task<ActionResult<bool>> DeleteCartById(Guid id)
    //     {
    //         var isDeleted = await _cartService.DeleteCartByIdAsync(id);
    //         return Ok(isDeleted);
    //     }

    // }}
        public static List<Cart> Carts = new List<Cart>(){
    new Cart
    {
        Id = Guid.NewGuid(),
        UserId = Guid.NewGuid(),
        CartDetails = new List<CartDetails>
        {
            new CartDetails
            {
                CartDetailsId = Guid.NewGuid(),
                Product = new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Laptop",
                    ProductColor = "Silver",
                    SKU = 10,
                    ProductPrice = 1000.00m,
                    Weight = 1.5m
                },
                Quantity = 1,
            },
            new CartDetails
            {
                CartDetailsId = Guid.NewGuid(),
                Product = new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Mouse",
                    ProductColor = "Black",
                    SKU = 50,
                    ProductPrice = 25.00m,
                    Weight = 0.2m
                },
                Quantity = 5,
            }
        },
        CartQuantity = 6,
        TotalPrice = 1125.00m
    }
};

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
            string result = CartUtils.ThereIsLowStockProduct(newCart);
            if (result != "")
            {
                return BadRequest($"Not enough stock for {result}");
            }
            // check if user already has an active cart
            var existingCart = Carts.FirstOrDefault(c => c.UserId == newCart.UserId);
            if (existingCart != null)
                return BadRequest("User already has an active cart.");//TODO: as improvement we can add the list of products to the existing cart

            newCart.Id = Guid.NewGuid();
            newCart.CartQuantity = newCart.CartDetails.Sum(p => p.Quantity);
            newCart.TotalPrice = newCart.CartDetails.Sum(p => p.Subtotal);
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
        public ActionResult AddListProductsToCart(Guid cartId, List<CartDetails> newProducts)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == cartId);//find cart
            if (cart == null)
                return NotFound("Cart not found.");

            // check if product is already in cart update quantity and subtotal
            List<CartDetails> productsInCart = cart.CartDetails;

            // check for SKU
            foreach (var addedProduct in newProducts)
            {
                var existingProduct = productsInCart.FirstOrDefault(p => p.Product.ProductId == addedProduct.Product.ProductId);// maybe chang from ID to name
                if (existingProduct != null)
                {
                    if (existingProduct.Product.SKU < existingProduct.Quantity + addedProduct.Quantity)
                    {
                        return BadRequest($"Not enough stock for {existingProduct.Product.ProductName}");
                    }
                }
                else
                {
                    if (addedProduct.Product.SKU < addedProduct.Quantity)
                    {
                        return BadRequest($"Not enough stock for {addedProduct.Product.ProductName}");
                    }
                }
            }
            // add product to cart
            foreach (var addedProduct in newProducts)
            {
                var existingProduct = productsInCart.FirstOrDefault(p => p.Product.ProductId == addedProduct.Product.ProductId);
                if (existingProduct != null)// update quantity and subtotal if product is already in cart
                {
                    existingProduct.Quantity += addedProduct.Quantity;
                    // existingProduct.Subtotal += addedProduct.Subtotal;// no longer needed auto calculated
                }
                else // add product to cart if it dose not exist
                {
                    cart.CartDetails.Add(addedProduct);
                }
                cart.CartQuantity += addedProduct.Quantity;
                cart.TotalPrice += addedProduct.Subtotal;
            }
            return Ok(cart);
        }
        //add single product to cart: PUT api/v1/cart/{id}/addSingleProduct
        [HttpPut("{cartId:guid}/addSingleProduct")]
        public ActionResult AddSingleProductToCart(Guid cartId, CartDetails newProduct)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == cartId);//find cart
            if (cart == null)
                return NotFound("Cart not found.");
            // check if product is already in cart update quantity and subtotal
            var existingProduct = cart.CartDetails.FirstOrDefault(p => p.Product.ProductId == newProduct.Product.ProductId);// maybe chang from ID to name
            if (existingProduct != null)
            {
                if (existingProduct.Product.SKU < existingProduct.Quantity + newProduct.Quantity)
                {
                    return BadRequest($"Not enough stock for {existingProduct.Product.ProductName}");
                }
                existingProduct.Quantity += newProduct.Quantity;
                //existingProduct.Subtotal += newProduct.Subtotal;// no longer needed auto calculated
            }
            else
            {
                if (newProduct.Product.SKU < newProduct.Quantity)
                {
                    return BadRequest($"Not enough stock for {newProduct.Product.ProductName}");
                }
                cart.CartDetails.Add(newProduct);
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
            var removedProduct = cart.CartDetails.FirstOrDefault(p => p.Product.ProductId == productId);
            if (removedProduct == null)
                return NotFound("Product not found.");
            cart.CartQuantity -= removedProduct.Quantity;
            //cart.TotalPrice -= product.Subtotal;// no longer needed auto calculated
            cart.CartDetails.Remove(removedProduct);
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

            var matchedProduct = cart.CartDetails.FirstOrDefault(p => p.Product.ProductId == productId);
            if (matchedProduct == null)//check if product exists
                return NotFound("Product not found.");

            if (matchedProduct.Product.SKU < quantity)//check if there is enough stock
                return BadRequest($"Not enough stock for {matchedProduct.Product.ProductName}");

            matchedProduct.Quantity = quantity;
            //product.Subtotal = quantity * product.ProductPrice;// no longer needed auto calculated
            cart.CartQuantity = cart.CartDetails.Sum(p => p.Quantity);
            cart.TotalPrice = cart.CartDetails.Sum(p => p.Subtotal);
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

            var matchedProduct = cart.CartDetails.FirstOrDefault(p => p.Product.ProductId == productId);
            if (matchedProduct == null)//check if product exists
                return NotFound("Product not found.");

            if (matchedProduct.Product.SKU < matchedProduct.Quantity + amount)//check if there is enough stock
                return BadRequest($"Not enough stock for {matchedProduct.Product.ProductName}");

            if (matchedProduct.Quantity + amount <= 0)
                return BadRequest("Quantity must be greater than zero.");//TODO: if quantity is 0 we should delete the product

            matchedProduct.Quantity += amount;
            //product.Subtotal = product.Quantity * product.ProductPrice;// no longer needed auto calculated
            cart.CartQuantity = cart.CartDetails.Sum(p => p.Quantity);
            cart.TotalPrice = cart.CartDetails.Sum(p => p.Subtotal);
            return Ok(cart);
        }
    }
}

