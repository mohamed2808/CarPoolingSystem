using CarPoolingSystem.BusinessLogic.Models.ApplicationUserDtos;
using CarPoolingSystem.BusinessLogic.Services.Autho;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly AuthService _authService;

    public AccountController(AuthService authService)
    {
        _authService = authService;
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO model, string role = "Customer")
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.RegisterAsync(model, role);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO model)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
        }
        return View(model);
    }


    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Login");
    }
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View(); 
    }
}