using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Aertigo.Areas.User.Models;

namespace Aertigo.Areas.HotelManagement.Models
{
    public class CommentsHosterViewModel
    {
        public int? CommentID { get; set; }
        public string EmailID { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
        public string UserImage { get; set; }
    }
}