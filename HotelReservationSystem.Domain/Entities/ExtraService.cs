using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class ExtraService
    {
        public int ExtraServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
