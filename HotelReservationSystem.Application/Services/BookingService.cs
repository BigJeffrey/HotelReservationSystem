using HotelReservationSystem.Application.DTOs.Bookings;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _bookingRepository.GetAllAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _bookingRepository.GetByIdAsync(id);
        }

        public async Task<Booking> AddAsync(CreateBookingRequest request)
        {
            if (request.StartDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Start date cannot be in the past.");
            }

            if (request.EndDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("End date cannot be in the past.");
            }

            if (request.StartDate >= request.EndDate)
            {
                throw new InvalidOperationException("Start date must be earlier than end date.");
            }

            Booking newBooking = new()
            {
                CustomerId = request.CustomerId,
                BookingDate = DateTime.UtcNow,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status
            };

            Booking createdBooking = await _bookingRepository.AddAsync(newBooking);
            await _bookingRepository.SaveChangesAsync();

            return createdBooking;
        }

        public async Task<Booking?> UpdateAsync(int id, UpdateBookingRequest request)
        {
            var BookingToUpdate = await _bookingRepository.GetByIdAsync(id);
            if (BookingToUpdate == null)
                return null;

            if (!string.IsNullOrWhiteSpace(request.Status))
                BookingToUpdate.Status = request.Status;


            if (request.StartDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Start date cannot be in the past.");
            }

            if (request.EndDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("End date cannot be in the past.");
            }

            if (request.StartDate < request.EndDate)
            {
                BookingToUpdate.StartDate = request.StartDate;
                BookingToUpdate.EndDate = request.EndDate;
            }
            else
            {
                throw new InvalidOperationException("Start date must be earlier than end date.");
            }

            var udpated = await _bookingRepository.UpdateAsync(BookingToUpdate);
            await _bookingRepository.SaveChangesAsync();

            return udpated;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _bookingRepository.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Booking not found.");

            await _bookingRepository.DeleteAsync(id);
            await _bookingRepository.SaveChangesAsync();
        }
    }
}
