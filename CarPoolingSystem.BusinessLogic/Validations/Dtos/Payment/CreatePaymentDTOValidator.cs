using CarPoolingSystem.BusinessLogic.Models.PaymentDtos;
using CarPoolingSystem.DataAccess.Common.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Validations.Dtos.Payment
{
    public class CreatePaymentDTOValidator : AbstractValidator<CreatePaymentDTO>
    {
        public CreatePaymentDTOValidator()
        {
            RuleFor(p => p.BookingId)
                .GreaterThan(0).WithMessage("Booking ID must be a positive number.");

            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("Amount must be a positive number.")
                .LessThanOrEqualTo(10000).WithMessage("Amount cannot exceed 10,000.");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => status == Status.Pending || status == Status.Completed || status == Status.Confirmed || status == Status.Cancelled || status == Status.Failed )
                .WithMessage("Status must be 'Pending', 'Completed' , 'Failed' ,'Cancelled', or 'Confirmed'.");
        }
    }
}
