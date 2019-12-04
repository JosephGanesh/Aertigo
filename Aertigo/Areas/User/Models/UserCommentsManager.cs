using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.User.Models
{
    public class UserCommentsManager
    {
        Connection connectionString = new Connection();

        public void AddUserComment(int? hotelID, string emailID, string password, int? rating, string comment)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AddUserComments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelid = new SqlParameter("@hotelID", SqlDbType.Int);
                    SqlParameter emailid = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter userPassword = new SqlParameter("@password", SqlDbType.VarChar, 40);
                    SqlParameter userRating = new SqlParameter("@rating", SqlDbType.Int);
                    SqlParameter userComment = new SqlParameter("@comment", SqlDbType.VarChar);

                    hotelid.Value = hotelID;
                    emailid.Value = emailID;
                    userPassword.Value = password;
                    userRating.Value = rating;
                    userComment.Value = comment;

                    command.Parameters.Add(hotelid);
                    command.Parameters.Add(emailid);
                    command.Parameters.Add(userPassword);
                    command.Parameters.Add(userRating);
                    command.Parameters.Add(userComment);

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