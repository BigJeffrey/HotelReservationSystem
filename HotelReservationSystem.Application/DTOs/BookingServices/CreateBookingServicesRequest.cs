using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.BookingServices
{
    public class CreateBookingServicesRequest
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int ExtraServiceId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be non-negative.")]
        public decimal TotalPrice { get; set; }
    }
}
