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
    public class WorkExperienceMap : IEntityTypeConfiguration<WorkExperience>
    {
        public void Configure(EntityTypeBuilder<WorkExperience> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.Company).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Location).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.IsCurrentyWorking).HasAnnotation("DefaultValue", "false");
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);
            builder.Property(x => x.Description).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Refrance).IsRequired(false).HasColumnType("nvarchar(max)");
        }
    }
}
