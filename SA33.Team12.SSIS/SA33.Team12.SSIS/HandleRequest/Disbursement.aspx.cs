using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.HandleRequest
{
    public partial class Disbursement : AppCode.PageBase
    {
        public int DisbursementId
        {
            get
            {
                try
                {
                    if (Request.QueryString["DisbursementID"] != null && Request.QueryString["DisbursementID"].ToString() != "")
                        return Convert.ToInt32(Request.QueryString["DisbursementID"].Trim());
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBindDisbursementFormView(this.DisbursementId);
            }
        }

        protected void DataBindDisbursementFormView(int disbursementId)
        {
            using (DisbursementManager dm = new DisbursementManager())
            {
                this.DisbursementFormView.DataSource =
                    dm.FindDisbursementByCriteria(
                        new DisbursementSearchDTO() { DisbursementID = disbursementId });
                this.DisbursementFormView.DataBind();
            }
        }

        protected void DisbursementFormView_DataBound(object sender, EventArgs e)
        {
            if (this.DisbursementFormView.CurrentMode == FormViewMode.ReadOnly)
            {
                //int disbursementID
                //    = (int)DataBinder.Eval(this.DisbursementFormView.DataItem, "DisbursementID");
                EntityCollection<DisbursementItem> disbursementItems
                    = (EntityCollection<DisbursementItem>)
                    DataBinder.Eval(this.DisbursementFormView.DataItem, "DisbursementItems");
                //using (DisbursementManager dm = new DisbursementManager())
                //{
                //    DAL.Disbursement disbursement = dm.FindDisbursementByID(disbursementID);
                //GridView DisbursementGridView
                //    = this.DisbursementFormView.FindControl("DisbursementGridView") 
                //        as GridView;
                DisbursementGridView.DataSource = disbursementItems;
                DisbursementGridView.DataBind();
                //}

            }
        }

        protected void DisbursementGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.DisbursementGridView.EditIndex = e.NewEditIndex;
            DataBindDisbursementFormView(this.DisbursementId);
        }

        protected void DisbursementGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DisbursementGridView.EditIndex = -1;
            DataBindDisbursementFormView(this.DisbursementId);
        }

        protected void DisbursementGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gridView = sender as GridView;
            int disbursementItemId = (int)gridView.DataKeys[e.RowIndex].Value;
            GridViewRow currentRow = gridView.Rows[e.RowIndex];
            TextBox QuantityDamagedTextBox = currentRow.FindControl("QuantityDamagedTextBox") as TextBox;
            TextBox ReasonTextBox = currentRow.FindControl("ReasonTextBox") as TextBox;

            using (DisbursementManager dm = new DisbursementManager())
            {
                DisbursementItem disbursementItem = dm.FindDisbursementItemByID(disbursementItemId);
                disbursementItem.QuantityDamaged = int.Parse(QuantityDamagedTextBox.Text.Trim());
                disbursementItem.Reason = ReasonTextBox.Text.Trim();
                dm.UpdateDisbursementItem(disbursementItem);
            }

            gridView.EditIndex = -1;
            DataBindDisbursementFormView(this.DisbursementId);
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HandleRequest/Disbursements.aspx");
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                AdjustmentVoucherTransaction adjustmentVoucher = new AdjustmentVoucherTransaction();
                for (int i = 0; i < DisbursementGridView.Rows.Count; i++)
                {
                    GridViewRow gridViewRow = DisbursementGridView.Rows[i];
                    int disbursementItemID = (int)DisbursementGridView.DataKeys[gridViewRow.RowIndex].Value;
                    DisbursementItem item = null;
                    using (DisbursementManager dm = new DisbursementManager())
                    {
                        item = dm.FindDisbursementItemByID(disbursementItemID);
                    }

                    if (item != null && item.QuantityDamaged > 0)
                    {
                        Stationery stationery = item.Stationery;
                        SpecialStationery specialStationery = item.SpecialStationery;

                        StockLogTransaction adj = new StockLogTransaction();
                        adj.Reason = item.Reason;
                        adj.Quantity = item.QuantityDamaged ?? 0;

                        if (item.StationeryID != null && item.StationeryID > 0)
                        {
                            adj.StationeryID = item.StationeryID;
                            StationeryPrice price = stationery.StationeryPrices.First();
                            adj.Price = price.Price;
                        }
                        else
                        {
                            adj.SpecialStationeryID = item.SpecialStationeryID;
                            adj.Price = 0.0m;
                        }

                        adj.Balance = stationery.QuantityInHand;
                        adj.DateCreated = DateTime.Now;
                        adj.Type = (int)AdjustmentType.Damage;
                        adjustmentVoucher.StockLogTransactions.Add(adj);
                    }
                    if (adjustmentVoucher.StockLogTransactions.Count > 0)
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
                            {
                                adjustmentVoucher.VoucherNumber = avm.GenerateVoucherNumber();
                                adjustmentVoucher.DateIssued = DateTime.Now;
                                adjustmentVoucher.CreatedBy = Utilities.Membership.LoggedInuser.UserID;
                                avm.CreateAdjustmentVoucherTransaction(adjustmentVoucher);
                            }
                            using (DisbursementManager dm = new DisbursementManager())
                            {
                                DAL.Disbursement disbursement = dm.FindDisbursementByID(this.DisbursementId);
                                disbursement.IsDistributed = true;
                                dm.UpdateDisbursement(disbursement);
                            }
                            ts.Complete();
                        }
                        Response.Redirect("~/HandleRequest/Disbursements.aspx");
                    }
                }
            }
            catch (Exception exception)
            {
                this.ErrorMessage.Text = exception.Message;
            }
        }
    }
}