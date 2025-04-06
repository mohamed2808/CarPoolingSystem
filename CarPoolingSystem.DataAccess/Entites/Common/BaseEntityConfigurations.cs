using CarPoolingSystem.DataAccess.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.DataAccess.Entites.Common
{
    public class BaseEntityConfigurations<TKey, TEntity> : IEntityTypeConfiguration<TEntity>
              where TKey : IEquatable<TKey>
              where TEntity : BaseEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(E => E.Id);
        }
    }
}
