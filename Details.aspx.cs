using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] == null)
            Response.Redirect("Products.aspx");
        else
        {
            int productID = 0;
            bool validProduct =
                int.TryParse(Request.QueryString["ID"].ToString(),
                    out productID);

            if (validProduct)
            {
                GetData(productID);
            }
            else
                Response.Redirect("Products.aspx");
        }
    }

    void GetData(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT p.Name, p.Code, p.Image,
                c.Category, p.CatID, p.Description, p.Price
                FROM Products p INNER JOIN Categories c
                ON p.CatID = c.CatID
                WHERE p.ProductID = @ProductID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ID);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows) // record is existing
                    {
                        while (dr.Read())
                        {
                            ltName.Text = dr["Name"].ToString();
                            ltCode.Text = dr["Code"].ToString();
                            imgProduct.ImageUrl = "~/content/img/products/" +
                                dr["Image"].ToString();
                            hlkCategory.Text = dr["Category"].ToString();
                            hlkCategory.NavigateUrl = "~/Products.aspx?c=" +
                                dr["CatID"].ToString();
                            ltDesc.Text =
                                Server.HtmlDecode(dr["Description"].ToString());
                            double price = double.Parse(dr["Price"].ToString());
                            ltPrice.Text = price.ToString("#,###,###.00");
                        }
                    }
                    else
                        Response.Redirect("Products.aspx");
                }
            }
        }
    }
    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        Util.AddToCart(Request.QueryString["ID"].ToString(),
            txtQuantity.Text);
    }
}