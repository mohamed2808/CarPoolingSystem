using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPoolingSystem.DataAccess.Common.Entities;

namespace CarPoolingSystem.DataAccess.Entites.Payment
{
    public class Payment 
    {
        [Key]
        public int PaymentId { get; set; }
        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking.Booking Booking { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } // "Cash", "Card"

        public string Status { get; set; } // "Paid", "Pending"
    }
}
