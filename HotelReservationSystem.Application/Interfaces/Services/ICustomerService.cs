using HotelReservationSystem.Application.DTOs.Customers;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer> AddAsync(CreateCustomerRequest customer);
        Task<Customer?> UpdateAsync(int id, UpdateCustomerRequest customer);
        Task DeleteAsync(int id);
    }
}
