using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarPoolingSystem.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using System.Threading.Tasks;
using CarPoolingSystem.BusinessLogic.Models.RideDtos;

namespace CarPoolingSystem.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRideService _rideService;

    public HomeController(ILogger<HomeController> logger, IRideService rideService)
    {
        _logger = logger;
        _rideService = rideService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new RideSearchViewModel();
        model.Rides = await _rideService.GetAllRidesAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(RideSearchViewModel model)
    {
        
        if (ModelState.IsValid)
        {
            var rides = await _rideService.SearchAboutRide(new SearchRideDTO(model.Origin, model.Destination, model.DateTime));
            
            
            model.Rides = rides;

        }
        return View(model);
    }
    [Authorize(Roles = "Admin")]
    public IActionResult AdminPanel()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
