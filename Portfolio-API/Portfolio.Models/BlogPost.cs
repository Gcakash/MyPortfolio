using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Models
{
    public class BlogPost
    {
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public DateTime? DatePublished { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<BlogComment> BlogsComment { get; set; }

    }

}
