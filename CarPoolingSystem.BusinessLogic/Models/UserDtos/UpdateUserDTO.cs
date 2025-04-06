using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.UserDtos
{
    public record UpdateUserDTO(int Id, string FullName, string PhoneNumber,string Email,string Role);
}
