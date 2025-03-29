﻿using CarPoolingSystem.DataAccess.Entites.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
    }
}
