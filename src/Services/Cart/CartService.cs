using AutoMapper;
using src.Repository;
using src.Entity;
using static src.DTO.CartDTO;


namespace src.Services.cart
{
    public class CartService : ICartService
    {
        protected readonly CartRepository _cartRepo;
        protected readonly IMapper _mapper;
        public CartService(CartRepository cartRepo, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _mapper = mapper;
        }

        public async Task<CartReadDto> CreateCartAsync(CartCreateDto createDto)
        {

            var cart = new Cart
            {
                UserId = createDto.UserId,
                CartDetails = new List<CartDetails>(),
                CartQuantity = 0,
                TotalPrice = 0
            };
            foreach (var detailsDto in createDto.CartDetails)
            {
                //check if product exists
                var product = await _cartRepo.GetProductByIdForCartAsync(detailsDto.ProductId);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                // Create new CartDetails but reference the existing Product
                var cartDetails = new CartDetails
                {
                    Product = product,
                    Quantity = detailsDto.Quantity,
                    CartId = cart.Id
                };
                cart.CartDetails.Add(cartDetails);
            }
            var cartCreated = await _cartRepo.CreateCartAsync(cart);
            return _mapper.Map<Cart, CartReadDto>(cartCreated);
        }

        public async Task<bool> DeleteCartByIdAsync(Guid id)
        {
            var foundCart = await _cartRepo.GetCartByIdAsync(id);
            bool isDeleted = await _cartRepo.DeleteCartAsync(foundCart);
            return isDeleted;
        }

        public async Task<CartReadDto> GetCartByIdAsync(Guid id)
        {
            var foundCart = await _cartRepo.GetCartByIdAsync(id);
            //handle not found
            return _mapper.Map<Cart, CartReadDto>(foundCart);
        }

        public async Task<List<CartReadDto>> GetCartsAsync()
        {
            var carts = await _cartRepo.GetAllCartsAsync();
            return _mapper.Map<List<Cart>, List<CartReadDto>>(carts);
        }

        public async Task<CartReadDto> UpdateCartAsync(Guid id, CartUpdateDto updateDto)
        {
            var foundCart = await _cartRepo.GetCartByIdAsync(id);

            if (foundCart == null)
                return null;

            _mapper.Map(updateDto, foundCart);
            var updatedCart = await _cartRepo.UpdateCartAsync(foundCart);
            return _mapper.Map<Cart, CartReadDto>(updatedCart);
        }

    }
}