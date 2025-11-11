using HotelReservationSystem.Application.DTOs.BookingDetails;
using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IBookingDetailService
    {
        Task<PagedResponse<BookingDetailResponse>> GetAllAsync(int page, int pageSize);
        Task<BookingDetailResponse?> GetByIdAsync(int id);
        Task<BookingDetail> AddAsync(CreateBookingDetailRequest request);
        Task<BookingDetail?> UpdateAsync(int id, UpdateBookingDetailRequest request);
        Task DeleteAsync(int id);
    }
}
