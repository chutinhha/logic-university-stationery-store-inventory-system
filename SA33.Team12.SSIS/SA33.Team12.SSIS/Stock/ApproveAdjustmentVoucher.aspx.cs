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

        List<AdjustmentVoucherTransaction> trans;
        User currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Populate();
            }

        }

        private void Populate()
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
            using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
            {
                AdjustmentVoucherTransaction tran = avm.GetAdjustmentVoucherTransactionByID(AdjID);
                SA33.Team12.SSIS.DAL.AdjustmentVoucher voucher = new SA33.Team12.SSIS.DAL.AdjustmentVoucher();

                voucher.CreatedBy = tran.CreatedBy;
                voucher.ApprovedBy = 3; //tesing only
                voucher.DateApproved = DateTime.Now;
                voucher.DateIssued = tran.DateIssued;
                voucher.VoucherNumber = tran.VoucherNumber;
                //  avm.DeleteAdjustmentVoucherTransaction(tran);             currently not working
                foreach (StockLogTransaction logTran in tran.StockLogTransactions)
                {
                    StockLog log = new StockLog();
                    log.AdjustmentVoucher = voucher;
                    log.Balance = logTran.Balance;
                    log.Quantity = logTran.Quantity;
                    log.Reason = logTran.Reason;
                    log.StationeryID = logTran.StationeryID;
                    log.Type = logTran.Type;

                }
                avm.CreateAdjustmentVoucher(voucher);
                UtilityFunctions.SendEmail(voucher.AdjustmentVoucherID + " - Your adjustment voucher has been approved", "Dear " + voucher.CreatedByUser.FirstName + "<br />" + "Your request has been approved.", voucher.ApprovedByUser);

            }

            Populate();
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