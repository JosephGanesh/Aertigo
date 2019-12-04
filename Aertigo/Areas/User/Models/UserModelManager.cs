using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aertigo.Areas.User.Models
{
    public class UserModelManager
    {
        Connection connectionString = new Connection();

        public List<SelectListItem> GetAllStates()
        {
            List<SelectListItem> states = new List<SelectListItem>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllStates", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                SelectListItem state = new SelectListItem();
                                state.Value = dr["stateID"].ToString();
                                state.Text = dr["stateName"].ToString();
                                states.Add(state);
                            }
                            dr.Close();
                        }
                        return states;
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


        public List<SelectListItem> GetAllCities(int stateID)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetCities", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter stID = new SqlParameter("@stateID", SqlDbType.Int);
                    stID.Value = stateID;
                    command.Parameters.Add(stID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                SelectListItem city = new SelectListItem();
                                city.Text = dr["cityName"].ToString();
                                city.Value = dr["cityID"].ToString();
                                cities.Add(city);
                            }
                            dr.Close();
                        }
                        return cities;
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

        public void AddNewUserWithCardID(UserModel user, string card_number)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AddUserWithCardID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter firstName = new SqlParameter("@first_name", SqlDbType.VarChar, 40);
                    SqlParameter lastName = new SqlParameter("@last_name", SqlDbType.VarChar, 40);
                    SqlParameter emailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 30);
                    SqlParameter dateOfBirth = new SqlParameter("@date_of_birth", SqlDbType.Date);
                    SqlParameter gender = new SqlParameter("@gender", SqlDbType.VarChar, 10);
                    SqlParameter cityID = new SqlParameter("@cityID", SqlDbType.Int);
                    SqlParameter phoneNumber = new SqlParameter("@phone_number", SqlDbType.VarChar, 12);
                    SqlParameter address = new SqlParameter("@address", SqlDbType.VarChar, 100);
                    SqlParameter userImage = new SqlParameter("@user_image", SqlDbType.VarChar, 1000);
                    SqlParameter cardNumber = new SqlParameter("@cardNumber", SqlDbType.VarChar, 40);
                    SqlParameter isActive = new SqlParameter("@isActive", SqlDbType.Bit);

                    firstName.Value = user.FirstName;
                    lastName.Value = user.LastName;
                    emailID.Value = user.EmailID;
                    password.Value = user.Password;
                    dateOfBirth.Value = user.DateOfBirth;
                    gender.Value = user.Gender;
                    cityID.Value = Convert.ToInt32(user.CityID);
                    phoneNumber.Value = user.PhoneNumber;
                    address.Value = user.Address;
                    userImage.Value = user.UserImage;
                    cardNumber.Value = card_number;
                    isActive.Value = user.isActive;

                    command.Parameters.Add(firstName);
                    command.Parameters.Add(lastName);
                    command.Parameters.Add(emailID);
                    command.Parameters.Add(password);
                    command.Parameters.Add(dateOfBirth);
                    command.Parameters.Add(gender);
                    command.Parameters.Add(cityID);
                    command.Parameters.Add(phoneNumber);
                    command.Parameters.Add(address);
                    command.Parameters.Add(userImage);
                    command.Parameters.Add(cardNumber);
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

        public void AddNewUserWithoutCardID(UserModel user)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AddUserWithoutCardID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter firstName = new SqlParameter("@first_name", SqlDbType.VarChar, 40);
                    SqlParameter lastName = new SqlParameter("@last_name", SqlDbType.VarChar, 40);
                    SqlParameter emailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 30);
                    SqlParameter dateOfBirth = new SqlParameter("@date_of_birth", SqlDbType.Date);
                    SqlParameter gender = new SqlParameter("@gender", SqlDbType.VarChar, 10);
                    SqlParameter cityID = new SqlParameter("@cityID", SqlDbType.Int);
                    SqlParameter phoneNumber = new SqlParameter("@phone_number", SqlDbType.VarChar, 12);
                    SqlParameter address = new SqlParameter("@address", SqlDbType.VarChar, 100);
                    SqlParameter userImage = new SqlParameter("@user_image", SqlDbType.VarChar, 1000);
                    SqlParameter cardID = new SqlParameter("@cardID", SqlDbType.Int);
                    SqlParameter isActive = new SqlParameter("@isActive", SqlDbType.Bit);

                    firstName.Value = user.FirstName;
                    lastName.Value = user.LastName;
                    emailID.Value = user.EmailID;
                    password.Value = user.Password;
                    dateOfBirth.Value = user.DateOfBirth;
                    gender.Value = user.Gender;
                    cityID.Value = Convert.ToInt32(user.CityID);
                    phoneNumber.Value = user.PhoneNumber;
                    address.Value = user.Address;
                    userImage.Value = user.UserImage;
                    cardID.Value = DBNull.Value;
                    isActive.Value = user.isActive;

                    command.Parameters.Add(firstName);
                    command.Parameters.Add(lastName);
                    command.Parameters.Add(emailID);
                    command.Parameters.Add(password);
                    command.Parameters.Add(dateOfBirth);
                    command.Parameters.Add(gender);
                    command.Parameters.Add(cityID);
                    command.Parameters.Add(phoneNumber);
                    command.Parameters.Add(address);
                    command.Parameters.Add(userImage);
                    command.Parameters.Add(cardID);
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

        public UserModel GetUserDetails(string userEmailID)
        {
            UserModel user = new UserModel();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetUserDetailsByEmailID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter emailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    emailID.Value = userEmailID;

                    command.Parameters.Add(emailID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                user.UserID = Convert.ToInt32(dr["userID"]);
                                user.FirstName = dr["first_name"].ToString();
                                user.LastName = dr["last_name"].ToString();
                                user.EmailID = dr["emailID"].ToString();
                                user.Password = dr["password"].ToString();
                                user.DateOfBirth = Convert.ToDateTime(dr["date_of_birth"]).ToShortDateString();
                                user.Gender = dr["gender"].ToString();
                                user.CityID = Convert.ToInt32(dr["cityID"]);
                                user.PhoneNumber = dr["phone_number"].ToString();
                                user.Address = dr["address"].ToString();
                                user.UserImage = dr["user_image"].ToString();
                                if (Convert.IsDBNull(dr["cardID"]))
                                {
                                    user.CardID = null;
                                }
                                else
                                {
                                    user.CardID = Convert.ToInt32(dr["cardID"]);
                                }
                                user.isActive = Convert.ToBoolean(dr["isActive"]);
                                user.StateID = Convert.ToInt32(dr["StateID"]);
                            }
                            dr.Close();
                            return user;
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

        public SelectListItem GetCityName(int userCityID)
        {
            SelectListItem city = new SelectListItem();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetCityName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter cityID = new SqlParameter("@cityID", SqlDbType.VarChar, 40);
                    cityID.Value = userCityID;

                    command.Parameters.Add(cityID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                city.Text = dr["cityName"].ToString();
                                city.Value = dr["cityID"].ToString();
                            }
                        }
                        return city;
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

        public void UpdateUserDetails(UserModel user)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("UpdateUserDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter userID = new SqlParameter("@userID", SqlDbType.Int);
                    SqlParameter firstName = new SqlParameter("@first_name", SqlDbType.VarChar, 40);
                    SqlParameter lastName = new SqlParameter("@last_name", SqlDbType.VarChar, 40);
                    SqlParameter emailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 30);
                    SqlParameter dateOfBirth = new SqlParameter("@date_of_birth", SqlDbType.Date);
                    SqlParameter gender = new SqlParameter("@gender", SqlDbType.VarChar, 10);
                    SqlParameter cityID = new SqlParameter("@cityID", SqlDbType.Int);
                    SqlParameter phoneNumber = new SqlParameter("@phone_number", SqlDbType.VarChar, 12);
                    SqlParameter address = new SqlParameter("@address", SqlDbType.VarChar, 100);
                    SqlParameter userImage = new SqlParameter("@user_image", SqlDbType.VarChar, 1000);
                    SqlParameter cardID = new SqlParameter("@cardID", SqlDbType.Int);
                    SqlParameter isActive = new SqlParameter("@isActive", SqlDbType.Bit);

                    userID.Value = Convert.ToInt32(user.UserID);
                    firstName.Value = user.FirstName;
                    lastName.Value = user.LastName;
                    emailID.Value = user.EmailID;
                    password.Value = user.Password;
                    dateOfBirth.Value = user.DateOfBirth;
                    password.Value = user.Password;
                    gender.Value = user.Gender;
                    cityID.Value = Convert.ToInt32(user.CityID);
                    phoneNumber.Value = user.PhoneNumber;
                    address.Value = user.Address;
                    userImage.Value = user.UserImage;


                    if (user.CardID == null)
                    {
                        cardID.Value = DBNull.Value;
                    }
                    else
                    {
                        cardID.Value = user.CardID;
                    }

                    isActive.Value = user.isActive;
                    command.Parameters.Add(userID);
                    command.Parameters.Add(firstName);
                    command.Parameters.Add(lastName);
                    command.Parameters.Add(emailID);
                    command.Parameters.Add(password);
                    command.Parameters.Add(dateOfBirth);
                    command.Parameters.Add(gender);
                    command.Parameters.Add(cityID);
                    command.Parameters.Add(phoneNumber);
                    command.Parameters.Add(address);
                    command.Parameters.Add(userImage);
                    command.Parameters.Add(cardID);
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
    }
}