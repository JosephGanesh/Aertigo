using Aertigo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aertigo.Areas.User.Models
{
    public class CarousalModuleManager
    {
        Connection connectionString = new Connection();

        public List<CarousalModel> GetCarousals()
        {
            List<CarousalModel> carousalsDetails = new List<CarousalModel>();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetCarouselsDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                CarousalModel carousal = new CarousalModel();
                                carousal.HotelName = dr["hotel_name"].ToString();
                                carousal.HotelImage1 = dr["hotel_image1"].ToString();
                                carousal.HotelImage2 = dr["hotel_image2"].ToString();
                                carousal.OfferTitle = dr["offer_title"].ToString();
                                carousal.OfferPoster = dr["offer_poster"].ToString();
                                carousal.PromoCode = dr["promo_code"].ToString();
                                carousal.ValidFrom = Convert.ToDateTime(dr["valid_from"]).Date.ToShortDateString();
                                carousal.ValidTo = Convert.ToDateTime(dr["valid_to"]).Date.ToShortDateString();
                                carousal.DiscountAmount = dr["discount_amount"].ToString();
                                carousalsDetails.Add(carousal);
                            }
                            dr.Close();
                            return carousalsDetails;
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

        public CarousalModel GetCarousalDetailsOnHotelID(int? hotelIDSpecified)
        {
            CarousalModel carousal = new CarousalModel();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetCarouselsDetailsOnHotelID", connection))
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
                                carousal.HotelName = dr["hotel_name"].ToString();
                                carousal.HotelImage1 = dr["hotel_image1"].ToString();
                                carousal.HotelImage2 = dr["hotel_image2"].ToString();
                                carousal.OfferTitle = dr["offer_title"].ToString();
                                carousal.OfferPoster = dr["offer_poster"].ToString();
                                carousal.PromoCode = dr["promo_code"].ToString();
                                carousal.ValidFrom = Convert.ToDateTime(dr["valid_from"]).Date.ToString();
                                carousal.ValidTo = Convert.ToDateTime(dr["valid_to"]).Date.ToString();
                                carousal.DiscountAmount = dr["discount_amount"].ToString();
                            }
                            dr.Close();
                            return carousal;
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