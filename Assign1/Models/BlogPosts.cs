using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public partial class BlogPosts
    {
        public BlogPosts()
        {
            Comments = new HashSet<Comments>();
        }
        [Key]
        public int BlogPostId { get; set; }
        
        public int UserId { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Posted { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
