using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class BookingDetailRepository : IBookingDetailsRepository
    {
        private readonly HotelDbContext _context;

        public BookingDetailRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingDetail>> GetAllAsync()
        {
            return await _context.BookingDetails.AsNoTracking().ToListAsync();
        }

        public async Task<BookingDetail?> GetByIdAsync(int id)
        {
            return await _context.BookingDetails.AsNoTracking()
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
