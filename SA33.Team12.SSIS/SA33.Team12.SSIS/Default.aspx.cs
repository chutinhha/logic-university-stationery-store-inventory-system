using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS
{
    public partial class _Default : AppCode.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Utilities.Membership.IsLoggedIn)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                if(Utilities.Membership.IsAdmin)
                {
                    Response.Redirect("~/Administration/Users.aspx");
                }
                if (Utilities.Membership.IsDeptHead || Utilities.Membership.IsTempDeptHead)
                {
                    Response.Redirect("~/Administration/Users.aspx");
                }
                if (Utilities.Membership.IsDeptRepresentative)
                {
                    Response.Redirect("~/Distribution/Disbursements.aspx");
                }
                if (Utilities.Membership.IsEmployee)
                {
                    Response.Redirect("~/RequestStationery/StationeryRequest.aspx");
                }
                if (Utilities.Membership.IsStoreClerk)
                {
                    Response.Redirect("~/HandleRequest/StationeryRetrievalList.aspx");
                }
                if (Utilities.Membership.IsStoreManager)
                {
                    Response.Redirect("~/HandleRequest/StationeryRetrievalList.aspx");
                }
                if (Utilities.Membership.IsStoreSupervisor)
                {
                    Response.Redirect("~/HandleRequest/StationeryRetrievalList.aspx");
                }
            }
            catch // (Exception exception)
            {
                // throw exception;
                // Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}
