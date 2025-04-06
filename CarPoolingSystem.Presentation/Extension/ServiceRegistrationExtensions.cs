using CarPoolingSystem.BusinessLogic.Services.Autho;
using CarPoolingSystem.BusinessLogic.Services.ServicesClasses;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using CarPoolingSystem.BusinessLogic.Validations.Entities;
using CarPoolingSystem.DataAccess.Entites.ApplicationUsers;
using CarPoolingSystem.DataAccess.Entites.Booking;
using CarPoolingSystem.DataAccess.Entites.Payment;
using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Entites.User;
using CarPoolingSystem.DataAccess.Interfaces.Repositories;
using CarPoolingSystem.DataAccess.Interfaces.UnitOfWork;
using CarPoolingSystem.DataAccess.Persistance.Data.ApplicationDbContext;
using CarPoolingSystem.DataAccess.Persistance.Data.UnitOfWork;
using CarPoolingSystem.DataAccess.Persistance.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingSystem.Web.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<CarPoolingSystemDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Identity Services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CarPoolingSystemDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            // Scoped Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IRideRepository, RideRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // FluentValidation
            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<Ride>, RideValidator>();
            services.AddScoped<IValidator<Booking>, BookingValidator>();
            services.AddScoped<IValidator<Payment>, PaymentValidator>();

            // Application Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRideService, RideService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();

            // Authentication and Authorization
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<SignInManager<ApplicationUser>>();
            services.AddScoped<AuthService>();

            // Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireCustomerOrDriver", policy =>
                    policy.RequireRole("Admin", "Driver"));
            });

            return services;
        }
    }
}
