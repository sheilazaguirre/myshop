using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Account_Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCart();
            GetPaymentSummary();
        }
    }

    void GetCart()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT od.RefNo, p.ProductID, p.Name, p.Code, p.Price, p.Image, c.Category, od.Quantity, od.Amount
                FROM OrderDetails OD INNER JOIN Products p ON od.ProductID=p.ProductID
                INNER JOIN Categories c ON p.CatID = c.CatID
                WHERE od.OrderNo=@OrderNo AND od.UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
                cmd.Parameters.AddWithValue("@UserID", Session["userid"].ToString());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    btnCheckout.Visible = dr.HasRows;

                    lvCart.DataSource = dr;
                    lvCart.DataBind();
                }
            }
        }
    }


    protected void lvCart_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltProductID = (Literal)e.Item.FindControl("ltProductID");
        Literal ltRefNo = (Literal)e.Item.FindControl("ltRefNo");
        TextBox txtqty = (TextBox)e.Item.FindControl("txtqty");
        double price = Util.GetPrice(ltProductID.Text);
        int qty = int.Parse(txtqty.Text);

        if (e.CommandName == "updateqty")
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();

                string SQL = @"UPDATE OrderDetails SET Quantity=@Quantity, Amount=@Amount
                    WHERE RefNo=@RefNo";
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@Quantity", qty);
                    cmd.Parameters.AddWithValue("@Amount", qty * price);
                    cmd.Parameters.AddWithValue("@RefNo", ltRefNo.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        else if (e.CommandName == "removeitem")
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string SQL = @"DELETE FROM OrderDetails WHERE RefNo=@RefNo";

                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@RefNo", ltRefNo.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        GetCart();
        GetPaymentSummary();
    }

    void GetPaymentSummary()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT SUM(Amount) FROM OrderDetails
                WHERE OrderNo=@OrderNo AND UserID=@UserID 
                HAVING COUNT(RefNo) > 0";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", 0);
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
    }
}