using System.ComponentModel.DataAnnotations.Schema;
using CarPoolingSystem.DataAccess.Common.Entities;

namespace CarPoolingSystem.DataAccess.Entites.Ride
{
    public class Ride : BaseAuditableEntity<int>
    {
        public int DriverId { get; set; }

        [ForeignKey("DriverId")]
        public virtual User.User Driver { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime DateTime { get; set; }

        public int SeatsAvailable { get; set; }

        public decimal Price { get; set; }
    }
}
