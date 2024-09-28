using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Database;
using src.Entity;
using Microsoft.EntityFrameworkCore;

namespace src.Repository
{
    public class OrderRepository
    {
        protected DbSet<Order> _order;
        protected DatabaseContext _databaseContext;

        public OrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _order = databaseContext.Set<Order>();
        }

        public async Task<Order> CreateOneAsync(Order newOrder)
        {
            await _order.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            return newOrder;
        }
        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _order.FindAsync(id);
        }
        public async Task<List<Order>> GetAllAsync()
        {
            return await _order.ToListAsync();
        }
        public async Task<bool> UpdateOneAsync(Order updateOrder)
        {
            _order.Update(updateOrder);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteOneAsync(Order order)
        {
            _order.Remove(order);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}