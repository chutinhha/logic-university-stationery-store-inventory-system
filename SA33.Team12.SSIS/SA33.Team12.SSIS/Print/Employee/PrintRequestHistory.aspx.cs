using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace SA33.Team12.SSIS.Print.Employee
{
    public partial class PrintRequestHistory : System.Web.UI.Page
    {
        ReportDS ds;
        ReportDSTableAdapters.RequisitionsTableAdapter ta;
        protected void Page_Load(object sender, EventArgs e)
        {
            ds = new ReportDS();
            ta = new ReportDSTableAdapters.RequisitionsTableAdapter();
            ta.Fill(ds.Requisitions);

            DataView dv = ds.Requisitions.DefaultView;

            dv.RowFilter = "createdby=" + Utilities.Membership.GetCurrentLoggedInUser().UserID;

            GridView1.DataSource = dv;
            DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}