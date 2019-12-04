using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.User.Models
{
    public class UserModel
    {
        public int? UserID { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = "Only Alphabets are allowded!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = "Only Alphabets are allowded!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter EmailID")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please Enter EmailID")]
        [Compare("Repassword", ErrorMessage = "Passwords Are Not Matching")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter EmailID")]
        public string Repassword { get; set; }
        [Required(ErrorMessage = "Please Provide Date Of Birth")]
        public string DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Select Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Select Your State")]
        public int? StateID { get; set; }
        [Required(ErrorMessage = "Please Select Your City Name")]
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Enter Your Mobile Number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Not Valid Mobile Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Your Address")]
        public string Address { get; set; }
        public string UserImage { get; set; }
        public int? CardID { get; set; }
        public bool isActive { get; set; }
    }
}