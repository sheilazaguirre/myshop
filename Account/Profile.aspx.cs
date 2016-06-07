using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class Account_Profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
    }

    void GetData()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT Email, Password, FirstName, LastName,
                Street, Municipality, City, Phone, Mobile FROM Users
                WHERE UserID=@UserID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@UserID",
                    Session["userid"].ToString());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        txtEmail.Text = dr["Email"].ToString();
                        txtPassword.Text = dr["Password"].ToString();
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = "";
            if (txtPassword.Text == "") //user don't want to change PW
            {
                SQL = @"UPDATE Users SET Email=@Email, FirstName=@FirstName,
                        LastName=@LastName, Street=@Street,
                        Municipality=@Municipality, City=@City,
                        Phone=@Phone, Mobile=@Mobile,
                        DateModified=@DateModified
                        WHERE UserID=@UserID";
            }
            else 
            {
                SQL = @"UPDATE Users SET Email=@Email, Password=@Password, FirstName=@FirstName,
                        LastName=@LastName, Street=@Street,
                        Municipality=@Municipality, City=@City,
                        Phone=@Phone, Mobile=@Mobile,
                        DateModified=@DateModified
                        WHERE UserID=@UserID";
            }

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
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
                Response.Redirect(Request.Url.AbsoluteUri); //refresh current page
            }
        }
    }
}