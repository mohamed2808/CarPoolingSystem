using CarPoolingSystem.DataAccess.Common.Entities;
using CarPoolingSystem.DataAccess.Common.Enums;
using System.ComponentModel.DataAnnotations;
namespace CarPoolingSystem.DataAccess.Entites.User
{
    public class User : BaseAuditableEntity<int>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; } 

        public Rating Rating { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
