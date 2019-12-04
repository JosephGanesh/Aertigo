using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.Admin.Models
{
    public class BookedDetailsModel
    {
        public string HotelName { get; set; }
        public string EmailID { get; set; }
        public string NumberOfRooms { get; set; }
        public string TotalCost { get; set; }
        public string ModeOfPayment { get; set; }
    }
}