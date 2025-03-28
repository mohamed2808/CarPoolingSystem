using CarPoolingSystem.DataAccess.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Entites.Common
{
    public class BaseAuditableEntityConfigurations<TKey, TEntity> : BaseEntityConfigurations<TKey, TEntity> where TKey : IEquatable<TKey>
       where TEntity : BaseAuditableEntity<TKey>
    {

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(E => E.CreatedBy).HasColumnType("varchar(50)");
            builder.Property(E => E.LastModifiedBy).HasColumnType("varchar(50)");
            builder.Property(E => E.CreatedOn).HasComputedColumnSql("GETDATE()");
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
