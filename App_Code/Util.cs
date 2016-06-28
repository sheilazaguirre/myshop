using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    public Util()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string GetConnection()
    {
        return ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
    }

    public static void AddToCart(string productID, string quantity)
    {
        using (SqlConnection con = new SqlConnection(GetConnection()))
        {
            con.Open();
            string SQL = @"INSERT INTO OrderDetails VALUES
                (@OrderNo, @UserID, @ProductID, @Quantity,
                @Amount, @Status)";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", 1);
                // or HttpContext.Current.Session["userid"].ToString()
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Amount", "we don't know yet");
                cmd.Parameters.AddWithValue("@Status", "In Cart");
                cmd.ExecuteNonQuery();
            }
        }
    }
}