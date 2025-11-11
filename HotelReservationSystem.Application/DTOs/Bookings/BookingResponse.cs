using HotelReservationSystem.Domain;

namespace HotelReservationSystem.Application.DTOs.Bookings
{
    public class BookingResponse
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public CustomerResponse? Customer { get; set; }

        public List<BookingDetailResponse> BookingDetails { get; set; } = new();
        public List<PaymentResponse> Payments { get; set; } = new();
        public List<BookingServiceResponse> ExtraServices { get; set; } = new();
    }

    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class BookingDetailResponse
    {
        public decimal Price { get; set; }
        public int Nights { get; set; }
        public RoomResponse Room { get; set; } = new();
    }

    public class RoomResponse
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public Boolean IsAvailable { get; set; }
    }

    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class BookingServiceResponse
    {
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public ExtraServiceResponse ExtraService { get; set; } = new();
    }

    public class ExtraServiceResponse
    {
        public int ExtraServiceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
