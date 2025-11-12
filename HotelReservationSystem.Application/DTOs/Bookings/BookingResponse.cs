using HotelReservationSystem.Domain;
using HotelReservationSystem.Domain.Entities;

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

        public BookingResponse(Booking b)
        {
            BookingId = b.BookingId;
            BookingDate = b.BookingDate;
            StartDate = b.StartDate;
            EndDate = b.EndDate;
            Status = b.Status;
            Customer = new CustomerResponse
            {
                CustomerId = b.Customer.CustomerId,
                FirstName = b.Customer.FirstName,
                LastName = b.Customer.LastName,
                Email = b.Customer.Email,
                PhoneNumber = b.Customer.PhoneNumber
            };
            BookingDetails = b.BookingDetails.Select(d => new BookingDetailResponse
            {
                Nights = d.Nights,
                Price = d.Price,
                Room = new RoomResponse
                {
                    RoomId = d.Room.RoomId,
                    RoomNumber = d.Room.RoomNumber,
                    Type = d.Room.RoomType,
                    PricePerNight = d.Room.PricePerNight,
                    Capacity = d.Room.Capacity,
                    IsAvailable = d.Room.IsAvailable
                }
            }).ToList();
            Payments = b.Payments.Select(p => new PaymentResponse
            {
                PaymentId = p.PaymentId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod,
                Status = p.Status
            }).ToList();
            ExtraServices = b.BookingServices.Select(s => new BookingServiceResponse
            {
                TotalPrice = s.TotalPrice,
                Quantity = s.Quantity,
                ExtraService = new ExtraServiceResponse
                {
                    ExtraServiceId = s.ExtraService.ExtraServiceId,
                    Name = s.ExtraService.Name,
                    Description = s.ExtraService.Description,
                    Price = s.ExtraService.Price
                }
            }).ToList();
        }
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
