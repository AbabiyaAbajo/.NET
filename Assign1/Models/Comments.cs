using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public partial class Comments
    {
        [Key]
        public int CommentId { get; set; }
        
        public int BlogPostId { get; set; }
        
        public int UserId { get; set; }
        [StringLength(2048)]
        public string Content { get; set; }

        [ForeignKey("BlogPostId")]
        public virtual BlogPosts BlogPost { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
    }
}
