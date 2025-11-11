using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> CountAsync();
        Task<IEnumerable<Customer>> GetAllAsync(int page, int pageSize);
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
