using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class BookingRepository(HotelDbContext context) : IBookingRepository
    {
        private readonly HotelDbContext _context = context;

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.AsNoTracking().ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings.AsNoTracking()
                .FirstOrDefaultAsync(c => c.BookingId == id);
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            return booking;
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await Task.CompletedTask;
            return booking;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Bookings.FindAsync(id);
            if (existing != null)
            {
                _context.Bookings.Remove(existing);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
