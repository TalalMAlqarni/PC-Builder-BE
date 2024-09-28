using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static src.DTO.CartDTO;

namespace src.Services.Cart
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
            var cart = _mapper.Map<CartCreateDto, Cart>(createDto);
            var cartCreated = await _cartRepo.CreateCartAsync(cart);
            return _mapper.Map<Cart, CartReadDto>(cartCreated);
        }

        public Task<bool> DeleteCartByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CartDTO.CartReadDto> GetCartByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartDTO.CartReadDto>> GetCartsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CartDTO.CartReadDto> UpdateCartAsync(Guid id, CartDTO.CartUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

    }
}