using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.Rooms
{
    public class CreateRoomRequest
    {
        [Required]
        [MaxLength(10)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
