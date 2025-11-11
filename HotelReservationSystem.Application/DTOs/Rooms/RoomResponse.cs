using HotelReservationSystem.Application.DTOs.BookingDetails;
using HotelReservationSystem.Domain;

namespace HotelReservationSystem.Application.DTOs.Rooms
{
    public class RoomResponse
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public Boolean IsAvailable { get; set; }
    }
}
