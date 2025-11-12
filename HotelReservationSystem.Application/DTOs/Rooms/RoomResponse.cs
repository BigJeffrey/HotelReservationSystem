using HotelReservationSystem.Domain;
using HotelReservationSystem.Domain.Entities;

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

        public RoomResponse(Room room) {
            RoomId = room.RoomId;
            RoomNumber = room.RoomNumber;
            Type = room.RoomType;
            PricePerNight = room.PricePerNight;
            Capacity = room.Capacity;
            IsAvailable = room.IsAvailable;
        }
    }
}
