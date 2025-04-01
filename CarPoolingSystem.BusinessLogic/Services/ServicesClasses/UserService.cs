using CarPoolingSystem.BusinessLogic.Models.UserDtos;
using CarPoolingSystem.DataAccess.Entites.User;
using CarPoolingSystem.DataAccess.Interfaces.UnitOfWork;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace CarPoolingSystem.BusinessLogic.Services.ServicesClasses
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<User> _validator;
        private readonly string _uploadPath;

        public UserService(IUnitOfWork unitOfWork, IValidator<User> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public async Task<IEnumerable<UserDetailsDTO>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return users.Adapt<IEnumerable<UserDetailsDTO>>();
        }

        public async Task<UserDetailsDTO?> GetUserByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid user ID.");
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return user?.Adapt<UserDetailsDTO>();
        }

        public async Task AddUserAsync(CreateUserDTO userDto)
        {
            var user = userDto.Adapt<User>();
            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            if (await _unitOfWork.Users.FindAsync(u => u.Email == user.Email) != null)
                throw new Exception("Email already exists.");

            await _unitOfWork.Users.AddAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDTO userDto)
        {
            if (userDto.Id <= 0) throw new ArgumentException("Invalid user ID.");
            var user = userDto.Adapt<User>();

            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingUser = await _unitOfWork.Users.GetByIdAsync(user.Id);
            if (existingUser == null) throw new Exception("User not found.");

            await _unitOfWork.Users.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid user ID.");

            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found.");

            await _unitOfWork.Users.DeleteAsync(user);
        }

        public async Task<string> UploadImage(int userId, IFormFile file)
        {
            var user = _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null) 
                throw new Exception("User is not found. ");

            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");

            if (!string.IsNullOrEmpty(file.FileName))
            {
                string oldImagePath = Path.Combine(_uploadPath, Path.GetFileName(user.Result.ProfilePicture!));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(_uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string fileUrl = $"/uploads/{fileName}";

            user.Result.ProfilePicture = fileUrl;
            await _unitOfWork.Users.UpdateAsync(user.Result);
            return fileUrl;
        }

        public async Task<string> GetImage(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            return user.ProfilePicture ?? "No image available.";
        }
    }
}
