using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.Admin.Models
{
    public class PartnersViewModel
    {
        public int? HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelAddres { get; set; }
        public bool Status { get; set; }
    }
}