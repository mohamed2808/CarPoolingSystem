using CarPoolingSystem.DataAccess.Entites.Booking;
using CarPoolingSystem.DataAccess.Entites.Payment;
using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRideRepository Rides { get; }
        IBookingRepository Bookings { get; }
        IPaymentRepository Payments { get; }
        Task<int> SaveAsync();
    }
}
