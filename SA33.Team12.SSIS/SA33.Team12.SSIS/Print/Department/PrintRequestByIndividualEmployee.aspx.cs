using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using System.Data;

namespace SA33.Team12.SSIS.Print
{
    public partial class PrintRequestByIndividualEmployee : AppCode.PageBase
    {
        ReportDS ds;
        ReportDSTableAdapters.VW_RequisitionsByEmployeeTableAdapter ta;

        protected void Page_Load(object sender, EventArgs e)
        {
            ds = new ReportDS();
            ta = new ReportDSTableAdapters.VW_RequisitionsByEmployeeTableAdapter();
            ta.Fill(ds.VW_RequisitionsByEmployee);

            DataView dv = ds.VW_RequisitionsByEmployee.DefaultView;
            dv.RowFilter = "username like '%" + Utilities.Membership.GetCurrentLoggedInUser().UserName + "%'";

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