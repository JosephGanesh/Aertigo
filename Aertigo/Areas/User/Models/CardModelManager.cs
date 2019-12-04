using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Aertigo.Utilities;

namespace Aertigo.Areas.User.Models
{
    public class CardModelManager
    {
        Connection connectionString = new Connection();

        public int AddNewCard(CardModel card)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AddNewCard", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter cardNumber = new SqlParameter("@card_number", SqlDbType.VarChar, 40);
                    SqlParameter cardType = new SqlParameter("@card_type", SqlDbType.VarChar, 40);
                    SqlParameter bankName = new SqlParameter("@bank_name", SqlDbType.VarChar, 40);
                    SqlParameter cardMerchant = new SqlParameter("@card_merchant", SqlDbType.VarChar, 40);
                    SqlParameter cardCVV = new SqlParameter("@card_cvv", SqlDbType.Int);
                    SqlParameter cardPassword = new SqlParameter("@card_password", SqlDbType.VarChar, 10);
                    SqlParameter cardValidFrom = new SqlParameter("@card_valid_from", SqlDbType.Date);
                    SqlParameter cardValidTo = new SqlParameter("@card_valid_to", SqlDbType.Date);
                    SqlParameter cardHolderName = new SqlParameter("@card_holder_name", SqlDbType.VarChar, 100);

                    cardNumber.Value = card.CardNumber;
                    cardType.Value = card.CardType;
                    bankName.Value = card.BankName;
                    cardMerchant.Value = card.CardMerchant;
                    cardCVV.Value = card.CardCVV;
                    cardPassword.Value = card.CardPassword;
                    cardValidFrom.Value = card.CardValidFrom;
                    cardValidTo.Value = card.CardValidTo;
                    cardHolderName.Value = card.CardHolderName;

                    command.Parameters.Add(cardNumber);
                    command.Parameters.Add(cardType);
                    command.Parameters.Add(bankName);
                    command.Parameters.Add(cardMerchant);
                    command.Parameters.Add(cardCVV);
                    command.Parameters.Add(cardPassword);
                    command.Parameters.Add(cardValidFrom);
                    command.Parameters.Add(cardValidTo);
                    command.Parameters.Add(cardHolderName);

                    try
                    {
                        connection.Open();
                        int count = command.ExecuteNonQuery();
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

        public void RemoveCard(string cardNumber)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("RemoveCard", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter card_number = new SqlParameter("@cardNumber", SqlDbType.VarChar, 40);
                    card_number.Value = cardNumber;

                    command.Parameters.Add(card_number);

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

        public CardModel FindCardByCardID(int? userCardID)
        {
            CardModel card = new CardModel();
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("FindCardByCardID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter cardID = new SqlParameter("@cardID", SqlDbType.Int);
                    cardID.Value = userCardID;

                    command.Parameters.Add(cardID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                card.CardID = Convert.ToInt32(dr["cardID"]);
                                card.CardNumber = dr["card_number"].ToString();
                                card.CardType = dr["card_type"].ToString();
                                card.BankName = dr["bank_name"].ToString();
                                card.CardMerchant = dr["card_merchant"].ToString();
                                card.CardCVV = Convert.ToInt32(dr["card_cvv"]);
                                card.CardPassword = dr["card_password"].ToString();
                                card.CardValidFrom = Convert.ToDateTime(dr["card_valid_from"]).ToShortDateString();
                                card.CardValidTo = Convert.ToDateTime(dr["card_valid_to"]).ToShortDateString();
                                card.CardHolderName = dr["card_holder_name"].ToString();
                            }
                            dr.Close();
                            return card;
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

        public void UpdateCardDetails(CardModel card)
        { 
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("UpdateCardDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter cardID = new SqlParameter("@cardID", SqlDbType.Int);
                    SqlParameter cardNumber = new SqlParameter("@card_number", SqlDbType.VarChar, 40);
                    SqlParameter cardType = new SqlParameter("@card_type", SqlDbType.VarChar, 40);
                    SqlParameter bankName = new SqlParameter("@bank_name", SqlDbType.VarChar, 40);
                    SqlParameter cardMerchant = new SqlParameter("@card_merchant", SqlDbType.VarChar, 40);
                    SqlParameter cardCVV = new SqlParameter("@card_cvv", SqlDbType.Int);
                    SqlParameter cardPassword = new SqlParameter("@card_password", SqlDbType.VarChar, 30);
                    SqlParameter cardValidFrom = new SqlParameter("@card_valid_from", SqlDbType.Date);
                    SqlParameter cardValidTo = new SqlParameter("@card_valid_to", SqlDbType.Date);
                    SqlParameter cardHolderName = new SqlParameter("@card_holder_name", SqlDbType.VarChar, 100);

                    cardID.Value = Convert.ToInt32(card.CardID);
                    cardNumber.Value = card.CardNumber;
                    cardType.Value = card.CardType;
                    bankName.Value = card.BankName;
                    cardMerchant.Value = card.CardMerchant;
                    cardCVV.Value = card.CardCVV;
                    cardPassword.Value = card.CardPassword;
                    cardValidFrom.Value = card.CardValidFrom;
                    cardValidTo.Value = card.CardValidTo;
                    cardHolderName.Value = card.CardHolderName;

                    command.Parameters.Add(cardID);
                    command.Parameters.Add(cardNumber);
                    command.Parameters.Add(cardType);
                    command.Parameters.Add(bankName);
                    command.Parameters.Add(cardMerchant);
                    command.Parameters.Add(cardCVV);
                    command.Parameters.Add(cardPassword);
                    command.Parameters.Add(cardValidFrom);
                    command.Parameters.Add(cardValidTo);
                    command.Parameters.Add(cardHolderName);
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

        public string AuthenticateCard(string userEmailID, string userPassword)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("AuthenticCard", connection))
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
                        string cardID = (string) command.ExecuteScalar().ToString();
                        return cardID;
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

        public int GetCardIdByCardNumber(string cardNumber)
        {
            string con = connectionString.ConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand("GetCardIDByCardNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter card_number = new SqlParameter("@cardNumber", SqlDbType.VarChar, 40);
                    card_number.Value = cardNumber;

                    command.Parameters.Add(card_number);

                    try
                    {
                        connection.Open();
                        int cardID = (int) command.ExecuteScalar();
                        return cardID;
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