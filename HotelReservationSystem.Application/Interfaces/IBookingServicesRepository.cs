using HotelReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Interfaces
{
    public interface IBookingServicesRepository
    {
        Task<IEnumerable<BookingService>> GetAllAsync();
        Task<BookingService?> GetByIdAsync(int id);
        Task<BookingService> AddAsync(BookingService bookingService);
        Task<BookingService> UpdateAsync(BookingService bookingService);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
