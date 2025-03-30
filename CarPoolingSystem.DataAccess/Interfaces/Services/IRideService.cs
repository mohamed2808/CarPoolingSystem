using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Interfaces.Services
{
    public interface IRideService
    {
        Task<IEnumerable<Ride>> GetAllRidesAsync();
        Task<Ride?> GetRideByIdAsync(int id);
        Task AddRideAsync(Ride Ride);
        Task UpdateRideAsync(Ride Ride);
        Task DeleteRideAsync(int id);
    }
}
