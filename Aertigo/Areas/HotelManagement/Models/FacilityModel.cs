using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Aertigo.Areas.HotelManagement.Models
{
    public class FacilityModel
    {
        public int? FacilityID { get; set; }
        public string Description { get; set; }
        public bool isSelected { get; set; }
    }
}