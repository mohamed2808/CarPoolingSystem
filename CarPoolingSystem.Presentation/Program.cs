using CarPoolingSystem.Web.Extensions;
using CarPoolingSystem.DataAccess.Entites.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


await MigrateDatabaseAsync(app.Services);


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();  
app.UseAuthorization();   

app.UseStaticFiles();
app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();

static async Task MigrateDatabaseAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    
    var roleNames = new[] { "Admin", "Passenger", "Driver" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
            {

                Console.WriteLine($"Failed to create role {roleName}");
            }
        }
    }

   
    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullName = adminEmail,
        };
        var createResult = await userManager.CreateAsync(adminUser, "Admin123!"); 
        if (createResult.Succeeded)
        {
           
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            
            Console.WriteLine("Failed to create admin user");
        }
    }
    else
    {
        
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
