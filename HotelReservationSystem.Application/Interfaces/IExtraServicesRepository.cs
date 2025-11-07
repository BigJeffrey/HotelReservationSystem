using HotelReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Interfaces
{
    public interface IExtraServicesRepository
    {
        Task<IEnumerable<ExtraService>> GetAllAsync();
        Task<ExtraService?> GetByIdAsync(int id);
        Task<ExtraService> AddAsync(ExtraService extraService);
        Task<ExtraService> UpdateAsync(ExtraService extraService);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
