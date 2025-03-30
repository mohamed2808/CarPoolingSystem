using CarPoolingSystem.DataAccess.Entites.Payment;
using CarPoolingSystem.DataAccess.Interfaces.Services;
using CarPoolingSystem.DataAccess.Interfaces.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Payment> _validator;

        public PaymentService(IUnitOfWork unitOfWork, IValidator<Payment> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _unitOfWork.Payments.GetAllAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid payment ID.");
            return await _unitOfWork.Payments.GetByIdAsync(id);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            var validationResult = await _validator.ValidateAsync(payment);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var booking = await _unitOfWork.Bookings.GetByIdAsync(payment.BookingId);
            if (booking == null) throw new Exception("Booking not found.");

            await _unitOfWork.Payments.AddAsync(payment);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            if (payment.PaymentId <= 0) throw new ArgumentException("Invalid payment ID.");

            var validationResult = await _validator.ValidateAsync(payment);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingPayment = await _unitOfWork.Payments.GetByIdAsync(payment.PaymentId);
            if (existingPayment == null) throw new Exception("Payment not found.");

            await _unitOfWork.Payments.UpdateAsync(payment);
        }

        public async Task DeletePaymentAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid payment ID.");

            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null) throw new Exception("Payment not found.");

            await _unitOfWork.Payments.DeleteAsync(payment);
        }
    }
}
