using CarPoolingSystem.BusinessLogic.Models.BookingDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingSystem.Presentation.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingDTO booking)
        {
            if (!ModelState.IsValid) return View(booking);

            await _bookingService.AddBookingAsync(booking);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBookingDTO booking)
        {
            if (!ModelState.IsValid) return View(booking);

            await _bookingService.UpdateBookingAsync(booking);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
