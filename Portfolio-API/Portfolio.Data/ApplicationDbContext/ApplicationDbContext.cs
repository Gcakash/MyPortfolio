using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models;
using Portfolio.API.Data.EntityMap;
using Portfolio.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Data.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // this to configure the contex
        }
        //OnModelCreating() method is used to configure the model using ModelBuilder Fluent API


        //Adding Domain Classes as DbSet Properties
        public DbSet<Admin> Admins { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<SocialMedia> SocialMedias { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<WorkExperience> WorkExperiences { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this to configure the model

            modelBuilder.ApplyConfiguration(new AdminMap());
            modelBuilder.ApplyConfiguration(new BlogPostMap());
            modelBuilder.ApplyConfiguration(new ContactMap());
            modelBuilder.ApplyConfiguration(new EducationMap());
            modelBuilder.ApplyConfiguration(new FeedbackMap());
            modelBuilder.ApplyConfiguration(new SkillMap());
            modelBuilder.ApplyConfiguration(new SocialMediaMap());
            modelBuilder.ApplyConfiguration(new UserInfoMap());
            modelBuilder.ApplyConfiguration(new WorkExperienceMap());
            modelBuilder.ApplyConfiguration(new BlogCommentMap());

            ApplicationDbConfiguration.Seed(modelBuilder);
        }


    }
}
