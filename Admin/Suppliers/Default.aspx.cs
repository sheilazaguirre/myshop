using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class Admin_Suppliers_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSuppliers();
        }
    }

    void GetSuppliers()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            string SQL = @"SELECT SupplierID, CompanyName, ContactPerson,
                Address, Phone, Mobile, Status, DateAdded, DateModified
                FROM Suppliers";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Anythingyouwouldliketowritehehehhehe");

                    lvSuppliers.DataSource = ds;
                    lvSuppliers.DataBind();
                }
            }
        }
    }

    //method overloading
    void GetSuppliers(string keyword)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            string SQL = @"SELECT SupplierID, CompanyName, ContactPerson,
                Address, Phone, Mobile, Status, DateAdded, DateModified
                FROM Suppliers WHERE SupplierID LIKE @keyword OR 
                CompanyName LIKE @keyword OR
                ContactPerson LIKE @keyword OR
                Address LIKE @keyword OR
                Phone LIKE @keyword OR
                Mobile LIKE @keyword";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Anythingyouwouldliketowritehehehhehe");

                    lvSuppliers.DataSource = ds;
                    lvSuppliers.DataBind();
                }
            }
        }
    }

    protected void lvSuppliers_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "archive") //user clicks delete icon
        {
            Literal ltSupplierID = (Literal)e.Item.FindControl("ltSupplierID");

            using (SqlConnection con = new SqlConnection(Util.GetConnection()))
            {
                string SQL = @"UPDATE Suppliers SET Status=@Status,
                    DateModified=@DateModified WHERE SupplierID=@SupplierID";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@SupplierID", ltSupplierID.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        GetSuppliers();
    }

    protected void lvSuppliers_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpSuppliers.SetPageProperties(e.StartRowIndex,
            e.MaximumRows, false);
        GetSuppliers();
    }

    protected void lvSuppliers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        dpSuppliers.Visible = dpSuppliers.TotalRowCount >
            dpSuppliers.PageSize;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtKeyword.Text == "")
            GetSuppliers();
        else
            GetSuppliers(txtKeyword.Text);
    }
}