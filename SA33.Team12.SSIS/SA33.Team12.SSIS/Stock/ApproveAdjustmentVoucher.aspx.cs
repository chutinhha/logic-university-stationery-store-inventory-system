using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using System.Collections.Specialized;

namespace SA33.Team12.SSIS.Stock
{
    public partial class ApproveAdjustmentVoucher : System.Web.UI.Page
    {
        AdjustmentVoucherManager avm = new AdjustmentVoucherManager();
        List<AdjustmentVoucherTransaction> trans;
        User currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
            {
                List<AdjustmentVoucherTransaction> trans = avm.GetAllAdjustmentVoucherTransaction();
                gvAdjustments.DataSource = trans;
                gvAdjustments.DataBind();
            }
          
        }

        protected void gvAdjustments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int UserID = (int)DataBinder.Eval(e.Row.DataItem, "CreatedBy");
                if (UserID != 0)
                {
                    Literal aa = e.Row.FindControl("CreatedByLiteral") as Literal;
                    if (aa != null)
                    {
                        using (UserManager um = new UserManager())
                        {
                            User user = um.GetUserByID(UserID);
                            if (user != null) aa.Text = user.UserName;
                        }
                    }
                }
            }
        }

        protected void gvAdjustments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                ApproveSingleAdj(Convert.ToInt32(e.CommandArgument));
            }
            if (e.CommandName == "AdjustmentVoucherTransactionID")
            {
                Response.Redirect("~/Stock/ApplyAdjustmentVoucher.aspx?AdjustmentVoucherTransactionID=" + e.CommandArgument);
            }
        }

        private void ApproveSingleAdj(int AdjID)
        {
            AdjustmentVoucherTransaction tran = avm.GetAdjustmentVoucherTransactionByID(AdjID);
            AdjustmentVoucher voucher = new AdjustmentVoucher();

     
     //       r.ApprovedBy = currentUser.UserID;
     //       requisitionManager.ApproveRequisition(r);
    //        UtilityFunctions.SendEmail(r.RequisitionID + " - Your Request has been approved", "Dear " + r.CreatedByUser.FirstName + "<br />" + "Your request has been approved.", r.CreatedByUser);

        }

        protected void btnApproveAll_Click(object sender, EventArgs e)
        {
            if (trans != null)
            {
                foreach (AdjustmentVoucherTransaction tran in trans)
                {
                    ApproveSingleAdj(tran.AdjustmentVoucherTransactionID);
                }
            }
            Response.Redirect("ApproveAdjustmentVoucher.aspx");
        }
    }
}