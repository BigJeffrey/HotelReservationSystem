using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.Bookings
{
    public class CreateBookingRequest
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "pending";

    }
}
