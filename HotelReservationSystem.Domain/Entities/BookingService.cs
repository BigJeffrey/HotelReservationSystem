namespace HotelReservationSystem.Domain.Entities
{
    public class BookingService
    {
        public int BookingServiceId { get; set; }
        public int BookingId { get; set; }
        public int ExtraServiceId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal TotalPrice { get; set; }

        public Booking Booking { get; set; } = null!;
        public ExtraService ExtraService { get; set; } = null!;
    }
}
