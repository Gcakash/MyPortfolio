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
    public class FeedbackMap : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100).HasColumnType("nvarchar"); ;
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar"); ;
            builder.Property(x => x.Message).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
        }
    }
}
