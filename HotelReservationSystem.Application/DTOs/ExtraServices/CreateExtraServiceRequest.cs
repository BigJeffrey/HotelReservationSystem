using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.ExtraServices
{
    public class CreateExtraServiceRequest
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Service name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }
    }
}
