using CarPoolingSystem.DataAccess.Entites.Booking;
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
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Booking> _validator;

        public BookingService(IUnitOfWork unitOfWork, IValidator<Booking> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _unitOfWork.Bookings.GetAllAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid booking ID.");
            return await _unitOfWork.Bookings.GetByIdAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            var validationResult = await _validator.ValidateAsync(booking);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var ride = await _unitOfWork.Rides.GetByIdAsync(booking.RideId);
            if (ride == null) throw new Exception("Ride not found.");

            if (ride.SeatsAvailable < booking.SeatsBooked)
                throw new Exception("Not enough available seats.");

            ride.SeatsAvailable -= booking.SeatsBooked;
            await _unitOfWork.Bookings.AddAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            if (booking.CustomerId <= 0) throw new ArgumentException("Invalid booking ID.");

            var validationResult = await _validator.ValidateAsync(booking);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingBooking = await _unitOfWork.Bookings.GetByIdAsync(booking.CustomerId);
            if (existingBooking == null) throw new Exception("Booking not found.");

            await _unitOfWork.Bookings.UpdateAsync(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid booking ID.");

            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null) throw new Exception("Booking not found.");

            await _unitOfWork.Bookings.DeleteAsync(booking);
        }
    }
}
