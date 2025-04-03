using CarPoolingSystem.BusinessLogic.Models.ApplicationUserDtos;
using CarPoolingSystem.DataAccess.Entites.ApplicationUsers;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Services.Autho
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO model, string role = "Customer")
        {
            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign the role to the user
                await _userManager.AddToRoleAsync(user, role); // Assign role to the user
            }

            return result;
        }


        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            }
            return SignInResult.Failed;
        }

        // Logout user
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
