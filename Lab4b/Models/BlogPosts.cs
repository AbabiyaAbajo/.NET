using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4b.Models
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BlogPosts
    {
        public int BlogPostId
        {
            get;
            set;
        }

        [Required]
        public int UserId
        {
            get;
            set;
        }

        [Required]
        [StringLength(200)]
        public string Title
        {
            get;
            set;
        }

        [Required]
        [StringLength(4000)]
        public string Content
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Posted
        {
            get;
            set;
        }
    }
}
