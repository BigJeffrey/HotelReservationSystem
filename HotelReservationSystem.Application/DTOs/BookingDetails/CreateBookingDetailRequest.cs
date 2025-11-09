using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.BookingDetails
{
    public class CreateBookingDetailRequest
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Nights must be at least 1.")]
        public int Nights { get; set; }

    }
}
