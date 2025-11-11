using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class PaymentRepository(HotelDbContext context) : IPaymentRepository
    {
        private readonly HotelDbContext _context = context;

        public async Task<int> CountAsync()
        {
            return await _context.Payments.CountAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Payments.AsNoTracking().AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments.AsNoTracking()
                .FirstOrDefaultAsync(c => c.PaymentId == id);
        }

        public async Task<Payment> AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await Task.CompletedTask;
            return payment;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Payments.FindAsync(id);
            if (existing != null)
            {
                _context.Payments.Remove(existing);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
