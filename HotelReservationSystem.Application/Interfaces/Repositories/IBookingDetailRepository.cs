using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Repositories
{
    public interface IBookingDetailRepository
    {
        Task<IEnumerable<BookingDetail>> GetAllAsync();
        Task<BookingDetail?> GetByIdAsync(int id);
        Task<BookingDetail> AddAsync(BookingDetail bookingDetail);
        Task<BookingDetail> UpdateAsync(BookingDetail bookingDetail);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
