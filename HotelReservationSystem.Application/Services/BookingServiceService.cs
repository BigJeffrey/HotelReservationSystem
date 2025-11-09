using HotelReservationSystem.Application.DTOs.BookingServices;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Services
{
    public class BookingServiceService : IBookingServiceService
    {
        private readonly IBookingServiceRepository _bookingServiceRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IExtraServiceRepository _extraServiceRepository;

        public BookingServiceService(
            IBookingServiceRepository bookingServiceRepository,
            IBookingRepository bookingRepository,
            IExtraServiceRepository extraServiceRepository)
        {
            _bookingServiceRepository = bookingServiceRepository;
            _bookingRepository = bookingRepository;
            _extraServiceRepository = extraServiceRepository;
        }

        public async Task<IEnumerable<BookingServiceEntity>> GetAllAsync()
        {
            return await _bookingServiceRepository.GetAllAsync();
        }

        public async Task<BookingServiceEntity?> GetByIdAsync(int id)
            => await _bookingServiceRepository.GetByIdAsync(id);

        public async Task<BookingServiceEntity> AddAsync(CreateBookingServiceRequest request)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking is null)
                throw new InvalidOperationException("Specified booking does not exist.");

            var extraService = await _extraServiceRepository.GetByIdAsync(request.ExtraServiceId);
            if (extraService is null)
                throw new InvalidOperationException("Specified extra service does not exist.");

            var bookingService = new BookingServiceEntity
            {
                BookingId = request.BookingId,
                ExtraServiceId = request.ExtraServiceId,
                Quantity = request.Quantity,
                TotalPrice = request.TotalPrice
            };

            var created = await _bookingServiceRepository.AddAsync(bookingService);
            await _bookingServiceRepository.SaveChangesAsync();
            return created;
        }

        public async Task<BookingServiceEntity?> UpdateAsync(int id, UpdateBookingServiceRequest request)
        {
            var existing = await _bookingServiceRepository.GetByIdAsync(id);
            if (existing is null)
                return null;

            if (request.Quantity.HasValue)
                existing.Quantity = request.Quantity.Value;

            if (request.TotalPrice.HasValue)
                existing.TotalPrice = request.TotalPrice.Value;

            var updated = await _bookingServiceRepository.UpdateAsync(existing);
            await _bookingServiceRepository.SaveChangesAsync();

            return updated;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _bookingServiceRepository.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Booking service not found.");

            await _bookingServiceRepository.DeleteAsync(id);
            await _bookingServiceRepository.SaveChangesAsync();
        }
    }
}
