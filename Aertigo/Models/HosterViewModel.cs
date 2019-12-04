using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aertigo.Models
{
    public class HosterViewModel
    {
        [Required(ErrorMessage = "Please Provide Hotel ID")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Not Valid Hotel ID")]
        public int? HotelID { get; set; }
        [Required(ErrorMessage = "Please Provide Hotel Password")]
        public string Password { get; set; }
    }
}