namespace HotelReservationSystem.Domain.Entities {
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
    }
}
