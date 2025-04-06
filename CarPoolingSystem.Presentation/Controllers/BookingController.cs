using CarPoolingSystem.BusinessLogic.Models.BookingDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using CarPoolingSystem.Presentation.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarPoolingSystem.Presentation.Controllers
{
    // Apply Authorization at Controller Level for Admin and Driver Roles
    [Authorize(Roles = "Admin,Driver")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRideService _rideService;

        public BookingController(IBookingService bookingService, IRideService rideService)
        {
            _bookingService = bookingService;
            _rideService = rideService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();

            var model = bookings.Select(b => new BookingViewModel
            {
                BookingId = b.BookingId,
                RideId = b.RideId,
                DriverName = b.Driver == null ? "Mohamed" : b.Driver.Name,
                Origin = b.Ride.Origin,
                Destination = b.Ride.Destination,
                DateTime = b.Ride.DateTime,
                SeatsBooked = b.SeatsBooked,
                Price = b.Ride.Price * b.SeatsBooked
            }).ToList();

            return View(model);
        }

    
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingViewModel bookingDto)
        {
            var message = "Booking Created Successfully";
            try
            {
                if (!ModelState.IsValid)
                    return View(bookingDto);

                var createBookingDto = bookingDto.Adapt<CreateBookingDTO>();
                var created = await _bookingService.AddBookingAsync(createBookingDto);
                if (!created)
                {
                    message = "Failed to create Booking";
                }
            }
            catch (Exception ex)
            {
                message = "Failed to create Booking";
            }

            TempData["Message"] = message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var booking = model.Adapt<UpdateBookingDTO>();

            await _bookingService.UpdateBookingAsync(booking);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int bookingId)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingId);

            if (result)
            {
                TempData["SuccessMessage"] = "Booking cancelled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to cancel booking.";
            }

            return RedirectToAction("Index");
        }
    }
}
