using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SA33.Team12.SSIS.Print
{
    public partial class Default : AppCode.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/RequestStationery/ViewRequestHistory.aspx");
        }
    }
}