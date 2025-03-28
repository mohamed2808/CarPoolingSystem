using CarPoolingSystem.DataAccess.Entites.Booking;
using CarPoolingSystem.DataAccess.Entites.Payment;
using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Entites.User;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace CarPoolingSystem.DataAccess.Persistance.Data.ApplicationDbContext
{
   public class CarPoolingSystemDbContext : DbContext
    {
        public CarPoolingSystemDbContext(DbContextOptions<CarPoolingSystemDbContext> options) : base(options)
        {
        }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
