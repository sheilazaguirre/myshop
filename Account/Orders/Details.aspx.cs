using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Account_Orders_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["OR"] == null)
            Response.Redirect("Default.aspx");
        else
        {
            int OR = 0;
            bool validOR = int.TryParse(Request.QueryString["OR"].ToString(), out OR);

            if (validOR)
            {
                GetCart(OR);
                GetPaymentSummary(OR);
                GetCustomerData();
            }
            else
                Response.Redirect("Default.aspx");
        }
    }

    void GetCart(int orderNo)
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
                cmd.Parameters.AddWithValue("@OrderNo",
                    orderNo);
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lvOrders.DataSource = dr;
                        lvOrders.DataBind();
                    }
                    else
                        Response.Redirect("Cart.aspx");
                }
            }

        }
    }

    void GetPaymentSummary(int orderNo)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT SUM(Amount) FROM OrderDetails 
                WHERE OrderNo=@OrderNo AND UserID=@UserID 
                HAVING COUNT(RefNo) > 0";
            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", orderNo);
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());
                double amount = cmd.ExecuteScalar() == null ? 0 :
                    Convert.ToDouble((decimal)cmd.ExecuteScalar());

                ltGross.Text = (amount * .88).ToString("#,###,##0.00");
                ltVAT.Text = (amount * .12).ToString("#,###,##0.00");
                ltDelivery.Text = (amount * .05).ToString("#,###,##0.00");
                ltTotal.Text = (amount * 1.05).ToString("#,###,##0.00");
            }
        }

        DateTime deliveryDate = DateTime.Now.AddDays(7);
    }

    void GetCustomerData()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT FirstName, LastName, Street,
                Municipality, City, Phone, Mobile FROM Users
                WHERE UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        txtFN.Text = dr["FirstName"].ToString();
                        txtLN.Text = dr["LastName"].ToString();
                        txtStreet.Text = dr["Street"].ToString();
                        txtMunicipality.Text = dr["Municipality"].ToString();
                        txtCity.Text = dr["City"].ToString();
                        txtPhone.Text = dr["Phone"].ToString();
                        txtMobile.Text = dr["Mobile"].ToString();
                    }
                }
            }
        }
    }
}