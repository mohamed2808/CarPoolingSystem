using CarPoolingSystem.DataAccess.Entites.Booking;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Validations.Entities
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(b => b.RideId)
                .GreaterThan(0).WithMessage("Ride ID must be valid.");

            RuleFor(b => b.CustomerId)
                .GreaterThan(0).WithMessage("Customer ID must be valid.");

            RuleFor(b => b.SeatsBooked)
                .GreaterThan(0).WithMessage("At least one seat must be booked.");
        }
    }
}
