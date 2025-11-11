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
    }
}
