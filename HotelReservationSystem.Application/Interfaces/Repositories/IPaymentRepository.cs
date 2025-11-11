using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<int> CountAsync();
        Task<IEnumerable<Payment>> GetAllAsync(int page, int pageSize);
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> AddAsync(Payment payment);
        Task<Payment> UpdateAsync(Payment payment);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
