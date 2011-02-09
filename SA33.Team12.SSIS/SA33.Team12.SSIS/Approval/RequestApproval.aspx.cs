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
    public partial class RequestApproval : AppCode.PageBase
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
            }
        }
     
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName == "Approve")
            {
                ApproveSingleReq(Convert.ToInt32(e.CommandArgument));
                Response.Redirect("~/Approval/RequestApproval.aspx");
            }
            if (e.CommandName == "RequestID")
            {
                Response.Redirect("~/RequestStationery/StationeryRequest.aspx?RequestID=" + e.CommandArgument);
            }
            if (e.CommandName == "Reject")
            {
                RejectSingleReq(Convert.ToInt32(e.CommandArgument));
                Response.Redirect("~/Approval/RequestApproval.aspx");
            }
        }

        private void ApproveSingleReq(int reqID)
        {
            Requisition r = requisitionManager.GetRequisitionByID(reqID);
            r.ApprovedBy = currentUser.UserID;                   
            
            requisitionManager.ApproveRequisition(r);
            
           // UtilityFunctions.SendEmail(r.RequisitionID + " - Your Request has been approved", "Dear " + r.CreatedByUser.FirstName + "<br />" + "Your request has been approved.", r.CreatedByUser);
           
        }

        private void RejectSingleReq(int reqID)
        {
            Requisition r = requisitionManager.GetRequisitionByID(reqID);
            r.ApprovedBy = currentUser.UserID;
            requisitionManager.RejectRequisition(r);
            // UtilityFunctions.SendEmail(r.RequisitionID + " - Your Request has been approved", "Dear " + r.CreatedByUser.FirstName + "<br />" + "Your request has been approved.", r.CreatedByUser);

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
            Response.Redirect("~/Approval/RequestApproval.aspx");
        }
    }
}