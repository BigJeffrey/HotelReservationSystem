using HotelReservationSystem.Application.DTOs.Payments;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> AddAsync(CreatePaymentRequest request);
        Task<Payment?> UpdateAsync(int id, UpdatePaymentRequest request);
        Task DeleteAsync(int id);
    }
}
