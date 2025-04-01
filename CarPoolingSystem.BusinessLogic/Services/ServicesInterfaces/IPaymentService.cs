using CarPoolingSystem.BusinessLogic.Models.PaymentDtos;
using CarPoolingSystem.DataAccess.Entites.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync();
        Task<PaymentDTO?> GetPaymentByIdAsync(int id);
        Task AddPaymentAsync(CreatePaymentDTO payment);
        Task UpdatePaymentAsync(UpdatePaymentDTO payment);
        Task DeletePaymentAsync(int id);
    }
}
