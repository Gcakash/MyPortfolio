using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Common.Models
{
    public class EducationModel
    {
        public int Id { get; set; }

        public string Degree { get; set; }

        public string School { get; set; }

        public string Location { get; set; }

        public string Score { get; set; }

        public string Activity { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? GraduationDate { get; set; }

    }

}
