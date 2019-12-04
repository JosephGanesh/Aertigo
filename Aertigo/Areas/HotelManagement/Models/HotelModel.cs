using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Aertigo.Areas.HotelManagement.Models
{
    public class HotelModel
    {
        public int HotelID { get; set; }
        public string HotelCertificate { get; set; }
        [Required(ErrorMessage = "Enter Your Hotel Name")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "Select Your State")]
        public int? StateID { get; set; }
        [DisplayName("City Name")]
        [Required(ErrorMessage = "Select Your City")]
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Select Hotel Started Date")]
        public string HotelStartedDate { get; set; }
        [Required(ErrorMessage = "Please Provide Hotel Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Provide About Your Hotel")]
        public string AboutHotel { get; set; }
        [Required(ErrorMessage = "Please Provide Price For 1BHK")]
        public int? PriceFor1BHK { get; set; }
        [Required(ErrorMessage = "Please Provide Price For 2BHK")]
        public int? PriceFor2BHK { get; set; }
        [Required(ErrorMessage = "Please Provide Hotel Google API")]
        public string GoogleAPI { get; set; }
        [Required(ErrorMessage = "Please Provide Hotel EMail ID")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please Set Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Provide Your Contact Number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Not Valid Mobile Number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Provide Facebook Page URL")]
        public string FacebookAddress { get; set; }
        [Required(ErrorMessage = "Provide Instagram Page URL")]
        public string InstagramAddress { get; set; }
        public string Image1 { get; set; }
        public string image2 { get; set; }
        public string Image3 { get; set; }
        public string image4 { get; set; }
        public bool isActive { get; set; }
    }
}