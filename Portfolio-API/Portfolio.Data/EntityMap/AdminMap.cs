using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.API.Models;

namespace Portfolio.API.Data.EntityMap
{
    public class AdminMap : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsActive).HasAnnotation("DefaultValue", "true");
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Password).IsRequired(true).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.ActivationExpireDate).IsRequired(false);            
        }
    }
}
