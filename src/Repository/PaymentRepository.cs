using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public class PaymentRepository
    {
        protected DbSet<Payment> _payments;
        protected DatabaseContext _databaseContext;

        public PaymentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _payments = databaseContext.Set<Payment>();
        }

        public async Task<Payment> CreateOneAsync(Payment newPayment)
        {
            await _payments.AddAsync(newPayment);
            await _databaseContext.SaveChangesAsync();
            return newPayment;
        }

        // Retrieve all payment records
        public async Task<List<Payment>> GetAllAsync()
        {
            return await _payments.ToListAsync();
        }

        // Retrieve a payment by its ID
        public async Task<Payment?> GetByIdAsync(Guid paymentId)
        {
            return await _payments.FindAsync(paymentId);
        }

        // Update an existing payment record
        public async Task<bool> UpdateOneAsync(Payment updatedPayment)
        {
            _payments.Update(updatedPayment);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // Delete a payment record
        public async Task<bool> DeleteOneAsync(Payment payment)
        {
            _payments.Remove(payment);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(Payment payment)
        {
            _payments.Update(payment);
            await _databaseContext.SaveChangesAsync();
        }

    }
}
