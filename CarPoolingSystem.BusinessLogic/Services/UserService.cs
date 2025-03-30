using CarPoolingSystem.DataAccess.Entites.User;
using CarPoolingSystem.DataAccess.Interfaces.Services;
using CarPoolingSystem.DataAccess.Interfaces.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Services
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid user ID.");
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            if (await _unitOfWork.Users.FindAsync(u => u.Email == user.Email) != null)
                throw new Exception("Email already exists.");

            await _unitOfWork.Users.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user.Id <= 0) throw new ArgumentException("Invalid user ID.");

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

            if (!String.IsNullOrEmpty(file.FileName))
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
