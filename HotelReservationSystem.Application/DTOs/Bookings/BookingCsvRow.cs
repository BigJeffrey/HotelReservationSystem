namespace HotelReservationSystem.Application.DTOs.Bookings
{
    internal class BookingCsvRow
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }

        // Customer info
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
