using CarPoolingSystem.BusinessLogic.Models.UserDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesClasses;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingSystem.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var bookings = await _userService.GetAllUsersAsync();
            return View(bookings);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, UpdateUserDTO user)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<string> UploadImage(int id, IFormFile file)
        {
            return await _userService.UploadImage(id, file);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserImage(int id)
        {
            var image = await _userService.GetImage(id);
            return File(image, "image/jpeg");
        }
    }
}
