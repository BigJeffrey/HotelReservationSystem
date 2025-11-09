using HotelReservationSystem.Application.DTOs.BookingServices;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IBookingServiceService
    {
        Task<IEnumerable<BookingServiceEntity>> GetAllAsync();
        Task<BookingServiceEntity?> GetByIdAsync(int id);
        Task<BookingServiceEntity> AddAsync(CreateBookingServiceRequest request);
        Task<BookingServiceEntity?> UpdateAsync(int id, UpdateBookingServiceRequest request);
        Task DeleteAsync(int id);
    }
}
