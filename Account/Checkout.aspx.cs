using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Account_Checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCart();
            GetPaymentSummary();
            GetCustomerData();
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
                    if (dr.HasRows)
                    {
                        lvCart.DataSource = dr;
                        lvCart.DataBind();
                    }
                    else
                        Response.Redirect("Cart.aspx");
                }
            }

        }
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

        DateTime deliveryDate = DateTime.Now.AddDays(7);
        txtDelivery.Text = deliveryDate.ToString("MMM. dd, yyyy");
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

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        #region Step 1: Update Customer Data

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"UPDATE Users SET FirstName=@FirstName,
                LastName=@LastName, Street=@Street, 
                Municipality=@Municipality, City=@City,
                Phone=@Phone, Mobile=@Mobile,
                DateModified=@DateModified
                WHERE UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@FirstName", txtFN.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLN.Text);
                cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                cmd.Parameters.AddWithValue("@Municipality", txtMunicipality.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Step 2: Insert Order Record

        int orderNo = 0;

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"INSERT INTO Orders VALUES (@DateOrdered,
                @PaymentMethod, @Status); SELECT TOP 1 OrderNo
                FROM Orders ORDER BY OrderNo DESC";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@DateOrdered", DateTime.Now);
                cmd.Parameters.AddWithValue("@PaymentMethod",
                    ddlStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                orderNo = (int)cmd.ExecuteScalar();
            }

        }

        #endregion

        #region Step 3: Update Cart Items

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"UPDATE OrderDetails SET OrderNo=@OrderNo,
                Status=@Status WHERE OrderNo=0 AND UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", orderNo);
                cmd.Parameters.AddWithValue("@Status", "Processing");
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());
                cmd.ExecuteNonQuery();
            }

        }

        #endregion

        #region Step 4: Insert Delivery Record

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"INSERT INTO Deliveries VALUES (@OrderNo,
                @Deadline, @DateDelivered, @Status)";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@OrderNo", orderNo);
                cmd.Parameters.AddWithValue("@Deadline",
                    DateTime.Now.AddDays(7));
                cmd.Parameters.AddWithValue("@DateDelivered", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.ExecuteNonQuery();

                Response.Redirect("~/Account/Orders/Default.aspx");
            }

        }

        #endregion
    }
}