using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Stock_StoreSupervisor_Manager
{
    public partial class AdjustmentVoucherDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Populate();
            }
        }

        private void Populate()
        {
            if (Request.QueryString["ID"] != "")
            {
                int adjID = int.Parse(Request.QueryString["ID"]);
                using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
                {
                    AdjustmentVoucherTransaction tran = avm.GetAdjustmentVoucherTransactionByID(adjID);
                    this.gvAdjustmentItems.DataSource = tran.StockLogTransactions.ToList<StockLogTransaction>();
                    this.gvAdjustmentItems.DataBind();

                    lblVoucherNumber.Text = tran.VoucherNumber;
                    lblIssueDate.Text = tran.DateIssued.ToShortDateString();
                    using (UserManager um = new UserManager())
                    {
                        User u = um.GetUserByID(tran.CreatedBy);
                        lblCreatedBy.Text = u.UserName;
                    }
                    decimal totalCost = avm.getTotalCost(tran);
                    lblCost.Text = String.Format("{0:C}", totalCost);
                }
            }
        }

        protected void gvAdjustmentItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int stationeryID = (int)DataBinder.Eval(e.Row.DataItem, "StationeryID");
                if (stationeryID != 0)
                {
                    Literal ltl = e.Row.FindControl("ltlDescription") as Literal;
                    if (ltl != null)
                    {
                        using (CatalogManager cm = new CatalogManager())
                        {
                            Stationery s = cm.FindStationeryByID(stationeryID);
                            if (s != null) ltl.Text = s.Description;
                        }
                    }
                }
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            RejectSingleAdj(int.Parse(Request.QueryString["ID"]));
        }

        private void RejectSingleAdj(int AdjID)
        {
            using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
            {
                AdjustmentVoucherTransaction tran = avm.GetAdjustmentVoucherTransactionByID(AdjID);
                avm.DeleteAdjustmentVoucherTransaction(tran);
                foreach (StockLogTransaction logTran in tran.StockLogTransactions)
                {
                    avm.DeleteStockLogTransaction(logTran);
                }
                //using (UserManager um = new UserManager())
                //{
                //    User u = um.GetUserByID(tran.CreatedBy);
                //    UtilityFunctions.SendEmail("Voucher Number " + tran.VoucherNumber + " has been Rejected", "Dear "
                //        + u.UserName + "<br />" + "Your adjustment Voucher has been Rejected. Reason:" + "<br />" + txtReason.Text, u);
                //}
            }
            Response.Redirect("~/Stock/ApproveAdjustmentVoucher.aspx");
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
                avm.DeleteAdjustmentVoucherTransaction(tran);
                foreach (StockLogTransaction logTran in tran.StockLogTransactions)
                {
                    StockLog log = new StockLog();
                    log.AdjustmentVoucher = voucher;
                    log.Balance = logTran.Balance;
                    log.Quantity = logTran.Quantity;
                    log.Reason = logTran.Reason;
                    log.StationeryID = logTran.StationeryID;
                    log.Type = logTran.Type;
                    log.Price = logTran.Price;
                    avm.CreateStockLog(log);
                    avm.DeleteStockLogTransaction(logTran);
                }
                avm.CreateAdjustmentVoucher(voucher);
                //          UtilityFunctions.SendEmail(voucher.AdjustmentVoucherID + " - Your adjustment voucher has been approved", "Dear " + voucher.CreatedByUser.FirstName + "<br />" + "Your request has been approved.", voucher.ApprovedByUser);

            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            ApproveSingleAdj(int.Parse(Request.QueryString["ID"]));
            Response.Redirect("~/Stock/ApproveAdjustmentVoucher.aspx");
        }
    }
}