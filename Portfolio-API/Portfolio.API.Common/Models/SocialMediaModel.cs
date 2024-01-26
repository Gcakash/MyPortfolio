using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Common.Models
{
    public class SocialMediaModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public string Description { get; set; }
    }

}
