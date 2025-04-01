using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.BookingDtos
{
    public record BookingDetailsDTO(int Id, int RideId, int UserId, int SeatsBooked);
}
