using HotelReservationSystem.Application.DTOs.Rooms;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;

namespace HotelReservationSystem.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<Room> AddAsync(CreateRoomRequest request)
        {
            var existingRoom = await _roomRepository.GetByRoomNumberAsync(request.RoomNumber);
            if (existingRoom != null)
                throw new InvalidOperationException("Room with this number already exists.");

            var room = new Room
            {
                RoomNumber = request.RoomNumber,
                RoomType = request.Type,
                PricePerNight = request.PricePerNight,
                Capacity = request.Capacity,
                IsAvailable = request.IsAvailable
            };

            var createdRoom = await _roomRepository.AddAsync(room);
            await _roomRepository.SaveChangesAsync();

            return createdRoom;
        }

        public async Task<Room?> UpdateAsync(int id, UpdateRoomRequest request)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                return null;

            if (!string.IsNullOrEmpty(request.RoomNumber))
            {
                var existing = await _roomRepository.GetByRoomNumberAsync(request.RoomNumber);
                if (existing is not null && existing.RoomId != id)
                    throw new InvalidOperationException("Another room with this number already exists.");

                room.RoomNumber = request.RoomNumber;
            }

            if (request.Type.HasValue)
                room.RoomType = request.Type.Value;

            if (request.PricePerNight.HasValue)
                room.PricePerNight = request.PricePerNight.Value;

            if (request.Capacity.HasValue)
                room.Capacity = request.Capacity.Value;

            if (request.IsAvailable.HasValue)
                room.IsAvailable = request.IsAvailable.Value;

            var updated = await _roomRepository.UpdateAsync(room);
            await _roomRepository.SaveChangesAsync();

            return updated;
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new InvalidOperationException("Room not found.");

            await _roomRepository.DeleteAsync(id);
            await _roomRepository.SaveChangesAsync();
        }
    }
}
