using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Capstone.Web.Models
{
    public class UserModel
    {

        public int UserId { get; set; }
        //[RegularExpression(@"^(?=.[a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        [Required(ErrorMessage ="Enter Your UserName.")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Enter Your Password")]
        //[RegularExpression("^(?=.[A-Z].[A-Z])(?=.[!@#$&])(?=.[0-9].[0-9])(?=.[a-z].[a-z].*[a-z]).{8}$", ErrorMessage = "your password sucks")]
        public string Password { get; set; }

        
        public string PasswordSalt { get; set; }

        public bool Admin { get; set; }

        [Required(ErrorMessage ="Enter Your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Your Last Name")]
        public string LastName { get; set; }
    }
}