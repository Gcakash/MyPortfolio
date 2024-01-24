using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.API.Models;

namespace Wallbee.Support.Data.EntityMap
{
    public class AdminMap : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsActive).HasAnnotation("DefaultValue", "false");
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Password).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.ActivationExpireDate).IsRequired(false);            
        }
    }
}
