using CarPoolingSystem.BusinessLogic.Models.RideDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Validations.Dtos.Ride
{
    public class CreateRideDTOValidator : AbstractValidator<CreateRideDTO>
    {
        public CreateRideDTOValidator()
        {
            RuleFor(r => r.DriverId)
                .GreaterThan(0).WithMessage("Driver ID must be a positive number.");

            RuleFor(r => r.SeatsAvailable)
                .GreaterThan(0).WithMessage("Seats available must be a positive number.");

            RuleFor(r => r.Price)
                .GreaterThan(0).WithMessage("Price per seat must be greater than 0.");

            RuleFor(r => r.DateTime)
                .GreaterThan(DateTime.Now).WithMessage("Departure time must be in the future.");
        }
    }
}
