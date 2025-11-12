using HotelReservationSystem.Application.DTOs.BookingDetails;
using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Services
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingDetailService(
            IBookingDetailRepository bookingDetailRepository,
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<PagedResponse<BookingDetailResponse>> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _bookingDetailRepository.CountAsync();

            var bookingDetails = await _bookingDetailRepository.GetAllAsync(page, pageSize);

            var items = bookingDetails.Select(b => new BookingDetailResponse(b)).ToList();

            return new PagedResponse<BookingDetailResponse>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<BookingDetailResponse?> GetByIdAsync(int id)
        {
            var b = await _bookingDetailRepository.GetByIdAsync(id);
            if (b is null)
                return null;

            return new BookingDetailResponse(b);            
        }

        public async Task<BookingDetail> AddAsync(CreateBookingDetailRequest request)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking is null)
                throw new InvalidOperationException("Specified booking does not exist.");

            var room = await _roomRepository.GetByIdAsync(request.RoomId);
            if (room is null)
                throw new InvalidOperationException("Specified room does not exist.");

            var bookingDetail = new BookingDetail
            {
                BookingId = request.BookingId,
                RoomId = request.RoomId,
                Price = request.Price,
                Nights = request.Nights
            };

            var created = await _bookingDetailRepository.AddAsync(bookingDetail);
            await _bookingDetailRepository.SaveChangesAsync();
            return created;
        }

        public async Task<BookingDetailResponse?> UpdateAsync(int id, UpdateBookingDetailRequest request)
        {
            var existing = await _bookingDetailRepository.GetByIdAsync(id);
            if (existing is null)
                return null;

            if (request.RoomId.HasValue)
            {
                var room = await _roomRepository.GetByIdAsync(request.RoomId.Value);
                if (room is null)
                    throw new InvalidOperationException("Specified room does not exist.");

                existing.RoomId = request.RoomId.Value;
            }

            if (request.Price.HasValue)
                existing.Price = request.Price.Value;

            if (request.Nights.HasValue)
                existing.Nights = request.Nights.Value;

            var b = await _bookingDetailRepository.UpdateAsync(existing);
            await _bookingDetailRepository.SaveChangesAsync();

            return new BookingDetailResponse(b);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _bookingDetailRepository.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Booking detail not found.");

            await _bookingDetailRepository.DeleteAsync(id);
            await _bookingDetailRepository.SaveChangesAsync();
        }
    }
}
