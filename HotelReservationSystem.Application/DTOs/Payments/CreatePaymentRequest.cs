using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.Payments
{
    public class CreatePaymentRequest
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [MaxLength(20)]
        public string? Status { get; set; } = "pending";
    }
}
