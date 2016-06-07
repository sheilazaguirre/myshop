using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    bool IsExisting(string email)
    {
        bool existing = false; // initial value
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT Email FROM Users WHERE Email=@Email";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                existing = cmd.ExecuteScalar() == null ? false : true;
            }
        }
        return existing;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (IsExisting(txtEmail.Text))
        {
            error.Visible = true;
        }
        else
        {
            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                con.Open();
                string SQL = @"INSERT INTO Users VALUES 
                    (@TypeID, @Email, @Password, @FirstName,
                    @LastName, @Street, @Municipality, @City,
                    @Phone, @Mobile, @Status, @DateAdded,
                    @DateModified)";

                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@TypeID", 5);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txtFN.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLN.Text);
                    cmd.Parameters.AddWithValue("@Street", "");
                    cmd.Parameters.AddWithValue("@Municipality", "");
                    cmd.Parameters.AddWithValue("@City", "");
                    cmd.Parameters.AddWithValue("@Phone", "");
                    cmd.Parameters.AddWithValue("@Mobile", "");
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.ExecuteNonQuery();
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}