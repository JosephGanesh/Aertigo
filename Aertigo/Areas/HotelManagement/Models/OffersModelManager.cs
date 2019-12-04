using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.HotelManagement.Models
{
    public class OffersModelManager
    {
        Connection connectionString = new Connection();

        public void AddNewOffer(OffersModel offer)
        {
            string con = connectionString.ConnectionString();

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AddOffers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    SqlParameter offerTitle = new SqlParameter("@offer_title", SqlDbType.VarChar, 40);
                    SqlParameter validFrom = new SqlParameter("@valid_from", SqlDbType.Date);
                    SqlParameter validTo = new SqlParameter("@valid_to", SqlDbType.Date);
                    SqlParameter aboutOffer = new SqlParameter("@about_the_offer", SqlDbType.VarChar);
                    SqlParameter termsAndCondition = new SqlParameter("@terms_and_condition", SqlDbType.VarChar);
                    SqlParameter offerPoster = new SqlParameter("@offer_poster", SqlDbType.VarChar);
                    SqlParameter promoCode = new SqlParameter("@promo_code", SqlDbType.VarChar, 30);
                    SqlParameter discountAmount = new SqlParameter("@discount_amount", SqlDbType.Int);
                    SqlParameter isActive = new SqlParameter("@isActive", SqlDbType.Bit);

                    hotelID.Value = offer.HotelID;
                    offerTitle.Value = offer.OfferTitle;
                    validFrom.Value = Convert.ToDateTime(offer.ValidFrom).Date;
                    validTo.Value = Convert.ToDateTime(offer.ValidTo).Date;
                    aboutOffer.Value = offer.AboutOffer;
                    termsAndCondition.Value = offer.TermsAndCondition;
                    offerPoster.Value = offer.OfferPoster;
                    promoCode.Value = offer.PromoCode;
                    discountAmount.Value = offer.DiscountAmount;
                    isActive.Value = false;

                    command.Parameters.Add(hotelID);
                    command.Parameters.Add(offerTitle);
                    command.Parameters.Add(validFrom);
                    command.Parameters.Add(validTo);
                    command.Parameters.Add(aboutOffer);
                    command.Parameters.Add(termsAndCondition);
                    command.Parameters.Add(offerPoster);
                    command.Parameters.Add(promoCode);
                    command.Parameters.Add(discountAmount);
                    command.Parameters.Add(isActive);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        public int CheckActiveOffer(int? hosterHotelID)
        {
            int count = 0;
            string con = connectionString.ConnectionString();

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("CheckActiveOfferOnHotelID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    hotelID.Value = hosterHotelID;

                    command.Parameters.Add(hotelID);

                    try
                    {
                        connection.Open();
                        count = (int) command.ExecuteScalar();
                        return count;
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}