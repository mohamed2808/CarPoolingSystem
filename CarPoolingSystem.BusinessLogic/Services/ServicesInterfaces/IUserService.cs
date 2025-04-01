using CarPoolingSystem.BusinessLogic.Models.UserDtos;
using CarPoolingSystem.DataAccess.Entites.User;
using Microsoft.AspNetCore.Http;
namespace CarPoolingSystem.BusinessLogic.Services.ServicesClasses
{
    public interface IUserService
    {
        Task<IEnumerable<UserDetailsDTO>> GetAllUsersAsync();
        Task<UserDetailsDTO?> GetUserByIdAsync(int id);
        Task AddUserAsync(CreateUserDTO user);
        Task UpdateUserAsync(UpdateUserDTO user);
        Task DeleteUserAsync(int id);
        Task<string> UploadImage(int userId,IFormFile file);
        Task<string> GetImage(int userId);
    }
}
