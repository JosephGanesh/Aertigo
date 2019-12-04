using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.User.Models
{
    public class UserCommentsModel
    {
        public int? CommentID { get; set; }
        public int? HotelID { get; set; }
        public string EmailID { get; set; }
        public List<BookRoomModel> BookingDetails { get; set; }
        public int? UserID { get; set; }
        public int? Rating { get; set; }
        public string Comment { get; set; }
    }
}