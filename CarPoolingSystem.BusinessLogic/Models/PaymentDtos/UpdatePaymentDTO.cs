using CarPoolingSystem.DataAccess.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.PaymentDtos
{
    public record UpdatePaymentDTO(int Id, Status Status);
}
