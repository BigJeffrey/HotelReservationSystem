using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.ExtraServices
{
    public class UpdateExtraServiceRequest
    {
        [MaxLength(100, ErrorMessage = "Service name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal? Price { get; set; }
    }
}
