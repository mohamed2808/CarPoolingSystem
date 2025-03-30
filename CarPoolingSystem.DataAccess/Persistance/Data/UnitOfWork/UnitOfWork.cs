using CarPoolingSystem.DataAccess.Entites.Booking;
using CarPoolingSystem.DataAccess.Entites.Payment;
using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Entites.User;
using CarPoolingSystem.DataAccess.Interfaces;
using CarPoolingSystem.DataAccess.Persistance.Data.ApplicationDbContext;
using CarPoolingSystem.DataAccess.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Persistance.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarPoolingSystemDbContext _context;

        public IUserRepository Users { get; }
        public IRideRepository Rides { get; }
        public IBookingRepository Bookings { get; }
        public IPaymentRepository Payments { get; }

        public UnitOfWork(CarPoolingSystemDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Rides = new RideRepository(context);
            Bookings = new BookingRepository(context);
            Payments = new PaymentRepository(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
