using System;
using System.Collections.Generic;

namespace assignment2Net.Models
{
    public partial class Photos
    {
        public int PhotoId { get; set; }
        public int BlogPostId { get; set; }
        public string Filename { get; set; }
        public string Url { get; set; }

        public virtual BlogPosts BlogPost { get; set; }
    }
}
