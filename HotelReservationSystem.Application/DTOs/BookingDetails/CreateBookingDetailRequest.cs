using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.BookingDetails
{
    public class CreateBookingDetailRequest
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int RoomId { get; set; }
    }
}
