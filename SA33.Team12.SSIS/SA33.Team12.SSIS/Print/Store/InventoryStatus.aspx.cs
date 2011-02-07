using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace SA33.Team12.SSIS.Print.Store
{
    public partial class InventoryStatus1 : System.Web.UI.Page
    {
        ReportDS ds;
        ReportDSTableAdapters.StationeriesTableAdapter ta;

        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDS ds = new ReportDS();
            ta = new ReportDSTableAdapters.StationeriesTableAdapter();
            ta.Fill(ds.Stationeries);

            ReportDocument doc = new ReportDocument();
            doc.Load(Server.MapPath("~/Print/Store/InventoryStatus.rpt"));

            doc.SetDataSource(ds);

            CrystalReportViewer1.ReportSource = doc;
        }
    }
}