using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.User.Models
{
    public class BookingRoomModelManager
    {
        Connection connectionString = new Connection();

        public BookRoomModel GetDetailsForBookRoom(int? hotelID, string email, string password)
        {
            BookRoomModel bookRoom = new BookRoomModel();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetHotelUserDetailsForBooking", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelId = new SqlParameter("@hotelID", SqlDbType.Int);
                    SqlParameter userEmailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter userPassword = new SqlParameter("@password", SqlDbType.VarChar, 40);

                    hotelId.Value = hotelID;
                    userEmailID.Value = email;
                    userPassword.Value = password;

                    command.Parameters.Add(hotelId);
                    command.Parameters.Add(userEmailID);
                    command.Parameters.Add(userPassword);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                bookRoom.UserID = Convert.ToInt32(dr["userID"]);
                                bookRoom.Fullname = dr["Fullname"].ToString();
                                bookRoom.UserImage = dr["user_image"].ToString();
                                bookRoom.Address = dr["address"].ToString();
                                bookRoom.MobileNumber = dr["phone_number"].ToString();
                                bookRoom.HotelID = Convert.ToInt32(dr["hotelID"]);
                                bookRoom.HotelName = dr["hotel_name"].ToString();
                                bookRoom.HotelAddress = dr["hotel_address"].ToString();
                                bookRoom.HotelImage1 = dr["hotel_image1"].ToString();
                            }
                            dr.Close();
                            return bookRoom;
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


        public int GetRoomPriceBasedOnRoomType(string userRoomType)
        {
            int price = 0;
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetPriceBasedOnRoomType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter roomType = new SqlParameter("@roomType", SqlDbType.VarChar, 30);

                    roomType.Value = userRoomType;

                    command.Parameters.Add(roomType);

                    try
                    {
                        connection.Open();
                        price = (int)command.ExecuteScalar();
                        return price;
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

        public int BookRoom(BookRoomModel book, string userEmailID, string userPassword)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("BookHotelRoom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter emailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 40);
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    SqlParameter checkIn = new SqlParameter("@check_in", SqlDbType.Date);
                    SqlParameter checkOut = new SqlParameter("@check_out", SqlDbType.Date);
                    SqlParameter numberOfRooms = new SqlParameter("@number_of_rooms", SqlDbType.Int);
                    SqlParameter numberOfGuests = new SqlParameter("@number_of_guests", SqlDbType.Int);
                    SqlParameter modeOfPayment = new SqlParameter("@mode_of_payment", SqlDbType.VarChar, 40);
                    SqlParameter promoCode = new SqlParameter("@promo_code", SqlDbType.VarChar, 30);
                    SqlParameter expectedTime = new SqlParameter("@expected_arriving_time", SqlDbType.Time);
                    SqlParameter totalCost = new SqlParameter("@total_cost", SqlDbType.Int);

                    emailID.Value = userEmailID;
                    password.Value = userPassword;
                    hotelID.Value = Convert.ToInt32(book.HotelID);
                    checkIn.Value = Convert.ToDateTime(book.CheckInDate).Date;
                    checkOut.Value = Convert.ToDateTime(book.CheckOutDate).Date;
                    numberOfRooms.Value = Convert.ToInt32(book.NumberOfRooms);
                    numberOfGuests.Value = Convert.ToInt32(book.NumberOfGuests);
                    modeOfPayment.Value = book.ModeOfPayment;
                    if (book.PromoCode == null)
                    {
                        promoCode.Value = DBNull.Value;
                    }
                    else
                    {
                        promoCode.Value = book.PromoCode;
                    }
                    expectedTime.Value = TimeSpan.Parse(book.ExpectedTime);
                    totalCost.Value = book.TotalCost;

                    command.Parameters.Add(emailID);
                    command.Parameters.Add(password);
                    command.Parameters.Add(hotelID);
                    command.Parameters.Add(checkIn);
                    command.Parameters.Add(checkOut);
                    command.Parameters.Add(numberOfRooms);
                    command.Parameters.Add(numberOfGuests);
                    command.Parameters.Add(modeOfPayment);
                    command.Parameters.Add(promoCode);
                    command.Parameters.Add(expectedTime);
                    command.Parameters.Add(totalCost);

                    try
                    {
                        connection.Open();
                        int bookingID = (int)command.ExecuteScalar();
                        return bookingID;
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

        public void AddGuests(GuestModel guests, int? roomBookingID)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AddGuests", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter guestName = new SqlParameter("@guest_name", SqlDbType.VarChar, 40);
                    SqlParameter relationType = new SqlParameter("@relationship_type", SqlDbType.VarChar, 40);
                    SqlParameter age = new SqlParameter("@age", SqlDbType.Int);
                    SqlParameter bookingID = new SqlParameter("@bookingID", SqlDbType.Int);

                    try
                    {
                        connection.Open();

                        for (int i = 0; i < guests.GuestName.Length; i++)
                        {
                            guestName.Value = guests.GuestName[i];
                            relationType.Value = guests.RelationType[i];
                            age.Value = guests.Age[i];
                            bookingID.Value = roomBookingID;

                            command.Parameters.Add(guestName);
                            command.Parameters.Add(relationType);
                            command.Parameters.Add(age);
                            command.Parameters.Add(bookingID);

                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
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

        public List<BookRoomModel> GetBookRoomDetails(string emailIDSpecified, string passwordSpecified)
        {
            List<BookRoomModel> lstRoomBooked = new List<BookRoomModel>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllRoomBookedDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter emailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 30);

                    emailID.Value = emailIDSpecified;
                    password.Value = passwordSpecified;

                    command.Parameters.Add(emailID);
                    command.Parameters.Add(password);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                BookRoomModel bookRoom = new BookRoomModel();
                                bookRoom.BookingID = Convert.ToInt32(dr["bookingID"]);
                                bookRoom.UserID = Convert.ToInt32(dr["userID"]);
                                bookRoom.HotelID = Convert.ToInt32(dr["hotelID"]);
                                bookRoom.HotelName = dr["hotel_name"].ToString();
                                bookRoom.HotelAddress = dr["hotel_address"].ToString();
                                bookRoom.HotelImage1 = dr["hotel_image"].ToString();
                                bookRoom.CheckInDate = Convert.ToDateTime(dr["check_in"]).ToShortDateString();
                                bookRoom.CheckOutDate = Convert.ToDateTime(dr["check_out"]).ToShortDateString();
                                bookRoom.TotalCost = Convert.ToInt32(dr["total_cost"]);
                                lstRoomBooked.Add(bookRoom);
                            }
                            dr.Close();
                            return lstRoomBooked;
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

        public void CancelRoomBooking(int? hotelRoomBookedID)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("CancelBooking", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter bookingID = new SqlParameter("@bookingID", SqlDbType.Int);
                    bookingID.Value = hotelRoomBookedID;

                    command.Parameters.Add(bookingID);
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

        public string GetDiscountAmount(int? hotelID, string promoCode)
        {
            string discountAmount = string.Empty;
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetOfferDiscountAmount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter promo_Code = new SqlParameter("@promo_code", SqlDbType.VarChar, 40);
                    SqlParameter hotel_id = new SqlParameter("@hotelID", SqlDbType.Int);

                    promo_Code.Value = promoCode;
                    hotel_id.Value = hotelID;

                    command.Parameters.Add(promo_Code);
                    command.Parameters.Add(hotel_id);

                    try
                    {
                        connection.Open();
                        discountAmount = (string)Convert.ToString(command.ExecuteScalar());
                        return discountAmount;
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