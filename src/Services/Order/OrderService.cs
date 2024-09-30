using AutoMapper;
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

        public async Task<OrderReadDTO> CreateOneAsync(OrderCreateDTO createDTO)
        {
            var order = _mapper.Map<OrderCreateDTO, Order>(createDTO);
            var orderCreated = await _orderRepository.CreateOneAsync(order);
            return _mapper.Map<Order, OrderReadDTO>(orderCreated);
        }

        public async Task<List<OrderReadDTO>> GetAllAsync()
        {
            var orderList = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<Order>, List<OrderReadDTO>>(orderList);
        }

        public async Task<OrderReadDTO> GetByIdAsync(Guid id)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<Order, OrderReadDTO>(foundOrder);
        }

        public async Task<List<OrderReadDTO>> GetByUserIdAsync(Guid userId)
        {
            var ordersList = await _orderRepository.GetByUserIdAsync(userId);
            return _mapper.Map<List<Order>, List<OrderReadDTO>>(ordersList);
        }

        public async Task<List<OrderReadDTO>> GetHistoryByUserIdAsync(Guid userId)
        {
            var ordersList = await _orderRepository.GetByHistoryUserIdAsync(userId); ;
            return _mapper.Map<List<Order>, List<OrderReadDTO>>(ordersList);
        }
        public async Task<bool> UpdateOneAsync(Guid id, OrderUpdateDTO updateDTO)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
            {
                return false;
            }
            _mapper.Map(updateDTO, foundOrder);
            return await _orderRepository.UpdateOneAsync(foundOrder);
        }
        public async Task<bool> DeleteOneAsync(Guid id)
        {

            var foundOrder = await _orderRepository.GetByIdAsync(id);
            return await _orderRepository.DeleteOneAsync(foundOrder);
        }
    }
}