using CarPoolingSystem.BusinessLogic.Services.ServicesClasses;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using CarPoolingSystem.BusinessLogic.Validations;
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
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CarPoolingSystemDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository,UserRepository>(); 
builder.Services.AddScoped<IBookingRepository,BookingRepository>();
builder.Services.AddScoped<IRideRepository,RideRepository>();
builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IValidator<User>,UserValidator>();
builder.Services.AddScoped<IValidator<Ride>, RideValidator>();
builder.Services.AddScoped<IValidator<Booking>, BookingValidator>();
builder.Services.AddScoped<IValidator<Payment>, PaymentValidator>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRideService, RideService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
