using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aertigo.Models
{
    public class HosterModelManager
    {
        public int ValidateHoster(int? hotelID, string hosterPassword)
        {
            Connection connectionString = new Connection();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("ValidateHoster", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelid = new SqlParameter("@hotelId", SqlDbType.Int);
                    SqlParameter password = new SqlParameter("password", SqlDbType.VarChar, 40);
                    hotelid.Value = hotelID;
                    password.Value = hosterPassword;

                    command.Parameters.Add(hotelid);
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