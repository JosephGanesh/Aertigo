using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.Admin.Models
{
    public class UsersModel
    {
        public int? UserID { get; set; }
        public string EmailID { get; set; }
        public string TotalBookings { get; set; }
        public bool Status { get; set; }
    }
}