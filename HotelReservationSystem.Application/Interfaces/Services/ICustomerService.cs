using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.DTOs.Customers;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<PagedResponse<CustomerResponse>> GetAllAsync(int page, int pageSize);
        Task<CustomerResponse?> GetByIdAsync(int id);
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer> AddAsync(CreateCustomerRequest customer);
        Task<Customer?> UpdateAsync(int id, UpdateCustomerRequest customer);
        Task DeleteAsync(int id);
    }
}
