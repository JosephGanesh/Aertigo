using Aertigo.Areas.HotelManagement.Models;
using Aertigo.Areas.User.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aertigo.Areas.HotelManagement.Controllers
{
    public class HosterController : Controller
    {
        // GET: HotelManagement/Hoster
        [Authorize(Roles = "Hoster, Admin")]
        public ActionResult Dashboard()
        {
            if (Session["hotelID"] != null && Session["hosterPassword"] != null || Session["adminEmail"] != null && Session["adminPassword"] != null)
            {
                HotelModelManager manager = new HotelModelManager();
                ViewBag.TodayCount = 0;
                ViewBag.TodayCount = manager.GetBookingsTotalCount(Convert.ToInt32(Session["hotelID"]), DateTime.Now.Date, DateTime.Now.Date);
                ViewBag.Details = manager.GetBookedUsersDetails(Convert.ToInt32(Session["hotelID"]));
                ViewBag.Comments = manager.GetUsersComments(Convert.ToInt32(Session["hotelID"]));
                return View("Home");
            }
            else
            {
                return RedirectToAction("MainLogin", "Login", new { area = "" });
            }
        }

        [Authorize(Roles = "Hoster, Admin")]
        public ActionResult Settings()
        {
            if (Session["hotelID"] != null && Session["hosterPassword"] != null || Session["adminEmail"] != null && Session["adminPassword"] != null)
            {
                HotelFacilityViewModel merge = new HotelFacilityViewModel();
                FacilityModelManager manager = new FacilityModelManager();
                UserModelManager userModule = new UserModelManager();
                HotelModelManager hotelManager = new HotelModelManager();
                merge.Facilities = manager.GetFacilityOnHotelID(Convert.ToInt32(Session["hotelID"]));
                merge.Hotel = hotelManager.GetHotelDetails(Convert.ToInt32(Session["hotelID"]));
                ViewBag.States = userModule.GetAllStates();
                return View(merge);
            }
            else
            {
                return RedirectToAction("MainLogin", "Login", new { area = "" });
            }
        }

        private bool validateSettings(HotelFacilityViewModel merge)
        {
            if (merge.Hotel.HotelName != null && merge.Hotel.StateID != null && merge.Hotel.Address != null
                && merge.Hotel.CityID != null && merge.Facilities != null && merge.Hotel.ContactNumber != null
                && merge.Hotel.EmailID != null && merge.Hotel.CityID != null && merge.Hotel.HotelStartedDate != null
                && merge.Hotel.AboutHotel != null && merge.Hotel.PriceFor1BHK != null && merge.Hotel.PriceFor2BHK != null
                && merge.Hotel.FacebookAddress != null && merge.Hotel.InstagramAddress != null && merge.Hotel.GoogleAPI != null
                && merge.Hotel.Password != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        public ActionResult Settings(HotelFacilityViewModel merge, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
        {
            if (validateSettings(merge))
            {
                string image1 = string.Empty;
                string image2 = string.Empty;
                string image3 = string.Empty;
                string image4 = string.Empty;
                if (file1 != null && file2 != null && file3 != null && file4 != null)
                {
                    // Saving Image In Local Folder
                    image1 = Path.GetFileName(file1.FileName);
                    image2 = Path.GetFileName(file2.FileName);
                    image3 = Path.GetFileName(file3.FileName);
                    image4 = Path.GetFileName(file4.FileName);
                    string image1Path = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}", image1));
                    string image2Path = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}", image2));
                    string image3Path = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}", image3));
                    string image4Path = Server.MapPath(string.Format("~/Areas/HotelManagement/HotelImages/{0}", image4));
                    file1.SaveAs(image1Path);
                    file2.SaveAs(image2Path);
                    file3.SaveAs(image3Path);
                    file4.SaveAs(image4Path);
                    merge.Hotel.Image1 = image1;
                    merge.Hotel.image2 = image2;
                    merge.Hotel.Image3 = image3;
                    merge.Hotel.image4 = image4;
                }
                merge.Hotel.HotelID = Convert.ToInt32(Session["hotelID"]);
                HotelModelManager hotelModuleManager = new HotelModelManager();
                hotelModuleManager.UpdateHotelDetails(merge, this.User.IsInRole("Admin"));
                ViewBag.Success = true;
            }
            UserModelManager userModule = new UserModelManager();
            ViewBag.States = userModule.GetAllStates();
            FacilityModelManager manager = new FacilityModelManager();
            HotelModelManager hotelManager = new HotelModelManager();
            merge.Hotel = hotelManager.GetHotelDetails(Convert.ToInt32(Session["hotelID"]));
            merge.Facilities = manager.GetAllFacilities();
            return View("Settings", merge);
        }
        [Authorize(Roles = "Hoster, Admin")]
        public ActionResult Offers()
        {
            if (Session["hotelID"] != null && Session["hosterPassword"] != null || Session["adminEmail"] != null && Session["adminPassword"] != null)
            {
                OffersModelManager manager = new OffersModelManager();
                int count = manager.CheckActiveOffer(Convert.ToInt32(Session["hotelID"]));
                ViewBag.Status = (count > 0) ? "Active" : "InActive";
                return View();
            }
            else
            {
                return RedirectToAction("MainLogin", "Login", new { area = "" });
            }
        }

        private bool validateOffersDetails(OffersModel offer)
        {
            if (offer.ValidTo != null && offer.ValidFrom != null && offer.TermsAndCondition != null && offer.PromoCode != null
                && offer.OfferTitle != null && offer.OfferPoster != null && offer.DiscountAmount != null && offer.AboutOffer != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        public ActionResult Offers(OffersModel offer, HttpPostedFileBase file)
        {
            OffersModelManager manager = new OffersModelManager();
            offer.HotelID = Convert.ToInt32(Session["hotelID"]);
            offer.OfferPoster = file.FileName;
            if (validateOffersDetails(offer))
            {
                string image = Path.GetFileName(file.FileName);
                string path = Server.MapPath(string.Format("~/Areas/HotelManagement/OfferPosters/{0}", image));
                file.SaveAs(path);
                manager.AddNewOffer(offer);
                ViewBag.Status = "Active";
            }
            return View(offer);
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }

        public ActionResult GetBookingsCount(int? hotelID, DateTime? fromDate, DateTime? toDate)
        {
            HotelModelManager manager = new HotelModelManager();
            int count = manager.GetBookingsTotalCount(hotelID, fromDate, toDate);
            return Json(count);
        }

        public ActionResult DeactivateHotel(int? hotelID)
        {
            HotelModelManager manager = new HotelModelManager();
            manager.DeactivateHotel(hotelID);
            Session.RemoveAll();
            Session.Abandon();
            return Json("Success");
        }
    }
}