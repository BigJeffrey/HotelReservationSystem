using HotelReservationSystem.Application.DTOs.Bookings;
using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;
using System.Data.Common;

namespace HotelReservationSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<PagedResponse<BookingResponse>> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _bookingRepository.CountAsync();

            var bookings = await _bookingRepository.GetAllAsync(page, pageSize);

            var items = bookings.Select(b => new BookingResponse(b)).ToList();

            return new PagedResponse<BookingResponse>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<BookingResponse?> GetByIdAsync(int id)
        {
            var b = await _bookingRepository.GetByIdAsync(id);
            if (b is null)
                return null;

            return new BookingResponse(b);
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

        public async Task<BookingResponse?> UpdateAsync(int id, UpdateBookingRequest request)
        {
            var bookingToUpdate = await _bookingRepository.GetByIdAsync(id);
            if (bookingToUpdate == null)
                throw new KeyNotFoundException("Booking not found.");

            if (!string.IsNullOrWhiteSpace(request.Status))
                bookingToUpdate.Status = request.Status;


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
                bookingToUpdate.StartDate = request.StartDate;
                bookingToUpdate.EndDate = request.EndDate;
            }
            else
            {
                throw new InvalidOperationException("Start date must be earlier than end date.");
            }

            var updated = await _bookingRepository.UpdateAsync(bookingToUpdate);
            await _bookingRepository.SaveChangesAsync();

            return new BookingResponse(updated);
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
