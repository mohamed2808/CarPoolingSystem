using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPoolingSystem.DataAccess.Common.Enums;

namespace CarPoolingSystem.DataAccess.Entites.Booking
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int RideId { get; set; }

        [ForeignKey("RideId")]
        public virtual Ride.Ride Ride { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual User.User Customer { get; set; }

        public int DirverId { get; set; }
        [ForeignKey("DirverId")]
        public virtual User.User Dirver { get; set; }
        public int SeatsBooked { get; set; }

        public Status Status { get; set; } 
    }
}
