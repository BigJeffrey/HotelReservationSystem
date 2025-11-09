using HotelReservationSystem.Application.DTOs.BookingDetails;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IBookingDetailService
    {
        Task<IEnumerable<BookingDetail>> GetAllAsync();
        Task<BookingDetail?> GetByIdAsync(int id);
        Task<BookingDetail> AddAsync(CreateBookingDetailRequest request);
        Task<BookingDetail?> UpdateAsync(int id, UpdateBookingDetailRequest request);
        Task DeleteAsync(int id);
    }
}
