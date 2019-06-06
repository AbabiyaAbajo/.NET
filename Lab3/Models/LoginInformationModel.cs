using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Controllers
{
    public class LoginInformationModel
    {
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string fname
        {
            get;
            set;
        }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string lname
        {
            get;
            set;
        }

        [Required]
        [StringLength(3)]
        [EmailAddress]
        public string age
        {
            get;
            set;
        }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string emailadd
        {
            get;
            set;
        }

        [Required]
        [StringLength(10)]
        [EmailAddress]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dob
        {
            get { return dob; }
            set { dob = value; }
        }

        [Required]
        [StringLength(100)]
        public string Password
        {
            get;
            set;
        }
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string description
        {
            get;
            set;
        }
    }
}
