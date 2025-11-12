using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.DTOs.Customers
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<BookingsResponse> Bookings { get; set; } = new();

        public CustomerResponse(Customer c)
        {
            CustomerId = c.CustomerId;
            FirstName = c.FirstName;
            LastName = c.LastName;
            Email = c.Email;
            PhoneNumber = c.PhoneNumber;
            Bookings = c.Bookings.Select(b => new BookingsResponse
            {
                BookingId = b.BookingId,
                BookingDate = b.BookingDate,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Status = b.Status
            }).ToList();
        }
    }

    public class BookingsResponse
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
