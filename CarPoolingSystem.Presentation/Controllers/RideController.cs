using CarPoolingSystem.BusinessLogic.Models.RideDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var ride = _rideService.GetRideByIdAsync(id);
            if (ride == null)
            {
                return NotFound();
            }
            return View(ride);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRideDTO ride)
        {
            if (ModelState.IsValid)
            {
                await _rideService.AddRideAsync(ride);
                return RedirectToAction("Index");
            }
            return View(ride);
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
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ride = await _rideService.GetRideByIdAsync(id);
            if (ride == null)
            {
                return NotFound();
            }
            return View(ride);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int ride)
        {
            await _rideService.DeleteRideAsync(ride);
            return RedirectToAction("Index");
        }
    }
}
