using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Reports_Suppliers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSuppliers();
    }

    void GetSuppliers()
    {
        ReportDocument report = new ReportDocument();
        report.Load(Server.MapPath("~/Admin/Reports/rptSuppliers.rpt"));
        report.SetDatabaseLogon("sa", "benilde", "Sheila-PC", "myshop");
        report.SetParameterValue("User", "Sheila Zaguirre");

        report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Suppliers Report");
    }
}