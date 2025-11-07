using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.Rooms
{
    public class UpdateRoomRequest
    {
        [MaxLength(10)]
        public string? RoomNumber { get; set; }

        [MaxLength(50)]
        public string? Type { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal? PricePerNight { get; set; }

        public bool? IsAvailable { get; set; }
    }
}
