using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aertigo.Areas.User.Models
{
    public class BookRoomModel
    {
        public int? BookingID { get; set; }
        public int? UserID { get; set; }
        public int? HotelID { get; set; }
        public string Fullname { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string UserImage { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelImage1 { get; set; }
        [Required(ErrorMessage = "Provide Check In Date")]
        public string CheckInDate { get; set; }
        [Required(ErrorMessage = "Provide Check Out Date")]
        public string CheckOutDate { get; set; }
        [Required(ErrorMessage = "Specify Number Of Rooms")]
        public int? NumberOfRooms { get; set; }
        [Required(ErrorMessage = "Specify Number Of Guests")]
        public int? NumberOfGuests { get; set; }
        [Required(ErrorMessage = "Specify Mode Of Payment")]
        public string ModeOfPayment { get; set; }
        public string PromoCode { get; set; }
        [Required(ErrorMessage = "Specify Expected Time")]
        public string ExpectedTime { get; set; }
        public int? TotalCost { get; set; }
        public GuestModel Guests { get; set; }
    }
}