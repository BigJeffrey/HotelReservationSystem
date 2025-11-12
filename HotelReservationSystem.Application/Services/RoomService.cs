using HotelReservationSystem.Application.DTOs.Rooms;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Application.DTOs.Common;

namespace HotelReservationSystem.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<PagedResponse<RoomResponse>> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _roomRepository.CountAsync();

            var rooms = await _roomRepository.GetAllAsync(page, pageSize);

            var items = rooms.Select(r => new RoomResponse(r)).ToList();

            return new PagedResponse<RoomResponse>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<RoomResponse?> GetByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                return null;

            return new RoomResponse(room);
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

        public async Task<RoomResponse?> UpdateAsync(int id, UpdateRoomRequest request)
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

            var r = await _roomRepository.UpdateAsync(room);
            await _roomRepository.SaveChangesAsync();

            return new RoomResponse(r);
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
