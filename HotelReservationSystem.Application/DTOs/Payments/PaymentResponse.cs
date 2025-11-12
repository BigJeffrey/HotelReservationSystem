using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.DTOs.Payments
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public PaymentResponse(Payment p)
        {
            PaymentId = p.PaymentId;
            Amount = p.Amount;
            PaymentDate = p.PaymentDate;
            PaymentMethod = p.PaymentMethod;
            Status = p.Status;
        }
    }
}
