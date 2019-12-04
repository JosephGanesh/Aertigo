using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aertigo.Models
{
    public class AdminModelManger
    {
        public int ValidateAdmin(string adminEmail, string adminPassword)
        {
            Connection connectionString = new Connection();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("ValidateAdmin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter emailID = new SqlParameter("@admin_username", SqlDbType.VarChar, 50);
                    SqlParameter password = new SqlParameter("@admin_password", SqlDbType.VarChar, 40);
                    emailID.Value = adminEmail;
                    password.Value = adminPassword;

                    command.Parameters.Add(emailID);
                    command.Parameters.Add(password);

                    try
                    {
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
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