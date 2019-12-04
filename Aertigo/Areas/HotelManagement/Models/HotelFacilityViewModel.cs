using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aertigo.Areas.HotelManagement.Models;

namespace Aertigo.Areas.HotelManagement.Models
{
    public class HotelFacilityViewModel
    {
        public HotelModel Hotel { get; set; }
        public List<FacilityModel> Facilities { get; set; }
    }
}