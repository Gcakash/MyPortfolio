
using Portfolio.API.Models;
using crypto;

namespace Portfolio.Data.DefaultSeed
{
    public class AdminSeed
    {
        public static List<Admin> DefaultAdminSeed()
        {
            List<Admin> adminUser = new List<Admin> {
            new Admin
            {
                Id = 1,
                UserName = "akash",
                IsActive = true,
                Password = "akashgc",
                Email = "akashgc2054@gmail.com",
            }};

            return adminUser;
        }
       
    }
}
