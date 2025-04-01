using CarPoolingSystem.BusinessLogic.Models.RideDtos;
using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces
{
    public interface IRideService
    {
        Task<IEnumerable<RideDetailsDTO>> GetAllRidesAsync();
        Task<RideDetailsDTO?> GetRideByIdAsync(int id);
        Task AddRideAsync(CreateRideDTO Ride);
        Task UpdateRideAsync(UpdateRideDTO Ride);
        Task DeleteRideAsync(int id);
    }
}
