using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.HotelManagement.Models
{
    public class BookedUsersDetailsModel
    {
        public int BookingID { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string ExpectedArrivingTime { get; set; }
        public string NumberOfGuests { get; set; }
        public int NumberOfRooms{ get; set; }
        public string ModeOfPayment { get; set; }
    }
}