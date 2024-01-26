using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Common.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Country { get; set; }

        public string FullAddress1 { get; set; }

        public string FullAddress2 { get; set; }

    }

}
