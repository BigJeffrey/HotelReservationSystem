using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.Bookings
{
    public class UpdateBookingRequest
    {
        public int CustomerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "pending";
    }
}
