using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Stock
{
    public partial class AdjustmentVoucher : System.Web.UI.Page
    {
        private List<StockLogTransaction> adjustments = new List<StockLogTransaction>();
        //      private List<Stationery> stationeries = new List<Stationery>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adjustments"] != null)
            {
                adjustments = (List<StockLogTransaction>)Session["adjustments"];
            }
            else
            {
                adjustments = new List<StockLogTransaction>();
                Session["adjustments"] = adjustments;
            }
        }

        private void Populate()
        {
            gvAdjustmentItems.DataSource = adjustments;
            gvAdjustmentItems.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool existing = false;
            using (CatalogManager cm = new CatalogManager())
            {
                Stationery stationery = cm.FindStationeryByID(int.Parse(ddlDescription.SelectedValue));
                foreach (StockLogTransaction adj in adjustments)
                {
                    if (adj.StationeryID == stationery.StationeryID)
                        existing = true;
                }
                if (!existing)
                {
                    StockLogTransaction adj = new StockLogTransaction();
                    adj.Reason = txtReason.Text.ToString();
                    adj.Quantity = int.Parse(txtQuantity.Text.ToString());
                    adj.StationeryID = stationery.StationeryID;
                    adj.Balance = stationery.QuantityInHand;
                    adj.Type = int.Parse(ddlType.SelectedValue);
                    adjustments.Add(adj);
                }
                txtQuantity.Text = "";
                txtReason.Text = "";
                Populate();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
            {
                AdjustmentVoucherTransaction tran = new AdjustmentVoucherTransaction();

                tran.VoucherNumber = "88888"; // tesing only
                tran.DateIssued = DateTime.Now;
                tran.CreatedBy = 1; //testing purpose only

                foreach (StockLogTransaction adj in adjustments)
                {
                    adj.AdjustmentVoucherTransaction = tran;
                    avm.CreateStockLogTransaction(adj);
                }

                avm.CreateAdjustmentVoucherTransaction(tran);
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
    }
}