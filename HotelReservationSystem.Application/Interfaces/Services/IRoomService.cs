using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.DTOs.Rooms;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<PagedResponse<RoomResponse>> GetAllAsync(int page, int pageSize);
        Task<RoomResponse?> GetByIdAsync(int id);
        Task<Room> AddAsync(CreateRoomRequest request);
        Task<RoomResponse?> UpdateAsync(int id, UpdateRoomRequest request);
        Task DeleteAsync(int id);
    }
}
