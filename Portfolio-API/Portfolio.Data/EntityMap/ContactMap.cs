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
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100).HasColumnType("nvarchar");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Phone).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Country).IsRequired(false).HasMaxLength(20).HasColumnType("nvarchar");
            builder.Property(x => x.FullAddress1).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.FullAddress2).IsRequired(false).HasColumnType("nvarchar(max)");
        }
    }
}
