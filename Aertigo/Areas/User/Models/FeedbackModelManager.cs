using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.User.Models
{
    public class FeedbackModelManager
    {
        Connection connectionString = new Connection();

        public void AddUserFeedback(FeedBackModel feedBack, string userEmailID, string userPassword)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AddUserFeedback", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter emailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 40);
                    SqlParameter feedback = new SqlParameter("@feedback", SqlDbType.VarChar);

                    emailID.Value = userEmailID;
                    password.Value = userPassword;
                    feedback.Value = feedBack.Feedback;

                    command.Parameters.Add(emailID);
                    command.Parameters.Add(password);
                    command.Parameters.Add(feedback);

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