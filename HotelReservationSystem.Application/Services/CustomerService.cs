using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            // ✅ Example of business rule: check for duplicate email
            var existingCustomers = await _customerRepository.GetAllAsync();
            if (existingCustomers.Any(c => c.Email == customer.Email))
                throw new InvalidOperationException("Customer with this email already exists.");

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
            await _customerRepository.SaveChangesAsync();
        }
    }
}
