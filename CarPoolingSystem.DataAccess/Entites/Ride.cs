using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Entites
{
    public class Ride
    {
        [Key]
        public int RideId { get; set; }

        [Required]
        public int DriverId { get; set; }

        [ForeignKey("DriverId")]
        public User Driver { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        public DateTime DateTime { get; set; }

        public int SeatsAvailable { get; set; }

        public decimal Price { get; set; }
    }
}
