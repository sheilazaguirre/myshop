using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Admin_Orders_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStart.Attributes.Add("min",
                DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd"));
            GetOrders();
        }
    }

    void GetOrders()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT DISTINCT o.OrderNo, o.DateOrdered, 
                o.PaymentMethod, o.Status, 
                (SELECT SUM(Quantity) FROM OrderDetails 
                WHERE OrderNo = o.OrderNo) AS TotalItems, 
                (SELECT SUM(Amount) FROM OrderDetails
                WHERE OrderNo = o.OrderNo) * 1.05 AS TotalAmount
                FROM Orders o 
                ORDER BY o.DateOrdered DESC";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ikawnabahalamaglagayngvaluesdito");

                    lvOrders.DataSource = ds;
                    lvOrders.DataBind();
                }
            }

        }
    }


    void GetOrders(DateTime start, DateTime end)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT DISTINCT o.OrderNo, o.DateOrdered, 
                o.PaymentMethod, o.Status, 
                (SELECT SUM(Quantity) FROM OrderDetails 
                WHERE OrderNo = o.OrderNo) AS TotalItems, 
                (SELECT SUM(Amount) FROM OrderDetails
                WHERE OrderNo = o.OrderNo) * 1.05 AS TotalAmount
                FROM Orders o 
                WHERE o.DateOrdered BETWEEN @start AND @end
                ORDER BY o.DateOrdered DESC";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {

                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end",
                    end.AddDays(1).AddSeconds(-1));
                //cmd.Parameters.AddWithValue("@end",
                //    end.AddHours(23).AddMinutes(59).AddSeconds(59));
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ikawnabahalamaglagayngvaluesdito");

                    lvOrders.DataSource = ds;
                    lvOrders.DataBind();
                }
            }

        } 
    }

    protected void SearchByDate(object sender, EventArgs e)
    {
        DateTime start = DateTime.Now;
        DateTime end = DateTime.Now;
        bool validStart = DateTime.TryParse(txtStart.Text, out start);
        bool validEnd = DateTime.TryParse(txtEnd.Text, out end);

        if (validStart && validEnd)
        {
            GetOrders(start, end);
        }
    }
}
