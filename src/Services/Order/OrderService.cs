using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.DTO;
using src.Entity;
using src.Repository;
using static src.DTO.OrderDTO;

namespace src.Services
{
    public class OrderService : IOrderService
    {
        protected readonly OrderRepository _orderRepository;
        protected IMapper _mapper;
        public OrderService(OrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO.OrderReadDTO> CreateOneAsync(OrderDTO.OrderCreateDTO createDTO)
        {
            var order = _mapper.Map<OrderCreateDTO, Order>(createDTO);
            var orderCreated = await _orderRepository.CreateOneAsync(order);
            return _mapper.Map<Order, OrderReadDTO>(orderCreated);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {

            var foundOrder = await _orderRepository.GetByIdAsync(id);
            return await _orderRepository.DeleteOneAsync(foundOrder);
        }

        public async Task<List<OrderDTO.OrderReadDTO>> GetAllAsync()
        {
            var orderList = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<Order>, List<OrderDTO.OrderReadDTO>>(orderList);
        }

        public async Task<OrderDTO.OrderReadDTO> GetByIdAsync(Guid id)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<Order, OrderDTO.OrderReadDTO>(foundOrder);
        }

        public async Task<bool> UpdateOneAsync(Guid id, OrderDTO.OrderUpdateDTO updateDTO)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder != null)
            {
                return false;
            }
            _mapper.Map(updateDTO, foundOrder);
            return await _orderRepository.UpdateOneAsync(foundOrder);
        }
    }
}