using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.RideDtos
{
    public record UpdateRideDTO(int Id,string Origin,string Destination, int SeatsAvailable, decimal Price,DateTime DateTime);
}
