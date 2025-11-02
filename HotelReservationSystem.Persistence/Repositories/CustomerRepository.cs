using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HotelDbContext _context;

        public CustomerRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing != null)
            {
                _context.Customers.Remove(existing);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
