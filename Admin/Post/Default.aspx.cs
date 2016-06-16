using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Post_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetPost();
    }

    void GetPost()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = "SELECT a.PostID, a.PostDate, b.PostType, a.Title, a.Post, a.FeaturedImage, a.Keywords, a.Status FROM Posts a INNER JOIN PostTypes b ON a.TypeID = b.TypeID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Posts");

                    lvPosts.DataSource = ds;
                    lvPosts.DataBind();
                }
            }
        }
    }
}