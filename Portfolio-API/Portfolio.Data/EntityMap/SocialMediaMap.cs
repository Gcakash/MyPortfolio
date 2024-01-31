using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Portfolio.API.Models;

namespace Portfolio.API.Data.EntityMap
{
    public class SocialMediaMap : IEntityTypeConfiguration<SocialMedia>
    {
        public void Configure(EntityTypeBuilder<SocialMedia> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Description).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.URL).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.IsActive).HasAnnotation("DefaultValue", "true");
        }
    }
}
