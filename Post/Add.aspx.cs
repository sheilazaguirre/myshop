using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Post_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetPost();
            //GetStatus();
        }
    }

    void GetPost()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = "SELECT TypeID, PostType FROM PostTypes ORDER BY PostType";
            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    ddlType.DataSource = dr;
                    ddlType.DataTextField = "PostType";
                    ddlType.DataValueField = "TypeID";
                    ddlType.DataBind();

                    ddlType.Items.Insert(0, new ListItem("Select a post type", ""));
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = "INSERT INTO Posts VALUES (@PostDate, @TypeID, @Title, @Post, @FeaturedImage, @Keywords, @Status)";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@PostDate", txtPD.Text);
                cmd.Parameters.AddWithValue("@TypeID", ddlType.SelectedValue);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Post", Server.HtmlEncode(txtPost.Text));
                cmd.Parameters.AddWithValue("@FeaturedImage", DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + fuImage.FileName);
                fuImage.SaveAs(Server.MapPath("~/Content/img/products/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + fuImage.FileName));
                cmd.Parameters.AddWithValue("@Keywords", txtKeyword.Text);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.ExecuteNonQuery();
                Response.Redirect("Default.aspx");
            }
        }
    }
}