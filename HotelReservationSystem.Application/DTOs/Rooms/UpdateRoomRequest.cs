using System.ComponentModel.DataAnnotations;
using HotelReservationSystem.Domain;

namespace HotelReservationSystem.Application.DTOs.Rooms
{
    public class UpdateRoomRequest
    {
        [MaxLength(10)]
        public string? RoomNumber { get; set; }

        public RoomType? Type { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal? PricePerNight { get; set; }

        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10.")]
        public int? Capacity { get; set; }

        public bool? IsAvailable { get; set; }
    }
}
