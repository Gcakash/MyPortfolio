using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models;
using Portfolio.Data.DefaultSeed;
using System;
using System.Threading.Tasks;

namespace Portfolio.API.Data
{
    public class ApplicationDbConfiguration
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(AdminSeed.DefaultAdminSeed());
 
        }
    }
}
