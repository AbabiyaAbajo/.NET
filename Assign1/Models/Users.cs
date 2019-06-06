using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public partial class Users
    {
        public Users()
        {
            BlogPosts = new HashSet<BlogPosts>();
            Comments = new HashSet<Comments>();
        }

        [Key]
        public int UserId { get; set; }
        
        public int RoleId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles Role { get; set; }
        public virtual ICollection<BlogPosts> BlogPosts { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
