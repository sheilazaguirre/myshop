using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class Admin_Suppliers_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            string SQL = @"INSERT INTO Suppliers VALUES (@CompanyName,
                @ContactPerson, @Address, @Phone, @Mobile, @Status,
                @DateAdded, @DateModified)";

            con.Open();
            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@CompanyName", txtname.Text);
                cmd.Parameters.AddWithValue("@ContactPerson", txtcontactperson.Text);
                cmd.Parameters.AddWithValue("@Address", txtadd.Text);
                cmd.Parameters.AddWithValue("@Phone", txtphone.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtmobile.Text);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
        Response.Redirect("Default.aspx");
    }
}