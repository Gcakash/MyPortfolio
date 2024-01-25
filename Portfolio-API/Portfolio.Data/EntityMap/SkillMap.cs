using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;
using Portfolio.API.Models;

namespace Portfolio.API.Data.EntityMap
{
    public class SkillMap : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(200).HasColumnType("nvarchar");
            builder.Property(x => x.RateOutOfFive).IsRequired(false);
                   
        }
    }
}
