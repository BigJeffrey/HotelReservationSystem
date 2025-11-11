using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class BookingServiceRepository(HotelDbContext context) : IBookingServiceRepository
    {
        private readonly HotelDbContext _context = context;

        public async Task<int> CountAsync()
        {
            return await _context.BookingServices.CountAsync();
        }

        public async Task<IEnumerable<BookingServiceEntity>> GetAllAsync(int page, int pageSize)
        {
            return await _context.BookingServices.AsNoTracking().AsNoTracking()
                .Include(bs => bs.Booking)
                .Include(bs => bs.ExtraService)
                .OrderBy(bs => bs.BookingServiceId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<BookingServiceEntity?> GetByIdAsync(int id)
        {
            return await _context.BookingServices.AsNoTracking()
                .Include(bs => bs.Booking)
                .Include(bs => bs.ExtraService)
                .FirstOrDefaultAsync(c => c.BookingServiceId == id);
        }

        public async Task<BookingServiceEntity> AddAsync(BookingServiceEntity bookingService)
        {
            await _context.BookingServices.AddAsync(bookingService);
            return bookingService;
        }

        public async Task<BookingServiceEntity> UpdateAsync(BookingServiceEntity bookingService)
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
