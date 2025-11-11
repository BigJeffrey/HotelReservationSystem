using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Repositories
{
    public interface IBookingDetailRepository
    {
        Task<int> CountAsync();
        Task<IEnumerable<BookingDetail>> GetAllAsync(int page, int pageSize);
        Task<BookingDetail?> GetByIdAsync(int id);
        Task<BookingDetail> AddAsync(BookingDetail bookingDetail);
        Task<BookingDetail> UpdateAsync(BookingDetail bookingDetail);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
