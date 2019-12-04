using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Aertigo.Utilities;

namespace Aertigo.Models
{
    public class UserModelManager
    {
        public int AuthenticateUser(string userEmailID, string userPassword)
        {
            Connection connectionString = new Connection();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("ValidateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter emailID = new SqlParameter("@emailID", SqlDbType.VarChar, 40);
                    SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 40);

                    emailID.Value = userEmailID;
                    password.Value = userPassword;

                    command.Parameters.Add(emailID);
                    command.Parameters.Add(password);

                    try
                    {
                        connection.Open();
                        int count = (int) command.ExecuteScalar();
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