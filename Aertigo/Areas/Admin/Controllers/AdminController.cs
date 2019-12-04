using Aertigo.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aertigo.Areas.HotelManagement.Models;
using System.IO;

namespace Aertigo.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        AdminModuleManager manager = new AdminModuleManager();
        [Authorize(Roles = "Admin")]
        public ActionResult Dashboard()
        {
            if (Session["adminEmail"] != null && Session["adminPassword"] != null)
            {
                ViewBag.Count = manager.GetTodayBookedCount();
                ViewBag.BookedDetails = manager.GetAllBookedDetails();
                return View();
            }
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetTotalCountOnGivenDate(DateTime fromDate, DateTime toDate)
        {
            int count = manager.GetTotalCountOnGivenDate(fromDate, toDate);
            return Json(count);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Partners()
        {
            if (Session["adminEmail"] != null && Session["adminPassword"] != null)
            { 
                List<PartnersViewModel> hotelsDetails = manager.GetHotelsDetails();
                return View(hotelsDetails);
            }
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Offers()
        {
            if (Session["adminEmail"] != null && Session["adminPassword"] != null)
            {
                List<OfferDetailsModel> offerDetails = manager.GetAllOffersDetails();
                return View(offerDetails);
            }
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OffersDetails(int? hotelID, int? offerID)
        {
            if (Session["adminEmail"] != null && Session["adminPassword"] != null)
            {
                OffersModel offer = new OffersModel();
                offer = manager.GetOfferDetails(hotelID);
                return View(offer);
            }
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateOfferDetails(OffersModel offer, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string image = Path.GetFileName(file.FileName);
                    string path = Server.MapPath("~/Areas/HotelManagement/OfferPosters/" + image);
                    file.SaveAs(path);
                    offer.OfferPoster = file.FileName;
                }
                manager.ApproveOffer(offer);
                return RedirectToAction("Offers");
            }
            return View("OffersDetails", offer);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Requests()
        {
            if (Session["adminEmail"] != null && Session["adminPassword"] != null)
            {
                List<PartnersViewModel> hotelsDetails = manager.GetInActiveHotelsDetails();
                return View(hotelsDetails);
            }
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ExploreRequestHotelDetails(int? hotelID)
        {
            Session["hotelID"] = hotelID;
            return RedirectToAction("Settings", "Hoster", new { area = "HotelManagement" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {
            if (Session["adminEmail"] != null && Session["adminPassword"] != null)
            {
                List<UsersModel> usersDetails = manager.GetUsersDetails();
                return View(usersDetails);
            }
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GoToSpecificPartner(int? hotelID)
        {
            Session["hotelID"] = hotelID;
            return RedirectToAction("Dashboard", "Hoster", new { area = "HotelManagement" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteComment(int? commentID)
        {
            manager.DeleteUserComment(commentID);
            return RedirectToAction("Dashboard", "Hoster", new { area = "HotelManagement" });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeActivateHotel(int? hotelID)
        {
            manager.DeactivateHotel(hotelID);
            List<PartnersViewModel> hotelsDetails = manager.GetHotelsDetails();
            return View("Partners", hotelsDetails);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteOffer(int? offerID)
        {
            manager.DeleteOffer(offerID);
            return RedirectToAction("Offers");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RejectHotelRequest(int? hotelID)
        {
            manager.DeleteHotel(hotelID);
            return RedirectToAction("Requests");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ActivateUser(int? userID)
        {
            manager.ActivateUser(userID);
            return RedirectToAction("Users");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeActivateUser(int? userID)
        {
            manager.DeActivateUser(userID);
            return RedirectToAction("Users");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Feedbacks()
        {
            ViewBag.Feedbacks = manager.GetAllFeedbackDetails();
            return View();
        }
    }
}