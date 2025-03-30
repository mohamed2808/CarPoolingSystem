using CarPoolingSystem.DataAccess.Entites.Ride;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Validations
{
    public class RideValidator : AbstractValidator<Ride>
    {
        public RideValidator()
        {
            RuleFor(r => r.Origin)
                .NotEmpty().WithMessage("Origin is required.");

            RuleFor(r => r.Destination)
                .NotEmpty().WithMessage("Destination is required.");

            RuleFor(r => r.DateTime)
                .GreaterThan(DateTime.Now).WithMessage("Departure time must be in the future.");

            RuleFor(r => r.SeatsAvailable)
                .GreaterThan(0).WithMessage("At least one seat must be available.");
        }
    }
}
