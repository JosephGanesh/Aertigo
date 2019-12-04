using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aertigo.Areas.User.Models
{
    public class CardModel
    {
        public int? CardID { get; set; }
        [Required(ErrorMessage = "Enter Card Holder Name")]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = "Only Alphabets are allowded!")]
        public string CardHolderName { get; set; }
        [Required(ErrorMessage = "Enter Card Number")]
        [RegularExpression(@"\d{16}", ErrorMessage = "Not Valid Card Number")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Select Card Type")]
        public string CardType { get; set; }
        [Required(ErrorMessage = "Enter Card Number")]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = "Only Alphabets are allowded!")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "Select Card Merchant")]
        public string CardMerchant { get; set; }
        [Required(ErrorMessage = "Enter Card CVV")]
        [RegularExpression(@"\d+", ErrorMessage = "CVV Must Be A Number!")]
        public int? CardCVV { get; set; }
        [Required(ErrorMessage = "Enter Card Password")]
        [Compare("RePassword", ErrorMessage = "Password Are Not Matching")]
        public string CardPassword { get; set; }
        [Required(ErrorMessage = "Re-Enter The Password")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "Select Card Valid From Date")]
        public string CardValidFrom { get; set; }
        [Required(ErrorMessage = "Select Card Valid From To")]
        public string CardValidTo { get; set; }
    }
}