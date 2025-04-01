using CarPoolingSystem.BusinessLogic.Models.BookingDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Validations.Dtos.Booking
{
    public class UpdateBookingDTOValidator : AbstractValidator<UpdateBookingDTO>
    {
        public UpdateBookingDTOValidator()
        {
            RuleFor(b => b.SeatsBooked)
                .InclusiveBetween(1, 10).WithMessage("Seats booked must be between 1 and 10.");
        }
    }
}
