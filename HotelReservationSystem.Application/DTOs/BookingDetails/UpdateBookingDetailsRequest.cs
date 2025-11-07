using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.BookingDetails
{
    public class UpdateBookingDetailsRequest
    {
        public int RoomId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Nights must be at least 1.")]
        public int Nights { get; set; }
    }
}
