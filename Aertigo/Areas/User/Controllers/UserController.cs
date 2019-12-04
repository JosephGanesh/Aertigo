using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aertigo.Areas.User.Models;

namespace Aertigo.Areas.User.Controllers
{
    public class UserController : Controller
    {
        // GET: User/User
        public ActionResult UserRegistration()
        {
            UserModel user = new UserModel();
            UserModelManager manager = new UserModelManager();
            ViewBag.States = manager.GetAllStates();
            CardModel card = new CardModel();
            ViewBag.UserCard = card;
            return View("Registration", user);
        }
        private bool IsValidUser(UserModel user)
        {
            if (user.StateID != null && user.PhoneNumber != null && user.Password != null
                 && user.LastName != null && user.Gender != null && user.FirstName != null
                 && user.EmailID != null && user.DateOfBirth != null && user.CityID != null
                 && user.Address != null && user.Password == user.Repassword)
                return true;
            else
                return false;
        }

        [HttpPost]
        public ActionResult UserRegistration(UserModel user, HttpPostedFileBase file)
        {
            UserModelManager manager = new UserModelManager();
            if (IsValidUser(user))
            {
                string image = string.Empty;
                string cardNumber = null;
                if (file != null)
                {
                    image = Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath(string.Format("~/Areas/User/UserImages/{0}", image)));
                }
                if (TempData["cardNumber"] != null)
                {
                    cardNumber = TempData["cardNumber"].ToString();
                    TempData.Remove("cardNumber");
                }
                user.UserImage = image;
                user.isActive = true;
                if (cardNumber != null)
                {
                    manager.AddNewUserWithCardID(user, cardNumber);
                    TempData["success"] = true;
                }
                else
                {
                    manager.AddNewUserWithoutCardID(user);
                    TempData["success"] = true;
                }
                return Redirect("~/Login/MainLogin");
            }
            ViewBag.States = manager.GetAllStates();
            CardModel card = new CardModel();
            ViewBag.UserCard = card;
            return View("Registration", user);
        }

        public JsonResult GetCities(int stateID)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            UserModelManager manager = new UserModelManager();
            cities = manager.GetAllCities(stateID);
            return Json(cities);
        }


        public JsonResult GetCityName(int cityID)
        {
            SelectListItem city = new SelectListItem();
            UserModelManager manager = new UserModelManager();
            city = manager.GetCityName(cityID);
            return Json(city);
        }

        public ActionResult AddNewCard(CardModel card)
        {
            TempData["cardNumber"] = card.CardNumber;
            ModelState.Clear();
            CardModel cardDetails = new CardModel();
            CardModelManager manager = new CardModelManager();
            manager.AddNewCard(card);
            return PartialView("_UserCardDetails", cardDetails);
        }
    }
}