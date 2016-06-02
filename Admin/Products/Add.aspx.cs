using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Products_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCategories();
        }
    }

    void GetCategories()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = "SELECT CatID, Category FROM Categories";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    ddlCategories.DataSource = dr;
                    ddlCategories.DataTextField = "Category";
                    ddlCategories.DataValueField = "CatID";
                    ddlCategories.DataBind();

                    ddlCategories.Items.Insert(0, new ListItem("Select a category...", ""));
                }
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"INSERT INTO Products VALUES
                (@Name, @CatID, @Code, @Description, @Image,
                @Price, @IsFeatured, @Available, @CriticalLevel,
                @Maximum, @Status, @DateAdded, @DateModified)";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
                cmd.Parameters.AddWithValue("@Code", txtCode.Text);
                cmd.Parameters.AddWithValue("@Description",
                    Server.HtmlEncode(txtDesc.Text));
                cmd.Parameters.AddWithValue("@Image",
                    DateTime.Now.ToString("yyyyMMddhhmmss") + "_" +
                    fuImage.FileName);
                fuImage.SaveAs(Server.MapPath("~/Content/img/products/" +
                   DateTime.Now.ToString("yyyyMMddhhmmss") + "_" +
                   fuImage.FileName));
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@IsFeatured", ddlFeatured.SelectedValue);
                cmd.Parameters.AddWithValue("@Available", 0);
                cmd.Parameters.AddWithValue("@CriticalLevel", txtCritical.Text);
                cmd.Parameters.AddWithValue("@Maximum", txtMaximum.Text);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();
                Response.Redirect("Default.aspx");
            }
        }
    }
}