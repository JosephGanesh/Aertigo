using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Aertigo.Areas.HotelManagement.Models;
using Aertigo.Areas.User.Models;

namespace Aertigo.Areas.HotelManagement.Controllers
{
    public class HotelController : Controller
    {
        // GET: HotelManagement/Hotel
        public ActionResult Registration()
        {
            HotelFacilityViewModel merge = new HotelFacilityViewModel();
            FacilityModelManager manager = new FacilityModelManager();
            UserModelManager userModule = new UserModelManager();
            merge.Hotel = new Models.HotelModel();
            merge.Facilities = manager.GetAllFacilities();
            ViewBag.States = userModule.GetAllStates();
            ViewBag.Success = false;
            return View("Registration", merge);
        }

        private bool validateHotelDetails(HotelFacilityViewModel merge)
        {
            if (merge.Hotel.HotelName != null && merge.Hotel.StateID != null && merge.Hotel.Address != null
                    && merge.Hotel.CityID != null && merge.Facilities != null && merge.Hotel.ContactNumber != null
                    && merge.Hotel.EmailID != null && merge.Hotel.CityID != null && merge.Hotel.HotelStartedDate != null
                    && merge.Hotel.AboutHotel != null && merge.Hotel.PriceFor1BHK != null && merge.Hotel.PriceFor2BHK != null
                    && merge.Hotel.FacebookAddress != null && merge.Hotel.InstagramAddress != null && merge.Hotel.GoogleAPI != null
                    && merge.Hotel.Password != null && merge.Hotel.ContactNumber.Length <= 10)
                return true;
            else
                return false;
        }

        [HttpPost]
        public ActionResult Registration(HotelFacilityViewModel merge, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
        {
            if (validateHotelDetails(merge))
            {
                string certifacte = string.Empty;
                string image1 = string.Empty;
                string image2 = string.Empty;
                string image3 = string.Empty;
                string image4 = string.Empty;

                List<FacilityModel> facilities = new List<FacilityModel>();
                for (int i = 0; i < merge.Facilities.Count; i++)
                {
                    if (merge.Facilities[i].isSelected)
                    {
                        facilities.Add(merge.Facilities[i]);
                    }
                }
                merge.Facilities = facilities;
                if (file != null && file1 != null && file2 != null && file3 != null && file4 != null)
                {
                    // Saving Image In Local Folder
                    certifacte = Path.GetFileName(file.FileName);
                    image1 = Path.GetFileName(file1.FileName);
                    image2 = Path.GetFileName(file2.FileName);
                    image3 = Path.GetFileName(file3.FileName);
                    image4 = Path.GetFileName(file4.FileName);
                    string certificatePath = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}",certifacte));
                    string image1Path = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}",image1));
                    string image2Path = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}", image2));
                    string image3Path = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}" ,image3));
                    string image4Path = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}" ,image4));
                    file.SaveAs(certificatePath);
                    file1.SaveAs(image1Path);
                    file2.SaveAs(image2Path);
                    file3.SaveAs(image3Path);
                    file4.SaveAs(image4Path);
                }
                HotelModel hotel = new HotelModel();
                hotel = merge.Hotel;
                hotel.HotelCertificate = certifacte;
                hotel.Image1 = image1;
                hotel.image2 = image2;
                hotel.Image3 = image3;
                hotel.image4 = image4;
                HotelModelManager hotelModuleManager = new HotelModelManager();
                hotelModuleManager.AddNewHotel(merge);
                TempData["success"] = true;
                return Redirect("~/Login/MainLogin");
            }
            UserModelManager userModule = new UserModelManager();
            ViewBag.States = userModule.GetAllStates();
            FacilityModel facility = new FacilityModel();
            FacilityModelManager manager = new FacilityModelManager();
            merge.Hotel = new HotelModel();
            merge.Facilities = manager.GetAllFacilities();
            return View("Registration", merge);
        }
    }
}