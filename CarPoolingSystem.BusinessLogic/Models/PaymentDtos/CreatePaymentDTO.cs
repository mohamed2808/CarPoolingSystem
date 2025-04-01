using CarPoolingSystem.DataAccess.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Models.PaymentDtos
{
    public record CreatePaymentDTO(int BookingId, decimal Amount, Status Status);
}
