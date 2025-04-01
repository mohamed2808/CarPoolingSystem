using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.RideDtos
{
    public record CreateRideDTO(string Origin, string Destination, DateTime DateTime, int SeatsAvailable, decimal Price, int DriverId);
}
