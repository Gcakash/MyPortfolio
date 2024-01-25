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
    public class BlogCommentMap : IEntityTypeConfiguration<BlogComment>
    {
        public void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100).HasColumnType("nvarchar");
            builder.Property(x => x.Message).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.HasOne(x => x.BlogPost).WithMany(x => x.BlogsComment).HasForeignKey(x => x.BlogId).IsRequired(false);
            builder.Property(x => x.PostDate).IsRequired(false);
        }
    }
}
