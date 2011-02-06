using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;

namespace SA33.Team12.SSIS.Print
{
    public partial class PrintRequestByIndividualEmployee : System.Web.UI.Page
    {
        RequisitionManager reqManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            reqManager = new RequisitionManager();
            if (!IsPostBack)
            {
                GridView1.DataSource = reqManager.GetRequisitionByEmployee();
                DataBind();
            }
        }
    }
}