using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.BookingServices
{
    public class UpdateBookingServiceRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int? Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be non-negative.")]
        public decimal? TotalPrice { get; set; }
    }
}
