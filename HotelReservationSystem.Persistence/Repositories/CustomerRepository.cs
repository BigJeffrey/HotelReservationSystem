using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class CustomerRepository(HotelDbContext context) : ICustomerRepository
    {
        private readonly HotelDbContext _context = context;

        public async Task<int> CountAsync()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Customers.AsNoTracking().AsNoTracking()
                .Include(c => c.Bookings)
                .OrderBy(c => c.CustomerId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.AsNoTracking()
                .Include(c => c.Bookings)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email.ToLower());
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            customer.Email = customer.Email.ToLowerInvariant();
            await _context.Customers.AddAsync(customer);
            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await Task.CompletedTask;
            return customer;
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
