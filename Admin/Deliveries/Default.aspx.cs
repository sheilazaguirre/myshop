using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_Deliveries_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetDeliveries();
    }

    void GetDeliveries()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT d.DeliveryNo, d.OrderNo,
                o.DateOrdered, d.Deadline,
                (SELECT SUM(QUANTITY) FROM OrderDetails
                WHERE OrderNo = d.OrderNo) AS TotalQuantity,
                d.DateDelivered, d.Status FROM Deliveries d
                INNER JOIN Orders o ON d.OrderNo = o.OrderNo
                ORDER BY o.DateOrdered DESC";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Deliveries");

                    lvDeliveries.DataSource = ds;
                    lvDeliveries.DataBind();
                }
            }
        }
    }
}