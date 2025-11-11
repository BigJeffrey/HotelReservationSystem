using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class BookingDetailRepository(HotelDbContext context) : IBookingDetailRepository
    {
        private readonly HotelDbContext _context = context;

        public async Task<int> CountAsync()
        {
            return await _context.BookingDetails.CountAsync();
        }

        public async Task<IEnumerable<BookingDetail>> GetAllAsync(int page, int pageSize)
        {
            return await _context.BookingDetails.AsNoTracking()
                .Include(b => b.Booking)
                    .ThenInclude(book => book.Customer)
                .Include(b => b.Room)
                .OrderBy(b => b.BookingDetailId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<BookingDetail?> GetByIdAsync(int id)
        {
            return await _context.BookingDetails.AsNoTracking()
                .Include(b => b.Booking)
                    .ThenInclude(book => book.Customer)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(c => c.BookingDetailId == id);
        }

        public async Task<BookingDetail> AddAsync(BookingDetail bookingDetails)
        {
            await _context.BookingDetails.AddAsync(bookingDetails);
            return bookingDetails;
        }

        public async Task<BookingDetail> UpdateAsync(BookingDetail bookingDetails)
        {
            _context.BookingDetails.Update(bookingDetails);
            await Task.CompletedTask;
            return bookingDetails;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.BookingDetails.FindAsync(id);
            if (existing != null)
            {
                _context.BookingDetails.Remove(existing);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
