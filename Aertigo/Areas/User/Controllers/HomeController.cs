using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aertigo.Areas.User.Models;
using Aertigo.Areas.HotelManagement.Models;

namespace Aertigo.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        // GET: User/Home
        [Authorize(Roles = "User")]
        public ActionResult Home()
        {
            if (Session["email"] != null && Session["password"] != null)
            {
                CarousalModuleManager manager = new CarousalModuleManager();
                HotelModelManager hotelManger = new HotelModelManager();
                ViewBag.carousals = manager.GetCarousals();
                List<HotelModel> hotel = new List<HotelModel>();
                if (TempData["customResult"] != null)
                {
                    ModelState.Clear();
                    hotel = TempData["customResult"] as List<HotelModel>;
                    return View("HomePage", hotel);

                }
                else
                {
                    hotel = hotelManger.GetAllHotels();
                    return View("HomePage", hotel);
                }
            }
            else
            {
                return RedirectToAction("MainLogin", "Login", new { area = "" });
            }
        }

        [Authorize(Roles = "User")]
        public ActionResult History()
        {
            if (Session["email"] != null && Session["password"] != null)
            {
                UserCommentsModel comment = new UserCommentsModel();
                UserCommentsManager userComments = new UserCommentsManager();
                BookingRoomModelManager manager = new BookingRoomModelManager();
                comment.BookingDetails = manager.GetBookRoomDetails(Session["email"].ToString(), Session["password"].ToString());
                return View("History", comment);
            }
            else
            {
                return RedirectToAction("MainLogin", "Login", new { area = "" });
            }
        }

        public ActionResult AddComment(int? hotelID, string emailID, string password, int? rating, string comment)
        {
            UserCommentsManager commentsManager = new UserCommentsManager();
            commentsManager.AddUserComment(hotelID, emailID, password, rating, comment);
            UserCommentsModel comments = new UserCommentsModel();
            BookingRoomModelManager manager = new BookingRoomModelManager();
            comments.BookingDetails = manager.GetBookRoomDetails(Session["email"].ToString(), Session["password"].ToString());
            return Json(comments);
        }

        [Authorize(Roles = "User")]
        public ActionResult CancelRoom(int? bookingID)
        {
            BookingRoomModelManager manager = new BookingRoomModelManager();
            manager.CancelRoomBooking(bookingID);
            return RedirectToAction("History");
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult Settings()
        {
            if (Session["email"] != null && Session["password"] != null)
            {
                UserModel user = new UserModel();
                UserModelManager manager = new UserModelManager();
                ViewBag.States = manager.GetAllStates();
                user = manager.GetUserDetails(Session["email"].ToString());
                CardModel card = new CardModel();
                CardModelManager cardManager = new CardModelManager();
                if (user.CardID != null)
                {
                    card = cardManager.FindCardByCardID(user.CardID);
                    ViewBag.UserCard = card;
                    ViewBag.CardView = "_UserCardDetailsRegistered";
                }
                else
                {
                    ViewBag.UserCard = card;
                    ViewBag.CardView = "_UserCardDetails";
                }
                return View("UserSettings", user);
            }
            else
            {
                return RedirectToAction("MainLogin", "Login", new { area = "" });
            }
        }

        private bool ValidateUserDetails(UserModel user)
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
        public ActionResult Settings(UserModel user, HttpPostedFileBase file)
        {
            UserModelManager manager = new UserModelManager();
            CardModelManager cardManager = new CardModelManager();
            CardModel card = new CardModel();
            if (TempData["cardNumber"] != null)
            {
                user.CardID = cardManager.GetCardIdByCardNumber(TempData["cardNumber"].ToString());
            }
            user.isActive = true;
            string image = string.Empty;
            if (file != null)
            {
                // Saving Image In Local Folder
                image = Path.GetFileName(file.FileName);
                string path = Server.MapPath(string.Format("~/Areas/User/UserImages/{0}", image));
                file.SaveAs(path);
                user.UserImage = image;
            }
            if (ValidateUserDetails(user))
            {
                manager.UpdateUserDetails(user);
                user = manager.GetUserDetails(Session["email"].ToString());
            }
            ViewBag.States = manager.GetAllStates();
            if (user.CardID != null)
            {
                card = cardManager.FindCardByCardID(user.CardID);
                ViewBag.UserCard = card;
                ViewBag.CardView = "_UserCardDetailsRegistered";
            }
            else
            {
                ViewBag.UserCard = card;
                ViewBag.CardView = "_UserCardDetails";
            }
            return View("UserSettings", user);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("MainLogin", "Login", new { area = "" });
        }

        public ActionResult UpdateCardsDetails(CardModel card)
        {
            CardModelManager manager = new CardModelManager();
            manager.UpdateCardDetails(card);
            return Json("_UserCardDetailsRegistered");
        }

        [Authorize(Roles = "User")]
        public ActionResult Booking(int? id)
        {
            if (Session["email"] != null && Session["password"] != null && id != null)
            {
                BookingRoomModelManager manager = new BookingRoomModelManager();
                BookRoomModel bookRoom = new BookRoomModel();
                bookRoom.HotelID = id;
                bookRoom = manager.GetDetailsForBookRoom(id, Session["email"].ToString(), Session["password"].ToString());
                ViewBag.Error = false;
                return View(bookRoom);
            }
            else
            {
                return RedirectToAction("MainLogin", "Login", new { area = "" });
            }
        }

        private bool validateBookingRoomDetails(BookRoomModel bookRoom)
        {
            if (bookRoom.ExpectedTime != null && bookRoom.CheckInDate != null && bookRoom.CheckOutDate != null
                && bookRoom.NumberOfRooms != null && bookRoom.TotalCost != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        public ActionResult Booking(BookRoomModel bookRoom)
        {
            BookingRoomModelManager manager = new BookingRoomModelManager();
            if (validateBookingRoomDetails(bookRoom))
            {
                bookRoom.HotelID = Convert.ToInt32(bookRoom.HotelID);
                int bookingID = manager.BookRoom(bookRoom, Session["email"].ToString(), Session["password"].ToString());
                if (bookRoom.Guests != null)
                {
                    manager.AddGuests(bookRoom.Guests, bookingID);
                }
                return RedirectToAction("History");
            }
            bookRoom = manager.GetDetailsForBookRoom(Convert.ToInt32(bookRoom.HotelID), Session["email"].ToString(), Session["password"].ToString());
            ViewBag.Error = true;
            return View(bookRoom);
        }

        public ActionResult GetRoomTypePrice(string roomType)
        {
            string room = (roomType == "1 BHK") ? "price_for_1bhk" : "price_for_2bkh";
            BookingRoomModelManager manager = new BookingRoomModelManager();
            int price = manager.GetRoomPriceBasedOnRoomType(room);
            return Json(price);
        }

        public ActionResult AuthenticateUserCard(string emailID, string password)
        {
            CardModelManager manager = new CardModelManager();
            string cardID = manager.AuthenticateCard(Session["email"].ToString(), Session["password"].ToString());
            return Json(cardID);
        }

        [Authorize(Roles = "User")]
        public ActionResult AddNewCard(CardModel card)
        {
            TempData["cardNumber"] = card.CardNumber;
            ModelState.Clear();
            CardModel cardDetails = new CardModel();
            CardModelManager manager = new CardModelManager();
            manager.AddNewCard(card);
            return PartialView("_UserCardDetails", cardDetails);
        }

        public ActionResult GetDiscountAmount(int? hotelID, string promoCode)
        {
            BookingRoomModelManager manager = new BookingRoomModelManager();
            string amount = manager.GetDiscountAmount(hotelID, promoCode);
            amount = (amount == null) ? "" : amount;
            return Json(amount);
        }

        [Authorize(Roles = "User")]
        public ActionResult ShowHotelDetails(int? hotelID)
        {
            HotelFacilityViewModel merge = new HotelFacilityViewModel();
            CarousalModuleManager carousalManger = new CarousalModuleManager();
            HotelModelManager hotelManager = new HotelModelManager();
            FacilityModelManager facilityManager = new FacilityModelManager();
            HotelModelManager manager = new HotelModelManager();
            merge.Hotel = hotelManager.GetHotelDetails(hotelID);
            ViewBag.facilities = facilityManager.GetFacilityOnHotelID(hotelID);
            ViewBag.Carousal = carousalManger.GetCarousalDetailsOnHotelID(hotelID);
            ViewBag.Comments = manager.GetUsersComments(Convert.ToInt32(hotelID));
            return View("HotelDetails", merge);
        }

        [Authorize(Roles = "User")]
        public ActionResult CustomHotelSearch(string hotelName, string cityName, int? minPrice, int? maxPrice)
        {
            HotelModelManager hotelManager = new HotelModelManager();
            TempData["customResult"] = hotelManager.GetCustomHotelsDetails(hotelName, cityName, minPrice, maxPrice);
            return Json("Home");
        }

        [Authorize(Roles = "User")]
        public ActionResult Contact()
        {
            if (Session["email"] != null && Session["password"] != null)
            {
                FeedBackModel feedBack = new FeedBackModel();
                ViewBag.Success = false;
                return View("Contact", feedBack);
            }
            else
            {
                return RedirectToAction("MainLogin", "Login", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult Contact(FeedBackModel feedBackNew)
        {
            if (ModelState.IsValid)
            {
                FeedbackModelManager feedbackManager = new FeedbackModelManager();
                feedbackManager.AddUserFeedback(feedBackNew, Session["email"].ToString(), Session["password"].ToString());
                FeedBackModel feedBack = new FeedBackModel();
                ViewBag.Success = true;
                ModelState.Clear();
                return View(feedBack);
            }
            ViewBag.Success = false;
            return View(feedBackNew);
        }
    }
}