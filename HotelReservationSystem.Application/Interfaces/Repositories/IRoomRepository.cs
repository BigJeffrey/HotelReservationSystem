using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task<Room?> GetByRoomNumberAsync(string roomNumber);
        Task<Room> AddAsync(Room payment);
        Task<Room> UpdateAsync(Room payment);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
