using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Aertigo.Areas.HotelManagement.Models;
using System.Web.UI.WebControls;

namespace Aertigo.Areas.Admin.Models
{
    public class AdminModuleManager
    {
        Connection connectionString = new Connection();

        public int GetTodayBookedCount()
        {
            string con = connectionString.ConnectionString();
            int count = 0;
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllBookingCountToday", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        count = (int)command.ExecuteScalar();
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

        public int GetTotalCountOnGivenDate(DateTime fromDateSpecified, DateTime toDateSpecified)
        {
            string con = connectionString.ConnectionString();
            int count = 0;
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetTotalBookingsWithoutHotelID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter fromDate = new SqlParameter("@fromDate", SqlDbType.Date);
                    SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);

                    fromDate.Value = Convert.ToDateTime(fromDateSpecified).Date;
                    toDate.Value = Convert.ToDateTime(toDateSpecified).Date;

                    command.Parameters.Add(fromDate);
                    command.Parameters.Add(toDate);

                    try
                    {
                        connection.Open();
                        count = (int)command.ExecuteScalar();
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

        public List<BookedDetailsModel> GetAllBookedDetails()
        {
            List<BookedDetailsModel> bookedDetails = new List<BookedDetailsModel>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllBookedDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                BookedDetailsModel detail = new BookedDetailsModel();
                                detail.HotelName = dr["hotel_name"].ToString();
                                detail.EmailID = dr["emailID"].ToString();
                                detail.NumberOfRooms = dr["number_of_rooms"].ToString();
                                detail.TotalCost = dr["total_cost"].ToString();
                                detail.ModeOfPayment = dr["mode_of_payment"].ToString();
                                bookedDetails.Add(detail);
                            }
                            dr.Close();
                            return bookedDetails;
                        }
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

        public List<PartnersViewModel> GetHotelsDetails()
        {
            List<PartnersViewModel> hotelsDetails = new List<PartnersViewModel>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllTheHotelsDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                PartnersViewModel hotel = new PartnersViewModel();
                                hotel.HotelID = Convert.ToInt32(dr["hotelID"]);
                                hotel.HotelAddres = dr["hotel_address"].ToString();
                                hotel.HotelName = dr["hotel_name"].ToString();
                                hotel.Status = Convert.ToBoolean(dr["isActive"]);
                                hotelsDetails.Add(hotel);
                            }
                            dr.Close();
                            return hotelsDetails;
                        }
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

        public List<OfferDetailsModel> GetAllOffersDetails()
        {
            List<OfferDetailsModel> offerDetails = new List<OfferDetailsModel>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllOffersDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                OfferDetailsModel offer = new OfferDetailsModel();
                                offer.OfferID = Convert.ToInt32(dr["offerID"]);
                                offer.HotelID = Convert.ToInt32(dr["hotelID"]);
                                offer.HotelName = dr["hotel_name"].ToString();
                                offer.OfferTitle = dr["offer_title"].ToString();
                                offer.ValidFrom = Convert.ToDateTime(dr["valid_from"]).Date.ToShortDateString();
                                offer.ValidTo = Convert.ToDateTime(dr["valid_to"]).Date.ToShortDateString();
                                offerDetails.Add(offer);
                            }
                            dr.Close();
                            return offerDetails;
                        }
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

        public List<PartnersViewModel> GetInActiveHotelsDetails()
        {
            List<PartnersViewModel> hotelsDetails = new List<PartnersViewModel>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetInActiveHotelsDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                PartnersViewModel hotel = new PartnersViewModel();
                                hotel.HotelID = Convert.ToInt32(dr["hotelID"]);
                                hotel.HotelAddres = dr["hotel_address"].ToString();
                                hotel.HotelName = dr["hotel_name"].ToString();
                                hotelsDetails.Add(hotel);
                            }
                            dr.Close();
                            return hotelsDetails;
                        }
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

        public List<UsersModel> GetUsersDetails()
        {
            List<UsersModel> usersDetails = new List<UsersModel>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllUsersDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                UsersModel user = new UsersModel();
                                user.UserID = Convert.ToInt32(dr["userID"]);
                                user.EmailID = dr["emailID"].ToString();
                                user.TotalBookings = dr["Total Bookings"].ToString();
                                user.Status = Convert.ToBoolean(dr["isActive"]);
                                usersDetails.Add(user);
                            }
                            dr.Close();
                            return usersDetails;
                        }
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

        public void DeleteUserComment(int? userCommentID)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("DeleteUserComment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter commentID = new SqlParameter("@commentID", SqlDbType.Int);
                    commentID.Value = userCommentID;

                    command.Parameters.Add(commentID);
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

        public void DeactivateHotel(int? hosterHotelID)
        {
            Connection connectionString = new Connection();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("DeactivateHotel", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelid = new SqlParameter("@hotelId", SqlDbType.Int);
                    hotelid.Value = hosterHotelID;

                    command.Parameters.Add(hotelid);

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

        public OffersModel GetOfferDetails(int? offerHotelID)
        {
            OffersModel offer = new OffersModel();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetOfferDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    hotelID.Value = offerHotelID;
                    command.Parameters.Add(hotelID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                offer.OfferID = Convert.ToInt32(dr["offerID"]);
                                offer.HotelID = Convert.ToInt32(dr["hotelID"]);
                                offer.OfferTitle = dr["offer_title"].ToString();
                                offer.ValidFrom = Convert.ToDateTime(dr["valid_from"]).ToShortDateString();
                                offer.ValidTo = Convert.ToDateTime(dr["valid_to"]).ToShortDateString();
                                offer.AboutOffer = dr["about_the_offer"].ToString();
                                offer.TermsAndCondition = dr["terms_and_condition"].ToString();
                                offer.OfferPoster = dr["offer_poster"].ToString();
                                offer.PromoCode = dr["promo_code"].ToString();
                                offer.DiscountAmount = Convert.ToInt32(dr["discount_amount"]);
                                offer.isActive = Convert.ToBoolean(dr["isActive"]);
                            }
                            dr.Close();
                            return offer;
                        }
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

        public void ApproveOffer(OffersModel offer)
        {
            Connection connectionString = new Connection();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("ApproveOffer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter offerID = new SqlParameter("@offerID", SqlDbType.Int);
                    SqlParameter offerTitle = new SqlParameter("@offer_title", SqlDbType.VarChar, 40);
                    SqlParameter validFrom = new SqlParameter("@valid_from", SqlDbType.Date);
                    SqlParameter validTo = new SqlParameter("@valid_to", SqlDbType.Date);
                    SqlParameter aboutTheOffer = new SqlParameter("@about_the_offer", SqlDbType.VarChar);
                    SqlParameter termsAndCondition = new SqlParameter("@terms_and_condition", SqlDbType.VarChar);
                    SqlParameter offerPoster = new SqlParameter("@offer_poster", SqlDbType.VarChar);
                    SqlParameter promoCode = new SqlParameter("@promo_code", SqlDbType.VarChar, 30);
                    SqlParameter discountAmount = new SqlParameter("@discount_amount", SqlDbType.Int);
                    SqlParameter isActive = new SqlParameter("@isActive", SqlDbType.Bit);

                    offerID.Value = offer.OfferID;
                    offerTitle.Value = offer.OfferTitle;
                    validFrom.Value = offer.ValidFrom;
                    validTo.Value = offer.ValidTo;
                    aboutTheOffer.Value = offer.AboutOffer;
                    termsAndCondition.Value = offer.TermsAndCondition;
                    offerPoster.Value = offer.OfferPoster;
                    promoCode.Value = offer.PromoCode;
                    discountAmount.Value = offer.DiscountAmount;
                    isActive.Value = true;

                    command.Parameters.Add(offerID);
                    command.Parameters.Add(offerTitle);
                    command.Parameters.Add(validFrom);
                    command.Parameters.Add(validTo);
                    command.Parameters.Add(aboutTheOffer);
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

        public void DeleteOffer(int? offerIDSpecified)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("DeactivateHotel", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter offerID = new SqlParameter("@offerID", SqlDbType.Int);
                    offerID.Value = offerIDSpecified;

                    command.Parameters.Add(offerID);

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

        public void DeleteHotel(int? hotelIDSpecified)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("DeleteHotelRequest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    hotelID.Value = hotelIDSpecified;

                    command.Parameters.Add(hotelID);

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

        public void ActivateUser(int? userIDSpecified)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("ActivateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter userID = new SqlParameter("@userID", SqlDbType.Int);
                    userID.Value = userIDSpecified;

                    command.Parameters.Add(userID);

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

        public void DeActivateUser(int? userIDSpecified)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("DeActivateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter userID = new SqlParameter("@userID", SqlDbType.Int);
                    userID.Value = userIDSpecified;

                    command.Parameters.Add(userID);

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

        public List<FeedbackForAdminModel> GetAllFeedbackDetails()
        {
            List<FeedbackForAdminModel> feedbacks = new List<FeedbackForAdminModel>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllFeedbacks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                FeedbackForAdminModel feedback = new FeedbackForAdminModel();
                                feedback.FeedbackID = Convert.ToInt32(dr["feedbackID"]);
                                feedback.UserImage = dr["user_image"].ToString();
                                feedback.EmailID = dr["emailID"].ToString();
                                feedback.Feedback = dr["feedback"].ToString();
                                feedbacks.Add(feedback);
                            }
                            dr.Close();
                            return feedbacks;
                        }
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