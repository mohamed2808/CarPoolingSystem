using CarPoolingSystem.BusinessLogic.Models.PaymentDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using CarPoolingSystem.DataAccess.Entites.Payment;
using CarPoolingSystem.DataAccess.Interfaces.UnitOfWork;
using FluentValidation;
using Mapster;

namespace CarPoolingSystem.BusinessLogic.Services.ServicesClasses
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

        public async Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync()
        {
            var payments =  await _unitOfWork.Payments.GetAllAsync();
            return payments.Adapt<IEnumerable<PaymentDTO>>();
        }

        public async Task<PaymentDTO?> GetPaymentByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid payment ID.");
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            return payment.Adapt<PaymentDTO>();
        }

        public async Task AddPaymentAsync(CreatePaymentDTO paymentDto)
        {
            var payment = paymentDto.Adapt<Payment>();
            var validationResult = await _validator.ValidateAsync(payment);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var booking = await _unitOfWork.Bookings.GetByIdAsync(payment.BookingId);
            if (booking == null) throw new Exception("Booking not found.");

            await _unitOfWork.Payments.AddAsync(payment);
        }

        public async Task UpdatePaymentAsync(UpdatePaymentDTO paymentDto)
        {
            var payment = paymentDto.Adapt<Payment>();
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
