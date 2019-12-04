using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.User.Models
{
    public class CarousalModel
    {
        public string HotelName { get; set; }
        public string HotelImage1 { get; set; }
        public string HotelImage2 { get; set; }
        public string OfferPoster { get; set; }
        public string OfferTitle { get; set; }
        public string PromoCode { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string DiscountAmount { get; set; }
    }
}