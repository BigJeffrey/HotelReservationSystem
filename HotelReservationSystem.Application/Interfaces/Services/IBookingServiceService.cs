using HotelReservationSystem.Application.DTOs.BookingServices;
using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IBookingServiceService
    {
        Task<PagedResponse<BookingServiceResponse>> GetAllAsync(int page, int pageSize);
        Task<BookingServiceResponse?> GetByIdAsync(int id);
        Task<BookingServiceEntity> AddAsync(CreateBookingServiceRequest request);
        Task<BookingServiceEntity?> UpdateAsync(int id, UpdateBookingServiceRequest request);
        Task DeleteAsync(int id);
    }
}
