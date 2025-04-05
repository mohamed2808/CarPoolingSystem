namespace CarPoolingSystem.Presentation.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public int RideId { get; set; }
        public string? DriverName { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public DateTime DateTime { get; set; }
        public int SeatsBooked { get; set; }
        public decimal Price { get; set; }
    }

}
