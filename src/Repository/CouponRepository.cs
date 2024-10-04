using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

using src.Utils;
namespace src.Repository
{
    public class CouponRepository
    {
        protected DbSet<Coupon> _coupons;
        protected DatabaseContext _databaseContext;

        public CouponRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _coupons = databaseContext.Set<Coupon>();
        }
        public async Task<Coupon> CreateOneAsync(Coupon newCoupon)
        {
            await _coupons.AddAsync(newCoupon);
            await _databaseContext.SaveChangesAsync();
            return newCoupon;
        }
        
        public async Task<List<Coupon>> GetAllAsync()
        {
            return await _coupons.ToListAsync();
        }

        public async Task<Coupon> GetByIdAsync(Guid id)
        {
            return await _coupons.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteOneAsync(Coupon coupon)
        {
            _coupons.Remove(coupon);
            await _databaseContext.SaveChangesAsync();
            return true;
        }  
        
        public async Task<bool> UpdateOneAsync(Coupon coupon)
        {
            _coupons.Update(coupon);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}   