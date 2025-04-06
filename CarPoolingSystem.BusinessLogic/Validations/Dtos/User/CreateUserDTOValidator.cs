using CarPoolingSystem.BusinessLogic.Models.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Validations.Dtos.User
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(u => u.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .Length(3, 50).WithMessage("Full Name must be between 3 and 50 characters.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid phone number format.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "Customer" || role == "Driver" || role == "Admin")
                .WithMessage("Role must be 'Customer', 'Driver', or 'Admin'.");
        }
    }
}
