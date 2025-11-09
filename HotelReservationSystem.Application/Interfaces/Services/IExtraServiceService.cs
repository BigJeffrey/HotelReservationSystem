using HotelReservationSystem.Application.DTOs.ExtraServices;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IExtraServiceService
    {
        Task<IEnumerable<ExtraService>> GetAllAsync();
        Task<ExtraService?> GetByIdAsync(int id);
        Task<ExtraService> AddAsync(CreateExtraServiceRequest request);
        Task<ExtraService?> UpdateAsync(int id, UpdateExtraServiceRequest request);
        Task DeleteAsync(int id);
    }
}
