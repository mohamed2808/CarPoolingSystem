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
        public async Task<IEnumerable<RideDetailsDTO>> SearchAboutRide(SearchRideDTO rideDto)
        {
           if (rideDto == null) {throw new ArgumentNullException(nameof(rideDto));}
           var rides = await _unitOfWork.Rides.GetAllAsync();
            var filteredRides = rides.Where(r => r.Origin.Equals(rideDto.Origin, StringComparison.CurrentCultureIgnoreCase) && r.Destination.Equals(rideDto.Destination,StringComparison.OrdinalIgnoreCase) && r.DateTime.Equals(rideDto.DateTime)).ToList();
            return filteredRides.Adapt<IEnumerable<RideDetailsDTO>>();
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
            ride.CreatedOn = DateTime.Now.AddMinutes(5);
            ride.CreatedBy = "Dirver";
            ride.LastModifiedBy = "Driver";
            ride.DriverId = 3;
            var validationResult = await _validator.ValidateAsync(ride);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _unitOfWork.Rides.AddAsync(ride);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateRideAsync(UpdateRideDTO rideDto)
        {
            if (rideDto.Id <= 0) throw new ArgumentException("Invalid ride ID.");

            var existingRide = await _unitOfWork.Rides.GetByIdAsync(rideDto.Id);
            existingRide.Origin = rideDto.Origin;
            existingRide.Destination = rideDto.Destination;
            existingRide.DateTime = rideDto.DateTime;
            existingRide.SeatsAvailable = rideDto.SeatsAvailable;
            existingRide.Price = rideDto.Price;
            if (existingRide == null) throw new Exception("Ride not found.");
            var validationResult = await _validator.ValidateAsync(rideDto.Adapt<Ride>());
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            await _unitOfWork.Rides.UpdateAsync(existingRide);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRideAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ride ID.");

            var ride = await _unitOfWork.Rides.GetByIdAsync(id);
            if (ride == null) throw new Exception("Ride not found.");

            await _unitOfWork.Rides.DeleteAsync(ride);
            await _unitOfWork.SaveAsync();
        }
    }
}
