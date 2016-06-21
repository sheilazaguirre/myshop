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
        if (!IsPostBack)
        {
            GetProducts();
            GetCategories();
        }
    }

    void GetProducts()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();

            string SQL = @"SELECT p.ProductID, p.Image, c.Category, p.Price, 
                p.Name FROM Products p INNER JOIN Categories c
                ON p.CatID = c.CatID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Products");
                    lvProducts.DataSource = ds;
                    lvProducts.DataBind();


                    ltTotal.Text = ds.Tables[0].Rows.Count.ToString();
                    //con.Close();   --(optional)
                }
            }
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
}