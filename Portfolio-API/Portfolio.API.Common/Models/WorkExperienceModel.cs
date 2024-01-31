using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Common.Models
{
    public class WorkExperienceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrentyWorking {  get; set; }

        public string Description { get; set; }

        public string Refrance { get; set; }

        public bool IsActive { get; set; }

    }

}
