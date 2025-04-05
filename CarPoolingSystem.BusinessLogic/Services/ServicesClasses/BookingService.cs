using CarPoolingSystem.BusinessLogic.Models.BookingDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using CarPoolingSystem.DataAccess.Entites.Booking;
using CarPoolingSystem.DataAccess.Interfaces.UnitOfWork;
using FluentValidation;
using Mapster;

namespace CarPoolingSystem.BusinessLogic.Services.ServicesClasses
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

        public async Task<IEnumerable<BookingDetailsDTO>> GetAllBookingsAsync()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            return bookings.Adapt<IEnumerable<BookingDetailsDTO>>();
        }

        public async Task<BookingDetailsDTO?> GetBookingByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid booking ID.");
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            return booking.Adapt<BookingDetailsDTO>();
        }

        public async Task<bool> AddBookingAsync(CreateBookingDTO bookingDto)
        {
            var booking = bookingDto.Adapt<Booking>();
            booking.CustomerId = 2;
            booking.DirverId = 2;
            var validationResult = await _validator.ValidateAsync(booking);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var ride = await _unitOfWork.Rides.GetByIdAsync(booking.RideId);
            if (ride == null) throw new Exception("Ride not found.");

            if (ride.SeatsAvailable < booking.SeatsBooked)
                throw new Exception("Not enough available seats.");

            ride.SeatsAvailable -= booking.SeatsBooked;
            await _unitOfWork.Rides.UpdateAsync(ride);
            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<int> UpdateBookingAsync(UpdateBookingDTO bookingDto)
        {
            if (bookingDto.BookingId <= 0) throw new ArgumentException("Invalid booking ID.");

            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingDto.BookingId);
            booking.SeatsBooked = bookingDto.SeatsBooked;
            if(booking is null) throw new Exception("Ride not found.");


            var validationResult = await _validator.ValidateAsync(bookingDto.Adapt<Booking>());
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            await _unitOfWork.Bookings.UpdateAsync(booking);
           return await _unitOfWork.SaveAsync() ;

        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid booking ID.");

            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null) throw new Exception("Booking not found.");

            await _unitOfWork.Bookings.DeleteAsync(booking);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
