using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aertigo.Areas.User.Models
{
    public class GuestModel
    {
        public int?[] GuestID { get; set; }
        [Required(ErrorMessage = "Specify Guest Name")]
        public string[] GuestName { get; set; }
        [Required(ErrorMessage = "Specify Relation Type")]
        public string[] RelationType { get; set; }
        [Required(ErrorMessage = "Specify Guest Age")]
        public int?[] Age { get; set; }
        public int? BookingID { get; set; }
    }
}