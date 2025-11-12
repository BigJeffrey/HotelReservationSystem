using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.DTOs.ExtraServices
{
    public class ExtraServiceResponse
    {
        public int ExtraServiceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public ExtraServiceResponse(ExtraService e) {
            ExtraServiceId = e.ExtraServiceId;
            Name = e.Name;
            Description = e.Description;
            Price = e.Price;
        }
    }
}
