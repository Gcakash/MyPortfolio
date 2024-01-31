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
    public class UserInfoMap : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Image).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Gender).IsRequired(false);
            builder.Property(x => x.DOB).IsRequired(false);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.MiddleName).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Mobile).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.IsActive).HasAnnotation("DefaultValue", "true");
        }
    }
}
