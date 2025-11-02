namespace HotelReservationSystem.Domain.Entities
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public decimal Price { get; set; }
        public int Nights { get; set; }

        public Booking Booking { get; set; } = null!;
        public Room Room { get; set; } = null!;
    }
}
