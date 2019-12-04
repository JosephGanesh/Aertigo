using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aertigo.Areas.User.Models
{
    public class FeedBackModel
    {
        [Required(ErrorMessage = "Please Provide First Name")]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = "Only Alphabets are allowded!")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please Provide Email ID")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please Provide Phone Number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Not Valid Mobile Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Provide Feedback")]
        public string Feedback { get; set; }
    }
}