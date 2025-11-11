using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Repositories
{
    public interface IBookingServiceRepository
    {
        Task<int> CountAsync();
        Task<IEnumerable<BookingServiceEntity>> GetAllAsync(int page, int pageSize);
        Task<BookingServiceEntity?> GetByIdAsync(int id);
        Task<BookingServiceEntity> AddAsync(BookingServiceEntity bookingService);
        Task<BookingServiceEntity> UpdateAsync(BookingServiceEntity bookingService);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
