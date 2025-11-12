using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.DTOs.Payments;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;

        public PaymentService(IPaymentRepository paymentRepository, IBookingRepository bookingRepository)
        {
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<PagedResponse<PaymentResponse>> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _paymentRepository.CountAsync();

            var extraServices = await _paymentRepository.GetAllAsync(page, pageSize);

            var items = extraServices.Select(p => new PaymentResponse(p)).ToList();

            return new PagedResponse<PaymentResponse>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<PaymentResponse?> GetByIdAsync(int id)
        {
            var p = await _paymentRepository.GetByIdAsync(id);
            if (p == null)
                return null;

            return new PaymentResponse(p);
        }

        public async Task<Payment> AddAsync(CreatePaymentRequest request)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking is null)
                throw new InvalidOperationException("Specified booking does not exist.");

            var payment = new Payment
            {
                BookingId = request.BookingId,
                Amount = request.Amount,
                PaymentDate = request.PaymentDate,
                PaymentMethod = request.PaymentMethod,
                Status = string.IsNullOrWhiteSpace(request.Status) ? "pending" : request.Status
            };

            var created = await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return created;
        }

        public async Task<PaymentResponse?> UpdateAsync(int id, UpdatePaymentRequest request)
        {
            var existing = await _paymentRepository.GetByIdAsync(id);
            if (existing is null)
                return null;

            if (request.Amount.HasValue)
                existing.Amount = request.Amount.Value;

            if (!string.IsNullOrWhiteSpace(request.PaymentMethod))
                existing.PaymentMethod = request.PaymentMethod;

            if (!string.IsNullOrWhiteSpace(request.Status))
                existing.Status = request.Status;

            if (request.PaymentDate.HasValue)
                existing.PaymentDate = request.PaymentDate.Value;

            var p = await _paymentRepository.UpdateAsync(existing);
            await _paymentRepository.SaveChangesAsync();

            return new PaymentResponse(p);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _paymentRepository.GetByIdAsync(id);
            if (existing is null)
                return;

            await _paymentRepository.DeleteAsync(id);
            await _paymentRepository.SaveChangesAsync();
        }
    }
}
