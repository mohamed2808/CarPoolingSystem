using CarPoolingSystem.BusinessLogic.Models.BookingDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Validations.Dtos.Booking
{
    public class CreateBookingDTOValidator : AbstractValidator<CreateBookingDTO>
    {
        public CreateBookingDTOValidator()
        {
            RuleFor(b => b.RideId)
                .GreaterThan(0).WithMessage("Ride ID must be a positive number.");

            RuleFor(b => b.UserId)
                .GreaterThan(0).WithMessage("User ID must be a positive number.");

            RuleFor(b => b.SeatsBooked)
                .InclusiveBetween(1, 10).WithMessage("Seats booked must be between 1 and 10.");
        }
    }
}
