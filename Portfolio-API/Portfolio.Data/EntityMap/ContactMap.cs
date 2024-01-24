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
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Image).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.GenderId).IsRequired(false);
            builder.Property(x => x.DOB).IsRequired(false);
            builder.Property(x => x.IsDeleted).HasAnnotation("DefaultValue", "false");
            builder.Property(x => x.ApplicationId).IsRequired();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.MiddleName).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Mobile).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.ActivationCode).IsRequired(false).HasMaxLength(10).HasColumnType("nvarchar");
            builder.Property(x => x.ActivationExpireDate).IsRequired(false);            
        }
    }
}
