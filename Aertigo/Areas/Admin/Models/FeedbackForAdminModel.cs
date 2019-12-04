using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.Admin.Models
{
    public class FeedbackForAdminModel
    {
        public int? FeedbackID { get; set; }
        public string UserImage { get; set; }
        public string EmailID { get; set; }
        public string Feedback { get; set; }
    }
}