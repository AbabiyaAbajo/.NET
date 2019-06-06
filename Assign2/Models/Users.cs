using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace assignment2Net.Models
{
    public partial class Users
    {
        public Users()
        {
            BlogPosts = new HashSet<BlogPosts>();
            Comments = new HashSet<Comments>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<BlogPosts> BlogPosts { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
