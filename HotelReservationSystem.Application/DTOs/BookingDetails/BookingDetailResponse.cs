using HotelReservationSystem.Application.DTOs.Rooms;

namespace HotelReservationSystem.Application.DTOs.BookingDetails
{
    public class BookingDetailResponse
    {
        public int BookingDetailId { get; set; }
        public decimal Price { get; set; }
        public int Nights { get; set; }
        public BookingResponse? Booking { get; set; }
        public RoomResponse? Room { get; set; }
    }

    public class BookingResponse
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
