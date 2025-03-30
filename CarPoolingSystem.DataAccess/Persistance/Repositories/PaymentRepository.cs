using CarPoolingSystem.DataAccess.Entites.Payment;
using CarPoolingSystem.DataAccess.Interfaces.Repositories;
using CarPoolingSystem.DataAccess.Persistance.Data.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Persistance.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>,IPaymentRepository
    {
        public PaymentRepository(CarPoolingSystemDbContext context) : base(context) { }
    }
}
