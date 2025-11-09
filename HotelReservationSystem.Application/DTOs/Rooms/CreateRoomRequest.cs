using HotelReservationSystem.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.Rooms
{
    public class CreateRoomRequest
    {
        [Required]
        [MaxLength(10)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public RoomType Type { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal PricePerNight { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10.")]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
