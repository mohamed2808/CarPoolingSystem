using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.UserDtos
{
    public record CreateUserDTO(string FullName, string Email, string PhoneNumber, string Password, string Role);
}
