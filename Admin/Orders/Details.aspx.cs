using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Admin_Orders_Details : System.Web.UI.Page
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
                GetCustomerData(OR);
                GetOrderSummary(OR);
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
                WHERE od.OrderNo=@OrderNo";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo",
                    orderNo);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lvOrders.DataSource = dr;
                        lvOrders.DataBind();
                    }
                    else
                        Response.Redirect("Default.aspx");
                }
            }

        }
    }

    void GetOrderSummary(int orderNo)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT OrderNo, DateOrdered, PaymentMethod,
                Status FROM Orders WHERE OrderNo=@OrderNo";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", orderNo);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ltOrderNo.Text = dr["OrderNo"].ToString();
                        DateTime dateOrdered =
                            DateTime.Parse(dr["DateOrdered"].ToString());
                        ltDateOrdered.Text = dateOrdered.ToString("MMMM dd");
                        ltPayment.Text = dr["PaymentMethod"].ToString();
                        ltStatus.Text = dr["Status"].ToString();

                        btnAccept.Visible = ltStatus.Text == "Pending" ? true : false;
                        btnReject.Visible = ltStatus.Text == "Pending" ? true : false;
                    }
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
                WHERE OrderNo=@OrderNo
                HAVING COUNT(RefNo) > 0";
            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", orderNo);
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

    void GetCustomerData(int orderNo)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT u.FirstName, u.LastName, u.Street,
                u.Municipality, u.City, u.Phone, u.Mobile 
                FROM OrderDetails od 
                INNER JOIN Users u ON od.UserID = u.UserID
                WHERE od.OrderNo=@OrderNo";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", orderNo);

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

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"UPDATE Orders SET Status=@OStatus
                WHERE OrderNo=@OrderNo;
                UPDATE Deliveries SET Status=@DStatus
                WHERE OrderNo=@OrderNo";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OStatus", "Approved");
                cmd.Parameters.AddWithValue("@OrderNo", ltOrderNo.Text);
                cmd.Parameters.AddWithValue("@DStatus", "Processing");
                cmd.ExecuteNonQuery();
                Response.Redirect("Default.aspx");
            }
        }
    }
}