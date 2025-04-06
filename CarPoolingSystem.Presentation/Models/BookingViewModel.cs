namespace CarPoolingSystem.Presentation.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BookingViewModel
    {
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Ride ID is required.")]
        public int RideId { get; set; }

        [Required(ErrorMessage = "Driver Name is required.")]
        [StringLength(100, ErrorMessage = "Driver Name cannot be longer than 100 characters.")]
        public string? DriverName { get; set; }

        [Required(ErrorMessage = "Origin is required.")]
        [StringLength(200, ErrorMessage = "Origin cannot be longer than 200 characters.")]
        public string? Origin { get; set; }

        [Required(ErrorMessage = "Destination is required.")]
        [StringLength(200, ErrorMessage = "Destination cannot be longer than 200 characters.")]
        public string? Destination { get; set; }

        [Required(ErrorMessage = "Departure time is required.")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Seats booked is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select at least one seat.")]
        public int SeatsBooked { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
    }


}
