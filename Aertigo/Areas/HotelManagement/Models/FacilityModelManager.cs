using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aertigo.Areas.HotelManagement.Models;
using Aertigo.Utilities;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Aertigo.Areas.HotelManagement.Models
{
    public class FacilityModelManager
    {
        Connection connectionString = new Connection();

        public List<FacilityModel> GetAllFacilities()
        {
            List<FacilityModel> facilities = new List<FacilityModel>();
            string con = connectionString.ConnectionString();

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetAllFacilities", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                FacilityModel facility = new FacilityModel();
                                facility.FacilityID = Convert.ToInt32(dr["facilityID"]);
                                facility.Description = dr["description"].ToString();
                                facilities.Add(facility);
                            }
                            dr.Close();
                            return facilities;
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

        public List<FacilityModel> GetFacilityOnHotelID(int? hotelIDSpecified)
        {
            List<FacilityModel> facilites = new List<FacilityModel>();
            string con = connectionString.ConnectionString();

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetFacilitiesDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter hotelID = new SqlParameter("@hotelID", SqlDbType.Int);
                    hotelID.Value = hotelIDSpecified;
                    command.Parameters.Add(hotelID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                FacilityModel facility = new FacilityModel();
                                facility.FacilityID = Convert.ToInt32(dr["facilityID"]);
                                facility.Description = dr["description"].ToString();
                                facility.isSelected = true;
                                facilites.Add(facility);
                            }
                            dr.Close();
                            return facilites;
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