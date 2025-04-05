using CarPoolingSystem.BusinessLogic.Models.BookingDtos;
using CarPoolingSystem.BusinessLogic.Models.RideDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using CarPoolingSystem.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarPoolingSystem.Presentation.Controllers
{
    public class RideController : Controller
    {
        private readonly IRideService _rideService;

        public RideController(IRideService rideService)
        {
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
        [HttpGet("ride/offer")]
        public IActionResult GetOfferRide(CreateRideDTO ride)
        {
            return View("OfferRide", ride);
        }
        [Authorize(Roles = "Admin,Driver")]
        [ValidateAntiForgeryToken]
        [HttpPost("ride/offer")]
        public async Task<IActionResult> OfferRide(CreateRideDTO ride)
        {
            if (!ModelState.IsValid)
            {
                return View(ride);
            }

            await _rideService.AddRideAsync(ride);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ride = await _rideService.GetRideByIdAsync(id);
            if (ride == null)
            {
                return NotFound();
            }
            return View(ride);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRideDTO ride)
        {
            if (ModelState.IsValid)
            {
                await _rideService.UpdateRideAsync(ride);
                return RedirectToAction("Index");
            }
            return View(ride);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
               await _rideService.DeleteRideAsync(id);   
               return RedirectToAction("Index");         
        }
    }
}
