using System;

namespace HotelReservationSystem.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string? PaymentMethod { get; set; }
        public string Status { get; set; } = "pending";

        public Booking Booking { get; set; } = null!;
    }
}
