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
        // user did not select a category
        if (Request.QueryString["c"] == null)
        {
            if (!IsPostBack)
            {
                GetCategories();
                GetProducts();
            }
        }
        else // user selected a category
        {
            int catID = 0;
            bool validCategory =
                int.TryParse(Request.QueryString["c"].ToString(),
                    out catID);

            if (validCategory)
            {
                GetCategories();
                GetProducts(catID);
            }
            else
                Response.Redirect("Products.aspx");
        }
    }

    void GetCategories()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT c.CatID, c.Category, 
                (SELECT COUNT(ProductID) FROM Products
                WHERE CatID = c.CatID) AS TotalCount 
                FROM Categories c ORDER BY Category";

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


    void GetProducts()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT p.ProductID, c.Category, 
                p.Name, p.Image, p.Price FROM Products p
                INNER JOIN Categories c ON p.CatID = c.CatID
                ORDER BY NEWID()";

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
    /// Display products based from chosen category
    /// </summary>
    /// <param name="ID">Category ID</param>
    void GetProducts(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT p.ProductID, c.Category, 
                p.Name, p.Image, p.Price FROM Products p
                INNER JOIN Categories c ON p.CatID = c.CatID
                WHERE p.CatID=@CatID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@CatID", ID);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Products");

                    lvProducts.DataSource = ds;
                    lvProducts.DataBind();

                }
            }
        }
    }

    protected void lvProducts_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "addtocart")
        {
            Literal ltProductID = (Literal)e.Item.FindControl("ltProductID");
            // add to cart
            Util.AddToCart(ltProductID.Text, "1");
        }
    }
}