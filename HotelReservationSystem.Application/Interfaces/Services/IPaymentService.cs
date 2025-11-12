using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.DTOs.Payments;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<PagedResponse<PaymentResponse>> GetAllAsync(int page, int pageSize);
        Task<PaymentResponse?> GetByIdAsync(int id);
        Task<Payment> AddAsync(CreatePaymentRequest request);
        Task<PaymentResponse?> UpdateAsync(int id, UpdatePaymentRequest request);
        Task DeleteAsync(int id);
    }
}
