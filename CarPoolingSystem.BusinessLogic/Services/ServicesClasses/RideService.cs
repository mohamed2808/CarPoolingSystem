using CarPoolingSystem.BusinessLogic.Models.RideDtos;
using CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces;
using CarPoolingSystem.DataAccess.Entites.Ride;
using CarPoolingSystem.DataAccess.Interfaces.UnitOfWork;
using FluentValidation;
using Mapster;

namespace CarPoolingSystem.BusinessLogic.Services.ServicesClasses
{
    public class RideService : IRideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Ride> _validator;

        public RideService(IUnitOfWork unitOfWork, IValidator<Ride> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<RideDetailsDTO>> GetAllRidesAsync()
        {
            var rides =  await _unitOfWork.Rides.GetAllAsync();
            return rides.Adapt<IEnumerable<RideDetailsDTO>>();
        }

        public async Task<RideDetailsDTO?> GetRideByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ride ID.");
            var ride = await _unitOfWork.Rides.GetByIdAsync(id);
            return ride?.Adapt<RideDetailsDTO>();
        }

        public async Task AddRideAsync(CreateRideDTO rideDto)
        {
            var ride = rideDto.Adapt<Ride>();
            var validationResult = await _validator.ValidateAsync(ride);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _unitOfWork.Rides.AddAsync(ride);
        }

        public async Task UpdateRideAsync(UpdateRideDTO rideDto)
        {
            if (rideDto.Id <= 0) throw new ArgumentException("Invalid ride ID.");
            var ride = rideDto.Adapt<Ride>();

            var validationResult = await _validator.ValidateAsync(ride);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingRide = await _unitOfWork.Rides.GetByIdAsync(ride.Id);
            if (existingRide == null) throw new Exception("Ride not found.");

            await _unitOfWork.Rides.UpdateAsync(ride);
        }

        public async Task DeleteRideAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ride ID.");

            var ride = await _unitOfWork.Rides.GetByIdAsync(id);
            if (ride == null) throw new Exception("Ride not found.");

            await _unitOfWork.Rides.DeleteAsync(ride);
        }
    }
}
