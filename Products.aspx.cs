using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["c"] != null) //Category is selected
        {
            int CatID = 0;
            bool validCategory = int.TryParse(Request.QueryString["c"].ToString(),
                out CatID);
            if (validCategory)
            {
                if (!IsPostBack)
                {
                    GetCategories();
                    GetProducts(CatID);
                }
            }
            else
                Response.Redirect("Products.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                GetProducts();
                GetCategories();
            }
        }
    }

    void GetProducts()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();

            string SQL = @"SELECT p.ProductID, p.Image, c.Category, p.Price, p.Name FROM Products p INNER JOIN Categories c ON p.CatID = c.CatID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Products");
                    lvProducts.DataSource = ds;
                    lvProducts.DataBind();

                    ltTotal.Text = ds.Tables[0].Rows.Count.ToString();
                }
            }
        }
    }

    /// <summary>
    /// Display products based from Category ID
    /// </summary>
    /// <param name="ID">Category ID</param>

    void GetProducts(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();

            string SQL = @"SELECT p.ProductID, p.Image, c.Category, p.Price, p.Name FROM Products p INNER JOIN Categories c ON p.CatID = c.CatID
                        WHERE p.CatID=@CatID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("CatID", ID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Products");
                    lvProducts.DataSource = ds;
                    lvProducts.DataBind();

                    ltTotal.Text = ds.Tables[0].Rows.Count.ToString();
                }
            }
        }
    }

    void GetCategories()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();

            string SQL = @"SELECT c.CatID, c.Category, (SELECT COUNT(ProductID) FROM Products WHERE CatID = c.CatID) as TotalCount From Categories c ORDER BY Category";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    lvCategories.DataSource = dr;
                    lvCategories.DataBind();
                }
            }
        }
    }

    protected void lvProducts_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "addtocart")
        {
            Literal ltProductID = (Literal)e.Item.FindControl("ltProductID");
            Util.AddToCart(ltProductID.Text, "1");

        }
    }
}