using HotelReservationSystem.Application.DTOs.Rooms;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.DTOs.BookingDetails
{
    public class BookingDetailResponse
    {
        public int BookingDetailId { get; set; }
        public decimal Price { get; set; }
        public int Nights { get; set; }
        public BookingResponse? Booking { get; set; }
        public RoomResponse? Room { get; set; }

        public BookingDetailResponse(BookingDetail bd) 
        {
            BookingDetailId = bd.BookingDetailId;
            Price = bd.Price;
            Nights = bd.Nights;
            Booking = new BookingResponse
            {
                BookingId = bd.Booking.BookingId,
                CustomerId = bd.Booking.Customer.CustomerId,
                BookingDate = bd.Booking.BookingDate,
                StartDate = bd.Booking.StartDate,
                EndDate = bd.Booking.EndDate,
                Status = bd.Booking.Status,
            };
            Room = new RoomResponse(bd.Room);
        }
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
