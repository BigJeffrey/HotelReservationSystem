using HotelReservationSystem.Application.DTOs.Rooms;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task<Room> AddAsync(CreateRoomRequest request);
        Task<Room?> UpdateAsync(int id, UpdateRoomRequest request);
        Task DeleteAsync(int id);
    }
}
