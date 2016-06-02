using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class Admin_Suppliers_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // user did not select a record
        if (Request.QueryString["ID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else // user selected a record
        {
            int supplierID = 0; //initial value
            bool validSupplier = int.TryParse(
                                    Request.QueryString["ID"].ToString(),
                                    out supplierID);
            if (validSupplier) //ID is valid
            {
                if (!IsPostBack)
                {
                    GetData(supplierID);
                }
            }
            else //ID is invalid (or not a number)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    void GetData(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            string SQL = @"SELECT SupplierID, CompanyName, ContactPerson,
                Address, Phone, Mobile, Status FROM Suppliers
                WHERE SupplierID=@SupplierID";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@SupplierID", ID);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows) //record is existing
                    {
                        while (dr.Read())
                        {
                            ltID.Text = dr["SupplierID"].ToString();
                            txtName.Text = dr["CompanyName"].ToString();
                            txtContact.Text = dr["ContactPerson"].ToString();
                            txtAddress.Text = dr["Address"].ToString();
                            txtPhone.Text = dr["Phone"].ToString();
                            txtMobile.Text = dr["Mobile"].ToString();
                            ddlStatus.SelectedValue = dr["Status"].ToString();
                        }
                    }
                    else // record is not existing
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            string SQL = @"UPDATE Suppliers SET CompanyName=@CompanyName,
                ContactPerson=@ContactPerson, Address=@Address,
                Phone=@Phone, Mobile=@Mobile, Status=@Status,
                DateModified=@DateModified WHERE SupplierID=@SupplierID";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@CompanyName", txtName.Text);
                cmd.Parameters.AddWithValue("@ContactPerson", txtContact.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@SupplierID",
                    Request.QueryString["ID"].ToString());
                cmd.ExecuteNonQuery();
            }
        }
        Response.Redirect("Default.aspx");
    }
}