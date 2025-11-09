using HotelReservationSystem.Application.DTOs.Bookings;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking> AddAsync(CreateBookingRequest request);
        Task<Booking?> UpdateAsync(int id, UpdateBookingRequest request);
        Task DeleteAsync(int id);
    }
}
