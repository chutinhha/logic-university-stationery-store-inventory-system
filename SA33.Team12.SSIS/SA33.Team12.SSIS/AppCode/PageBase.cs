using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA33.Team12.SSIS.AppCode
{
    public class PageBase : System.Web.UI.Page
    {
        protected void Page_Error(object sender, EventArgs e)
        {
            // Pass the error on to the Generic Error page
            // Server.Transfer("~/GenericErrorPage.aspx", true);
        }
    }
}