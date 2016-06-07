using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Customer : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetInfo();
    }

    void GetInfo()
    {
        if (Session["userid"] == null) // user did not log in
        {
            cart.Visible = false;
            user.Visible = false;

            login.Visible = true;
            register.Visible = true;
        }
        else // user logged in
        {
            cart.Visible = true;
            user.Visible = true;

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
}
