using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.Admin.Models
{
    public class OfferDetailsModel
    {
        public int? OfferID { get; set; }
        public int? HotelID { get; set; }
        public string HotelName { get; set; }
        public string OfferTitle { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
    }
}