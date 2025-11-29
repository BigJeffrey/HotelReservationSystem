namespace HotelReservationSystem.Application.DTOs.Customers
{
    public class CsvImportResult
    {
        public int CreatedCount { get; set; }
        public int FailedCount { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
