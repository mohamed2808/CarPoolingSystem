using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.BookingDtos
{
    public record BookingDetailsDTO(int Id, int RideId, User? Driver, Ride Ride,int SeatsBooked);
}
