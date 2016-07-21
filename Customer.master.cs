using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Customer : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetInfo();
        }
    }

    void GetInfo()
    {
        if (Session["userid"] == null) // user did not log in
        {
            user.Visible = false;
            cart.Visible = false;

            login.Visible = true;
            register.Visible = true;
        }
        else // user logged in
        {
            CountCartItems();
            GetCart();

            user.Visible = true;
            cart.Visible = true;

            login.Visible = false;
            register.Visible = false;

            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string SQL = @"SELECT FirstName + ' ' + LastName AS Name
                    FROM Users WHERE UserID=@UserID";

                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@UserID",
                        Session["userid"].ToString());
                    ltUser.Text = (string)cmd.ExecuteScalar();
                }
            }
        }
    }

    void CountCartItems()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT SUM(Quantity) FROM OrderDetails
                WHERE OrderNo=@OrderNo AND UserID=@UserID
                HAVING COUNT(RefNo) > 0";
            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());
                ltTotal_Cart.Text = cmd.ExecuteScalar() == null ? "0" :
                    ((int)cmd.ExecuteScalar()).ToString();
            }
        }
    }

    void GetCart()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT od.RefNo, p.ProductID, p.Name, p.Code,
                p.Price, p.Image, c.Category, od.Quantity,
                od.Amount FROM OrderDetails od 
                INNER JOIN Products p ON od.ProductID = p.ProductID
                INNER JOIN Categories c ON p.CatID = c.CatID
                WHERE od.OrderNo=@OrderNo AND od.UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    lvCart.DataSource = dr;
                    lvCart.DataBind();
                }
            }

        }
    }
}