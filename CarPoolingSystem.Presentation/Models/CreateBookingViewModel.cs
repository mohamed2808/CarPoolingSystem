namespace CarPoolingSystem.Presentation.Models
{
    public class CreateBookingViewModel
    {
        public int RideId { get; set; }
        public int CusomerId { get; set; } = 1;
        public int RideSeats { get; set; }
        public int SeatsBooked { get; set; }
        public DateTime DateTime { get; set; }
    }
}
