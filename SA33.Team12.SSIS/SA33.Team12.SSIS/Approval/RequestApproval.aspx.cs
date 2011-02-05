using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using System.Collections.Specialized;

namespace SA33.Team12.SSIS.Approval
{
    public partial class RequestApproval : System.Web.UI.Page
    {
        RequisitionManager requisitionManager;
        List<Requisition> requisitions;
        User currentUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            requisitionManager = new RequisitionManager();           

            currentUser = Utilities.Membership.GetCurrentLoggedInUser();
            requisitions = requisitionManager.GetAllUnApprovedRequisitionByDepartmentID(currentUser.DepartmentID);
            if (requisitions != null)
            {
                GridView1.DataSource = requisitions;
                DataBind();
            }
            if (requisitions.Count == 0)
            {
                ApproveAllButton.Visible = false;
                Label1.Text = "No pending requests for approval.";
            }
        }

        private void ApproveSingleRequest()
        {
            NameValueCollection n = Request.QueryString;
            string requisitionID = string.Empty;
            string param = string.Empty;
            if (n.HasKeys())
            {
                param = n.GetKey(0);
                requisitionID = n.Get(0);
            }
            if (param == "RequestID")
            {
                Requisition r = requisitionManager.GetRequisitionByID(Convert.ToInt32(Request.QueryString["RequestID"]));
                r.ApprovedBy = currentUser.UserID;
                requisitionManager.ApproveRequisition(r);
                Response.Redirect("ApproveRequest.aspx");
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                ApproveSingleReq(Convert.ToInt32(e.CommandArgument));
            }
            if (e.CommandName == "RequestID")
            {
                Response.Redirect("~/RequestStationery/StationeryRequest.aspx?RequestID=" + e.CommandArgument);
            }
        }

        private void ApproveSingleReq(int reqID)
        {
            Requisition r = requisitionManager.GetRequisitionByID(reqID);
            r.ApprovedBy = currentUser.UserID;
            requisitionManager.ApproveRequisition(r);
            //UtilityFunctions.SendEmail(r.RequisitionID + " - Your Request has been approved", "Dear " + r.CreatedByUser.FirstName + "<br />" + "Your request has been approved.", new List<DAL.User>());
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (requisitions != null)
            {
                foreach (Requisition item in requisitions)
                {
                    ApproveSingleReq(item.RequisitionID);
                }
            }
            Response.Redirect("RequestApproval.aspx");
        }
    }
}