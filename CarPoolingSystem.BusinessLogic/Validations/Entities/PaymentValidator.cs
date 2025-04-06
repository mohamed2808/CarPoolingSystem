using CarPoolingSystem.DataAccess.Entites.Payment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Validations.Entities
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.BookingId)
                .GreaterThan(0).WithMessage("Booking ID must be valid.");

            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("Payment amount must be greater than 0.");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("Payment status is required.");
        }
    }
}
