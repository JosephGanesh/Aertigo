using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aertigo.Areas.HotelManagement.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Principal;
using System.Web.Security;

namespace Aertigo.Areas.HotelManagement.Models
{

    public class HotelModelManager
    {
        Connection connectionString = new Connection();

        private DataTable GetFacilitiesIDs(List<FacilityModel> facilities)
        {
            DataTable table = new DataTable();
            table.Columns.Add("FacilitiesID", typeof(int));

            foreach (FacilityModel facility in facilities)
            {
                DataRow row = table.NewRow();
                row["FacilitiesID"] = facility.FacilityID;
                table.Rows.Add(row);
            }
            return table;
        }

        public void AddNewHotel(HotelFacilityViewModel model)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AddNewHotel", connection)) 
                {
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = GetFacilitiesIDs(model.Facilities);

                    SqlParameter hotelCertificate = new SqlParameter("@hotel_certificate", SqlDbType.VarChar);
                    SqlParameter hotelName = new SqlParameter("@hotel_name", SqlDbType.VarChar, 50);
                    SqlParameter cityID = new SqlParameter("@cityID", SqlDbType.Int);
                    SqlParameter hotelStartedDate = new SqlParameter("@hotel_started_date", SqlDbType.Date);
                    SqlParameter hotelAddress = new SqlParameter("@hotel_address", SqlDbType.VarChar);
                    SqlParameter aboutHotel = new SqlParameter("@about_the_hotel", SqlDbType.VarChar);
                    SqlParameter priceFor1BHK = new SqlParameter("@price_for_1bhk", SqlDbType.Int);
                    SqlParameter priceFor2BHK = new SqlParameter("@price_for_2bkh", SqlDbType.Int);
                    SqlParameter hotelGoogleAPI = new SqlParameter("@hotel_googleMapApi", SqlDbType.VarChar);
                    SqlParameter hotelEmailID = new SqlParameter("@hotel_emailID", SqlDbType.VarChar, 50);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 40);
                    SqlParameter contactNumber = new SqlParameter("@contact_number", SqlDbType.VarChar, 15);
                    SqlParameter hotelFacebook = new SqlParameter("@hotel_facebook_address", SqlDbType.VarChar);
                    SqlParameter hotelInstagram = new SqlParameter("@hotel_instagram_address", SqlDbType.VarChar);
                    SqlParameter hotelImage1 = new SqlParameter("@hotel_image1", SqlDbType.VarChar);
                    SqlParameter hotelImage2 = new SqlParameter("@hotel_image2", SqlDbType.VarChar);
                    SqlParameter hotelImage3 = new SqlParameter("@hotel_image3", SqlDbType.VarChar);
                    SqlParameter hotelImage4 = new SqlParameter("@hotel_image4", SqlDbType.VarChar);
                    SqlParameter isActive = new SqlParameter("@isActive", SqlDbType.Bit);
                    SqlParameter facilitesIDs = new SqlParameter("@facilitiesIDs", SqlDbType.Structured, model.Facilities.Count);

                    facilitesIDs.Value = table;
                    hotelCertificate.Value = model.Hotel.HotelCertificate;
                    hotelName.Value = model.Hotel.HotelName;
                    cityID.Value = Convert.ToInt32(model.Hotel.CityID);
                    hotelStartedDate.Value = Convert.ToDateTime(model.Hotel.HotelStartedDate).Date;
                    hotelAddress.Value = model.Hotel.Address;
                    aboutHotel.Value = model.Hotel.AboutHotel;
                    priceFor1BHK.Value = Convert.ToInt32(model.Hotel.PriceFor1BHK);
                    priceFor2BHK.Value = Convert.ToInt32(model.Hotel.PriceFor2BHK);
                    hotelGoogleAPI.Value = model.Hotel.GoogleAPI;
                    hotelEmailID.Value = model.Hotel.EmailID;
                    password.Value = model.Hotel.Password;
                    contactNumber.Value = model.Hotel.ContactNumber;
                    hotelFacebook.Value = model.Hotel.FacebookAddress;
                    hotelInstagram.Value = model.Hotel.InstagramAddress;
                    hotelImage1.Value = model.Hotel.Image1;
                    hotelImage2.Value = model.Hotel.image2;
                    hotelImage3.Value = model.Hotel.Image3;
                    hotelImage4.Value = model.Hotel.image4;
                    isActive.Value = false;

                    command.Parameters.Add(facilitesIDs);
                    command.Parameters.Add(hotelCertificate);
                    command.Parameters.Add(hotelName);
                    command.Parameters.Add(cityID);
                    command.Parameters.Add(hotelStartedDate);
                    command.Parameters.Add(hotelAddress);
                    command.Parameters.Add(aboutHotel);
                    command.Parameters.Add(priceFor1BHK);
                    command.Parameters.Add(priceFor2BHK);
                    command.Parameters.Add(hotelGoogleAPI);
                    command.Parameters.Add(hotelEmailID);
                    command.Parameters.Add(password);
                    command.Parameters.Add(contactNumber);
                    command.Parameters.Add(hotelFacebook);
                    command.Parameters.Add(hotelInstagram);
                    command.Parameters.Add(hotelImage1);
                    command.Parameters.Add(hotelImage2);
                    command.Parameters.Add(hotelImage3);
                    command.Parameters.Add(hotelImage4);
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

        public List<HotelModel> GetAllHotels()
        {
            string con = connectionString.ConnectionString();
            List<HotelModel> hotels = new List<HotelModel>();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllHotels", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                HotelModel hotel = new HotelModel();
                                hotel.HotelID = Convert.ToInt32(dr["hotelID"]);
                                hotel.HotelName = dr["hotel_name"].ToString();
                                hotel.Image1 = dr["hotel_image1"].ToString();
                                hotel.Address = dr["hotel_address"].ToString();
                                hotel.PriceFor1BHK = Convert.ToInt32(dr["price_for_1bhk"]);
                                hotels.Add(hotel);
                            }
                            dr.Close();
                            return hotels;
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

        public int GetBookingsTotalCount(int? hosterHotelID, DateTime? fromDateSpecified, DateTime? toDateSpecified)
        {
            int count = 0;
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetTotalBookings", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    SqlParameter fromDate = new SqlParameter("@fromDate", SqlDbType.Date);
                    SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);

                    hotelID.Value = hosterHotelID;
                    fromDate.Value = Convert.ToDateTime(fromDateSpecified).Date;
                    toDate.Value = Convert.ToDateTime(toDateSpecified).Date;

                    command.Parameters.Add(hotelID);
                    command.Parameters.Add(fromDate);
                    command.Parameters.Add(toDate);

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

        public List<BookedUsersDetailsModel> GetBookedUsersDetails(int? hosterHotelID)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetBookedUserDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    hotelID.Value = hosterHotelID;

                    command.Parameters.Add(hotelID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            List<BookedUsersDetailsModel> lstBookedDetails = new List<BookedUsersDetailsModel>();
                            while (dr.Read())
                            {
                                BookedUsersDetailsModel details = new BookedUsersDetailsModel();
                                details.BookingID = Convert.ToInt32(dr["bookingID"]);
                                details.Username = dr["first_name"].ToString();
                                details.PhoneNumber = dr["phone_number"].ToString();
                                details.ExpectedArrivingTime = dr["expected_arriving_time"].ToString();
                                details.NumberOfGuests = dr["number_of_guests"].ToString();
                                details.NumberOfRooms = Convert.ToInt32(dr["number_of_rooms"]);
                                details.ModeOfPayment = dr["mode_of_payment"].ToString();
                                lstBookedDetails.Add(details);
                            }
                            return lstBookedDetails;
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

        public List<CommentsHosterViewModel> GetUsersComments(int? hosterHotelID)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetUserCommentsBasedOnHotelID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    hotelID.Value = hosterHotelID;

                    command.Parameters.Add(hotelID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            List<CommentsHosterViewModel> lstUsersComments = new List<CommentsHosterViewModel>();
                            while (dr.Read())
                            {
                                CommentsHosterViewModel comment = new CommentsHosterViewModel();
                                comment.CommentID = Convert.ToInt32(dr["commentID"]);
                                comment.EmailID = " "+ dr["emailID"].ToString();
                                comment.Comment = dr["comment"].ToString();
                                comment.Rating = dr["rating"].ToString();
                                comment.UserImage = dr["user_image"].ToString();
                                lstUsersComments.Add(comment);
                            }
                            return lstUsersComments;
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

        public HotelModel GetHotelDetails(int? hosterHotelID)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetHotelDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    hotelID.Value = hosterHotelID;

                    command.Parameters.Add(hotelID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            HotelModel hotel = new HotelModel();
                            while (dr.Read())
                            {
                                hotel.HotelID = Convert.ToInt32(dr["hotelID"]);
                                hotel.HotelName = dr["hotel_name"].ToString();
                                hotel.CityID = Convert.ToInt32(dr["cityID"]);
                                hotel.StateID = Convert.ToInt32(dr["stateID"]);
                                hotel.HotelStartedDate = Convert.ToDateTime(dr["hotel_started_date"]).ToShortDateString();
                                hotel.HotelCertificate = dr["hotel_certificate"].ToString();
                                hotel.Address = dr["hotel_address"].ToString();
                                hotel.AboutHotel = dr["about_the_hotel"].ToString();
                                hotel.PriceFor1BHK = Convert.ToInt32(dr["price_for_1bhk"]);
                                hotel.PriceFor2BHK = Convert.ToInt32(dr["price_for_2bkh"]);
                                hotel.GoogleAPI = dr["hotel_googleMapApi"].ToString();
                                hotel.EmailID = dr["hotel_emailID"].ToString();
                                hotel.Password = dr["password"].ToString();
                                hotel.ContactNumber = dr["contact_number"].ToString();
                                hotel.FacebookAddress = dr["hotel_facebook_address"].ToString();
                                hotel.InstagramAddress = dr["hotel_instagram_address"].ToString();
                                hotel.Image1 = dr["hotel_image1"].ToString();
                                hotel.image2 = dr["hotel_image2"].ToString();
                                hotel.Image3 = dr["hotel_image3"].ToString();
                                hotel.image4 = dr["hotel_image4"].ToString();
                                hotel.isActive = Convert.ToBoolean(dr["isActive"]);
                            }
                            return hotel;
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

        public void UpdateHotelDetails(HotelFacilityViewModel merge, bool isAdmin)
        { 
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("UpdateHotelDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    SqlParameter hotelName = new SqlParameter("@hotel_name", SqlDbType.VarChar, 50);
                    SqlParameter cityID = new SqlParameter("@cityID", SqlDbType.Int);
                    SqlParameter hotelStartedDate = new SqlParameter("@hotel_started_date", SqlDbType.Date);
                    SqlParameter hotelAddress = new SqlParameter("@hotel_address", SqlDbType.VarChar);
                    SqlParameter aboutHotel = new SqlParameter("@about_the_hotel", SqlDbType.VarChar);
                    SqlParameter priceFor1BHK = new SqlParameter("@price_for_1bhk", SqlDbType.Int);
                    SqlParameter priceFor2BHK = new SqlParameter("@price_for_2bkh", SqlDbType.Int);
                    SqlParameter hotelGoogleAPI = new SqlParameter("@hotel_googleMapApi", SqlDbType.VarChar);
                    SqlParameter hotelEmailID = new SqlParameter("@hotel_emailID", SqlDbType.VarChar, 50);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 40);
                    SqlParameter contactNumber = new SqlParameter("@contact_number", SqlDbType.VarChar, 15);
                    SqlParameter hotelFacebook = new SqlParameter("@hotel_facebook_address", SqlDbType.VarChar);
                    SqlParameter hotelInstagram = new SqlParameter("@hotel_instagram_address", SqlDbType.VarChar);
                    SqlParameter hotelImage1 = new SqlParameter("@hotel_image1", SqlDbType.VarChar);
                    SqlParameter hotelImage2 = new SqlParameter("@hotel_image2", SqlDbType.VarChar);
                    SqlParameter hotelImage3 = new SqlParameter("@hotel_image3", SqlDbType.VarChar);
                    SqlParameter hotelImage4 = new SqlParameter("@hotel_image4", SqlDbType.VarChar);
                    SqlParameter isActive = new SqlParameter("@isActive", SqlDbType.Bit);
                    SqlParameter facilitesIDs = new SqlParameter("@facilitiesIDs", SqlDbType.Structured, merge.Facilities.Count);

                    DataTable table = GetFacilitiesIDs(merge.Facilities);
                    facilitesIDs.Value = table;
                    hotelID.Value = merge.Hotel.HotelID;
                    hotelName.Value = merge.Hotel.HotelName;
                    cityID.Value = Convert.ToInt32(merge.Hotel.CityID);
                    hotelStartedDate.Value = Convert.ToDateTime(merge.Hotel.HotelStartedDate).Date;
                    hotelAddress.Value = merge.Hotel.Address;
                    aboutHotel.Value = merge.Hotel.AboutHotel;
                    priceFor1BHK.Value = Convert.ToInt32(merge.Hotel.PriceFor1BHK);
                    priceFor2BHK.Value = Convert.ToInt32(merge.Hotel.PriceFor2BHK);
                    hotelGoogleAPI.Value = merge.Hotel.GoogleAPI;
                    hotelEmailID.Value = merge.Hotel.EmailID;
                    password.Value = merge.Hotel.Password;
                    contactNumber.Value = merge.Hotel.ContactNumber;
                    hotelFacebook.Value = merge.Hotel.FacebookAddress;
                    hotelInstagram.Value = merge.Hotel.InstagramAddress;
                    hotelImage1.Value = merge.Hotel.Image1;
                    hotelImage2.Value = merge.Hotel.image2;
                    hotelImage3.Value = merge.Hotel.Image3;
                    hotelImage4.Value = merge.Hotel.image4;
                    isActive.Value = true;

                    command.Parameters.Add(facilitesIDs);
                    command.Parameters.Add(hotelID);
                    command.Parameters.Add(hotelName);
                    command.Parameters.Add(cityID);
                    command.Parameters.Add(hotelStartedDate);
                    command.Parameters.Add(hotelAddress);
                    command.Parameters.Add(aboutHotel);
                    command.Parameters.Add(priceFor1BHK);
                    command.Parameters.Add(priceFor2BHK);
                    command.Parameters.Add(hotelGoogleAPI);
                    command.Parameters.Add(hotelEmailID);
                    command.Parameters.Add(password);
                    command.Parameters.Add(contactNumber);
                    command.Parameters.Add(hotelFacebook);
                    command.Parameters.Add(hotelInstagram);
                    command.Parameters.Add(hotelImage1);
                    command.Parameters.Add(hotelImage2);
                    command.Parameters.Add(hotelImage3);
                    command.Parameters.Add(hotelImage4);
                    command.Parameters.Add(isActive);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        if (isAdmin)
                        {
                            MailMessage res_msg = new MailMessage();

                            res_msg.From = new MailAddress("joesphganesh508@gmail.com");

                            res_msg.To.Add(merge.Hotel.EmailID);

                            res_msg.Subject = "congratulations!";
                            res_msg.Body = "Your Hotel Hosted On Aertigo Successfully! Your Hotel ID Is: " + merge.Hotel.HotelID + " Use This ID To Login On Hoster Side, for more details contact us: 9611666579!";
                            res_msg.IsBodyHtml = true;
                            res_msg.Priority = MailPriority.High;

                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                            smtp.Port = 587;
                            smtp.Credentials = new System.Net.NetworkCredential("joesphganesh508@gmail.com", "9741902201");
                            smtp.EnableSsl = true;
                            smtp.Send(res_msg);

                            //https://www.google.com/settings/security/lesssecureapps
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

        public void DeactivateHotel(int? hosterHotelID)
        {
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

        public List<HotelModel> GetCustomHotelsDetails(string hotelNameSpecified, string cityNameSpecified, int? minPriceSpecified, int? maxPriceSpecified)
        {
            string con = connectionString.ConnectionString();
            List<HotelModel> hotels = new List<HotelModel>();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetCustomHotelsDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelName = new SqlParameter("@hotelName", SqlDbType.VarChar, 40);
                    SqlParameter cityName = new SqlParameter("@cityName", SqlDbType.VarChar, 40);
                    SqlParameter minPrice = new SqlParameter("@minPrice", SqlDbType.Int);
                    SqlParameter maxPrice = new SqlParameter("@maxPrice", SqlDbType.Int);

                    hotelName.Value = (hotelNameSpecified == "") ? (object)DBNull.Value : hotelNameSpecified;
                    cityName.Value = (cityNameSpecified == "") ? (object)DBNull.Value : cityNameSpecified;
                    minPrice.Value = (minPriceSpecified == null) ? (object)DBNull.Value : minPriceSpecified;
                    maxPrice.Value = (maxPriceSpecified == null) ? (object)DBNull.Value : maxPriceSpecified;

                    command.Parameters.Add(hotelName);
                    command.Parameters.Add(cityName);
                    command.Parameters.Add(minPrice);
                    command.Parameters.Add(maxPrice);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                HotelModel hotel = new HotelModel();
                                hotel.HotelID = Convert.ToInt32(dr["hotelID"]);
                                hotel.HotelName = dr["hotel_name"].ToString();
                                hotel.Image1 = dr["hotel_image1"].ToString();
                                hotel.Address = dr["hotel_address"].ToString();
                                hotel.PriceFor1BHK = Convert.ToInt32(dr["price_for_1bhk"]);
                                hotels.Add(hotel);
                            }
                            dr.Close();
                            return hotels;
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