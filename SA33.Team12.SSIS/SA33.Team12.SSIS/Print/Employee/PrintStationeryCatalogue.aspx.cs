using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace SA33.Team12.SSIS.Print.Employee
{
    public partial class PrintStationeryCatalogue1 : System.Web.UI.Page
    {
        ReportDS ds;
        ReportDSTableAdapters.VW_StationeryCatalogueTableAdapter ta;
        ReportDocument doc;
        protected void Page_Load(object sender, EventArgs e)
        {
            ds = new ReportDS();
            ta = new ReportDSTableAdapters.VW_StationeryCatalogueTableAdapter();
            ta.Fill(ds.VW_StationeryCatalogue);           

            doc = new ReportDocument();
            doc.Load(Server.MapPath("~/Print/Employee/PrintStationeryCatalogue.rpt"));
           
            if (!IsPostBack)
            {
                doc.SetDataSource(ds);
            }

            CrystalReportViewer1.ReportSource = doc;
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            DataView dv = ds.VW_StationeryCatalogue.DefaultView;
            dv.RowFilter = "category like '%" + DropDownList1.SelectedValue + "%'";
            doc.SetDataSource(dv);
        }
    }
}