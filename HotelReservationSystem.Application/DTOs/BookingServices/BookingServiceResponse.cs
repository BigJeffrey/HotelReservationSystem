namespace HotelReservationSystem.Application.DTOs.BookingServices
{
    public class BookingServiceResponse
    {
        public int BookingServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public ExtraServiceResponse ExtraService { get; set; } = new();
        public BookingResponse? Booking { get; set; }
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

    public class ExtraServiceResponse
    {
        public int ExtraServiceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}

