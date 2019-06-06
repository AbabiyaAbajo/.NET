using System;
using System.Collections.Generic;

namespace assignment2Net.Models
{
    public partial class Comments
    {
        public int CommentId { get; set; }
        public int BlogPostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public virtual BlogPosts BlogPost { get; set; }
        public virtual Users User { get; set; }

        //public virtual ICollection<BadWords> BadWords { get; set; }
    }
}
