using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Interfaces;
using CarPoolingSystem.DataAccess.Persistance.Data.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Persistance.Repositories
{
    public class RideRepository: GenericRepository<Ride>,IRideRepository
    {
        public RideRepository(CarPoolingSystemDbContext context) : base(context) { }
    }
}
