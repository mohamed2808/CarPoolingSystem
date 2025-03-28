using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Entites.Booking
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

             builder.HasOne(b => b.Customer)
                    .WithMany()
                    .HasForeignKey(b => b.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
             builder.HasOne(b => b.Dirver)
                    .WithMany()
                    .HasForeignKey(b => b.DirverId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
