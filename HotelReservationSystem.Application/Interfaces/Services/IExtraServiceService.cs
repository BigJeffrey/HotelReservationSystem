using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.DTOs.ExtraServices;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IExtraServiceService
    {
        Task<PagedResponse<ExtraServiceResponse>> GetAllAsync(int page, int pageSize);
        Task<ExtraServiceResponse?> GetByIdAsync(int id);
        Task<ExtraService> AddAsync(CreateExtraServiceRequest request);
        Task<ExtraService?> UpdateAsync(int id, UpdateExtraServiceRequest request);
        Task DeleteAsync(int id);
    }
}
