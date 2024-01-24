using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }

        public int? Gender { get; set; }

        public DateTime? DOB { get; set; }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public bool IsActive { get; set; }

    }

}
