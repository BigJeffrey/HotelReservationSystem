using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class BookingServiceRepository : IBookingServicesRepository
    {
        private readonly HotelDbContext _context;

        public BookingServiceRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingService>> GetAllAsync()
        {
            return await _context.BookingServices.AsNoTracking().ToListAsync();
        }

        public async Task<BookingService?> GetByIdAsync(int id)
        {
            return await _context.BookingServices.AsNoTracking()
                .FirstOrDefaultAsync(c => c.BookingServiceId == id);
        }

        public async Task<BookingService> AddAsync(BookingService bookingService)
        {
            await _context.BookingServices.AddAsync(bookingService);
            return bookingService;
        }

        public async Task<BookingService> UpdateAsync(BookingService bookingService)
        {
            _context.BookingServices.Update(bookingService);
            await Task.CompletedTask;
            return bookingService;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.BookingServices.FindAsync(id);
            if (existing != null)
            {
                _context.BookingServices.Remove(existing);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
