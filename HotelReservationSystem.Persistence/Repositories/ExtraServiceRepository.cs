using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence.Repositories
{
    public class ExtraServiceRepository(HotelDbContext context) : IExtraServiceRepository
    {
        private readonly HotelDbContext _context = context;

        public async Task<IEnumerable<ExtraService>> GetAllAsync()
        {
            return await _context.ExtraServices.AsNoTracking().ToListAsync();
        }

        public async Task<ExtraService?> GetByIdAsync(int id)
        {
            return await _context.ExtraServices.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ExtraServiceId == id);
        }

        public async Task<ExtraService?> GetByNameAsync(string name)
        {
            return await _context.ExtraServices.FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower());
        }


        public async Task<ExtraService> AddAsync(ExtraService extraService)
        {
            await _context.ExtraServices.AddAsync(extraService);
            return extraService;
        }

        public async Task<ExtraService> UpdateAsync(ExtraService extraService)
        {
            _context.ExtraServices.Update(extraService);
            await Task.CompletedTask;
            return extraService;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.ExtraServices.FindAsync(id);
            if (existing != null)
            {
                _context.ExtraServices.Remove(existing);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
