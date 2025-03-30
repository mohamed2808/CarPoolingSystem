using CarPoolingSystem.DataAccess.Entites.Ride;
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
    public class RideService : IRideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Ride> _validator;

        public RideService(IUnitOfWork unitOfWork, IValidator<Ride> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<Ride>> GetAllRidesAsync()
        {
            return await _unitOfWork.Rides.GetAllAsync();
        }

        public async Task<Ride?> GetRideByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ride ID.");
            return await _unitOfWork.Rides.GetByIdAsync(id);
        }

        public async Task AddRideAsync(Ride ride)
        {
            var validationResult = await _validator.ValidateAsync(ride);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _unitOfWork.Rides.AddAsync(ride);
        }

        public async Task UpdateRideAsync(Ride ride)
        {
            if (ride.Id <= 0) throw new ArgumentException("Invalid ride ID.");

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
