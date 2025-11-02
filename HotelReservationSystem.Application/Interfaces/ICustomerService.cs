using HotelReservationSystem.Application.DTOs.Customers;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> AddCustomerAsync(CreateCustomerRequest customer);
        Task<Customer?> UpdateCustomerAsync(int id, UpdateCustomerRequest customer);
        Task DeleteCustomerAsync(int id);
    }
}
