using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using Portfolio.API.Models;

namespace Portfolio.API.Data.EntityMap
{
    public class EducationMap : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Degree).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.School).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Location).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Score).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Activity).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.StartDate).IsRequired(false);
            builder.Property(x => x.GraduationDate).IsRequired(false);
            builder.Property(x => x.IsActive).HasAnnotation("DefaultValue", "true");
        }
    }
}
