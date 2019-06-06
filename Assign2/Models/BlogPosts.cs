using System;
using System.Collections.Generic;

namespace assignment2Net.Models
{
    public partial class BlogPosts
    {
        public BlogPosts()
        {
            Comments = new HashSet<Comments>();
            Photos = new HashSet<Photos>();
        }

        public int BlogPostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public DateTime Posted { get; set; }
        public bool IsAvailable { get; private set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Photos> Photos { get; set; }
    }
}
