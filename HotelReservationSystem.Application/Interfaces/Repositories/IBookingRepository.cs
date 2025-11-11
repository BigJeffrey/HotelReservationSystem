using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<int> CountAsync();
        Task<IEnumerable<Booking>> GetAllAsync(int page, int pageSize);
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking> AddAsync(Booking booking);
        Task<Booking> UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
