﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.UserDtos
{
    public record UserDetailsDTO(int Id,string FullName, string Email, string PhoneNumber, string Role);
}
