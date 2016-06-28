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

    public static double GetPrice(string productID)
    {
        double price = 0;
        using (SqlConnection con = new SqlConnection(GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT Price FROM Products WHERE
                ProductID=@ProductID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                price = Convert.ToDouble((decimal)cmd.ExecuteScalar());
            }
        }
        return price;
    }

    public static bool IsExisting(string productID)
    {
        bool existing = true;
        using (SqlConnection con = new SqlConnection(GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT ProductID FROM OrderDetails
                WHERE OrderNo=@OrderNo AND UserID=@UserID
                AND ProductID=@ProductID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", 1);
                // HttpContext.Current.Session["userid"].ToString()
                cmd.Parameters.AddWithValue("@ProductID", productID);

                existing = cmd.ExecuteScalar() == null ? false : true;
            }
        }
        return existing;
    }

    public static void AddToCart(string productID, string quantity)
    {
        bool existingProduct = IsExisting(productID);

        double price = GetPrice(productID);
        int qty = int.Parse(quantity);

        using (SqlConnection con = new SqlConnection(GetConnection()))
        {
            con.Open();
            string SQL = "";
            if (existingProduct)
            {
                SQL = @"UPDATE OrderDetails SET Quantity = Quantity + @Quantity,
                    Amount = Amount + @Amount WHERE OrderNo=@OrderNo
                    AND UserID=@UserID AND ProductID=@ProductID";
            }
            else
            {
                SQL = @"INSERT INTO OrderDetails VALUES
                (@OrderNo, @UserID, @ProductID, @Quantity,
                @Amount, @Status)";
            }

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", 1);
                // or HttpContext.Current.Session["userid"].ToString()
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@Quantity", quantity); // or qty :)
                cmd.Parameters.AddWithValue("@Amount", price * qty);
                cmd.Parameters.AddWithValue("@Status", "In Cart");
                cmd.ExecuteNonQuery();
            }
        }
    }
}