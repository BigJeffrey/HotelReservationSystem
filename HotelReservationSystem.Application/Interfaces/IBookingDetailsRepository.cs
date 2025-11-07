using HotelReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Interfaces
{
    public interface IBookingDetailsRepository
    {
        Task<IEnumerable<BookingDetail>> GetAllAsync();
        Task<BookingDetail?> GetByIdAsync(int id);
        Task<BookingDetail> AddAsync(BookingDetail bookingDetail);
        Task<BookingDetail> UpdateAsync(BookingDetail bookingDetail);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
