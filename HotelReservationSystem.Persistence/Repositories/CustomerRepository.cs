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
