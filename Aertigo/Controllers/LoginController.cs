using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aertigo.Models;
using Aertigo.Areas.User.Models;
using System.Web.Security;

namespace Aertigo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult MainLogin()
        {
            if (Session["email"] == null && Session["password"] == null)
            {
                if (TempData["cardNumber"] != null)
                {
                    CardModelManager manager = new CardModelManager();
                    manager.RemoveCard(TempData["cardNumber"].ToString());
                }
                HosterViewModel loginHotel = new HosterViewModel();
                ViewBag.hotelLogin = loginHotel;

                return View("MainLogin");
            }
            else
            {
                return RedirectToAction("Home", "Home", new { area = "User" });
            }

        }

        public ActionResult ValidateUser(string emailID, string password)
        {
            if (ModelState.IsValid)
            {
                Models.UserModelManager manager = new Models.UserModelManager();
                int count = manager.AuthenticateUser(emailID, password);
                if (count >= 1)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, emailID, DateTime.Now, DateTime.Now.AddMinutes(30), false, "User", FormsAuthentication.FormsCookiePath);
                    string enTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));
                    Session["email"] = emailID;
                    Session["password"] = password;
                    return RedirectToAction("Home", "Home", new { area = "User" });
                }
                else
                {
                    TempData["userNotFound"] = true;
                }
            }
            return View("MainLogin");
        }

        public ActionResult ValidateHoster(int? hotelID, string password)
        {
            if (ModelState.IsValid)
            {
                HosterModelManager manager = new HosterModelManager();
                int count = manager.ValidateHoster(hotelID, password);
                if (count >= 1)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, hotelID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), false, "Hoster", FormsAuthentication.FormsCookiePath);
                    string enTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));
                    Session["hotelID"] = hotelID;
                    Session["hosterPassword"] = password;
                    return RedirectToAction("Dashboard", "Hoster", new { area = "HotelManagement" });
                }
                else
                {
                    TempData["hosterNotFound"] = true;
                }
            }
            return View("MainLogin");
        }
    
        [AllowAnonymous]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string adminEmail, string adminPassword)
        {
            AdminModelManger manger = new AdminModelManger();
            int count = manger.ValidateAdmin(adminEmail, adminPassword);
            if (count > 0)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, adminEmail, DateTime.Now, DateTime.Now.AddMinutes(30), false, "Admin", FormsAuthentication.FormsCookiePath);
                string enTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));
                Session["adminEmail"] = adminEmail;
                Session["adminPassword"] = adminPassword;
                return RedirectToAction("Dashboard", "Admin", new { area = "Admin" });
            }
            ViewBag.Error = true;
            return View();
        }

        public ActionResult ErrorHandling()
        {
            return View("Error");
        }
    }
}