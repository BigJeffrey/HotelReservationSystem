using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Repositories
{
    public interface IExtraServiceRepository
    {
        Task<int> CountAsync();
        Task<IEnumerable<ExtraService>> GetAllAsync(int page, int pageSize);
        Task<ExtraService?> GetByIdAsync(int id);
        Task<ExtraService?> GetByNameAsync(string name);

        Task<ExtraService> AddAsync(ExtraService extraService);
        Task<ExtraService> UpdateAsync(ExtraService extraService);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
