using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.DTO;
using static src.DTO.OrderDTO;

namespace src.Services
{
    public interface IOrderService
    {
        Task<OrderReadDTO> CreateOneAsync(OrderCreateDTO createDTO);
        Task<List<OrderReadDTO>> GetAllAsync();
        Task<OrderReadDTO> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOneAsync(Guid id, OrderUpdateDTO updateDTO);


    }
}