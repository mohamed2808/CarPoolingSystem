using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.RideDtos
{
    public record UpdateRideDTO(int Id, int SeatsAvailable, decimal Price);
}
