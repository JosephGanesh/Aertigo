using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aertigo.Areas.HotelManagement.Models
{
    public class OffersModel
    {
        public int? OfferID { get; set; }
        public int? HotelID { get; set; }
        [Required(ErrorMessage = "Please Provide Offer Title")]
        public string OfferTitle { get; set; }
        [Required(ErrorMessage = "Please Provide Valid From Date")]
        public string ValidFrom { get; set; }
        [Required(ErrorMessage = "Please Provide Valid To Date")]
        public string ValidTo { get; set; }
        [Required(ErrorMessage = "Please Enter About The Offer")]
        public string AboutOffer { get; set; }
        [Required(ErrorMessage = "Please Enter Terms And Condition")]
        public string TermsAndCondition { get; set; }
        [Required(ErrorMessage = "Please Provide Offer Poster")]
        public string OfferPoster { get; set; }
        [Required(ErrorMessage = "Please Provide Promo Code")]
        public string PromoCode { get; set; }
        [Required(ErrorMessage = "Please Provide Discount Amount")]
        public int? DiscountAmount { get; set; }
        public Boolean isActive { get; set; }
    }
}