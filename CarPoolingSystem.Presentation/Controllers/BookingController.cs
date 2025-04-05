using CarPoolingSystem.BusinessLogic.Models.BookingDtos;
using CarPoolingSystem.BusinessLogic.Models.RideDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.Presentation.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingSystem.Presentation.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRideService _rideService;

        public BookingController(IBookingService bookingService,IRideService rideService)
        {
            _bookingService = bookingService;
            _rideService = rideService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            var listOfBookings = bookings.ToList();

            var model = listOfBookings.Select(b => new BookingViewModel
            {
                Id = b.Id,
                RideId = b.RideId,
                DriverName = b.Driver == null ? "mohamed" : b.Driver.Name ,
                Origin = b.Ride.Origin,
                Destination = b.Ride.Destination,
                DateTime = b.Ride.DateTime,
                SeatsBooked = b.SeatsBooked,
                Price = b.Ride.Price * b.SeatsBooked
            }).ToList();

            return View(model);
        }

       

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookingViewModel bookingDto)
        {
            var message = "Booking Created Successfully";
            try
            {
                if (!ModelState.IsValid)
                    return View(bookingDto);
                var createBookingDto = bookingDto.Adapt<CreateBookingDTO>();
                var Created = await _bookingService.AddBookingAsync(createBookingDto);
                if (!Created)
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

       
    
        public async Task<IActionResult> Edit(int? id)
        {
           if(!id.HasValue) return BadRequest();
           var booking = await _bookingService.GetBookingByIdAsync(id.Value);
            if (booking == null) return NotFound();
            else
            {
                booking.Adapt<UpdateBookingDTO>();
            }
            TempData["Id"] = id.Value;
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, UpdateBookingDTO booking)
        {
            if (((int?)TempData["Id"]) != id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Id", "Invalid Id");
                return BadRequest(ModelState);
            }

            var message = "Booking Updated Successfully";
            try
            {
                var updateBookingDto = booking.Adapt<UpdateBookingDTO>();
                var udpated = await _bookingService.UpdateBookingAsync(updateBookingDto) > 0;
                if (!udpated)
                {
                    message = "Failed to update department";
                }
            }
            catch (Exception ex)
            {
                message = "Failed to update department";
            }
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int bookingId)
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
