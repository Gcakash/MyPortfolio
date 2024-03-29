﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Common.Models
{
    public class BlogCommentModel
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public string FullName { get; set; }

        public string Message { get; set; }

        public DateTime? PostDate { get; set; }

        public bool IsActive { get; set; }

    }

}
