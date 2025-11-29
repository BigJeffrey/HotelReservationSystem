using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class RoomRepository(HotelDbContext context) : IRoomRepository
    {
        private readonly HotelDbContext _context = context;

        public async Task<int> CountAsync()
        {
            return await _context.Rooms.CountAsync();
        }

        public async Task<IEnumerable<Room>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Rooms.AsNoTracking().AsNoTracking()
                .Include(r => r.BookingDetails)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAvailableRoomsAsync(DateTime checkInDate, DateTime checkOutDate)
        {
            var bookedRoomIdsQuery = GetBookedRoomIdsQuery(checkInDate, checkOutDate);

            return await _context.Rooms
                .AsNoTracking()
                .Where(r => !bookedRoomIdsQuery.Contains(r.RoomId))
                .CountAsync();
        }

        // Retrieves rooms that are available between the specified check-in and check-out dates
        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkInDate, DateTime checkOutDate, int page, int pageSize)
        {
            var bookedRoomIdsQuery = GetBookedRoomIdsQuery(checkInDate, checkOutDate);

            // Select available rooms, apply pagination, and execute the query
            var availableRooms = await _context.Rooms
                .AsNoTracking()
                .Where(r => !bookedRoomIdsQuery.Contains(r.RoomId))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return availableRooms;
        }

        public async Task<Boolean> IsRoomAvailable(DateTime checkInDate, DateTime checkOutDate, int roomId)
        {
            var bookedRoomIdsQuery = GetBookedRoomIdsQuery(checkInDate, checkOutDate);
            return !await _context.Rooms
                .AsNoTracking()
                .AnyAsync(r => bookedRoomIdsQuery.Contains(roomId));
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Rooms.AsNoTracking()
                .FirstOrDefaultAsync(c => c.RoomId == id);
        }

        public async Task<Room?> GetByRoomNumberAsync(string roomNumber)
        {
            return await _context.Rooms.AsNoTracking()
                .FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
        }

        public async Task<Room> AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            return room;
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await Task.CompletedTask;
            return room;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Rooms.FindAsync(id);
            if (existing != null)
            {
                _context.Rooms.Remove(existing);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<int> GetBookedRoomIdsQuery(DateTime checkInDate, DateTime checkOutDate)
        {
            return _context.BookingDetails
                .Where(bd => bd.Booking != null &&
                             bd.Booking.EndDate > checkInDate &&
                             bd.Booking.StartDate < checkOutDate)
                .Select(bd => bd.RoomId)
                .Distinct();
        }
    }
}
