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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CarPoolingSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CarPoolingSystemDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Redirects to the MVC login page
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirects to the AccessDenied page
});


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

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireCustomerOrDriver", policy =>
        policy.RequireRole("Admin", "Driver"));
});


var app = builder.Build();

// Seed Roles and Migrate Database
 static IHost MigrateDatabase(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        CreateRoles(services, userManager).Wait();
    }
    return host;
}
MigrateDatabase(app.Services.GetRequiredService<IHost>());
static async Task CreateRoles(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roleNames = new[] { "Admin", "Passenger", "Driver" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var user = await userManager.FindByEmailAsync("admin@example.com");
    if (user != null)
    {
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
