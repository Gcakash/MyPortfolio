﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.API.Models;

namespace Portfolio.API.Data.EntityMap
{
    public class BlogPostMap : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(x => x.BlogId);
            builder.Property(x => x.Image).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Content).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.DatePublished).IsRequired(false);
            builder.Property(x => x.IsActive).HasAnnotation("DefaultValue", "true");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");         
        }
    }
}
