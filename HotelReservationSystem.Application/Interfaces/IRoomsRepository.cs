using HotelReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Interfaces
{
    internal interface IRoomsRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task<Room> AddAsync(Room payment);
        Task<Room> UpdateAsync(Room payment);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
