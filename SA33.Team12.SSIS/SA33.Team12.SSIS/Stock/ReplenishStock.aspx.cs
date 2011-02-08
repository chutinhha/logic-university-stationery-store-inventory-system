using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using SA33.Team12.SSIS.Utilities;

namespace SA33.Team12.SSIS.Stock
{
    public partial class ReplenishStock : System.Web.UI.Page
    {
        //  public PurchaseOrder po = new PurchaseOrder();
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
                int purchaseOrderID = int.Parse(Request.QueryString["ID"]);
                using (PurchaseOrderManager pom = new PurchaseOrderManager())
                {
                    PurchaseOrder po = pom.FindPurchaseOrderByID(purchaseOrderID);
                    List<PurchaseOrderItem> items = po.PurchaseOrderItems.ToList<PurchaseOrderItem>();
                    this.gvPOitems.DataSource = items;
                    this.gvPOitems.DataBind();

                    lblPONumber.Text = po.PONumber;
                    lblSupplier.Text = po.Supplier.CompanyName;
                    lblOrderDate.Text = po.DateOfOrder.ToShortDateString();
                    txtReceivedDate.Text = DateTime.Now.ToShortDateString();
                    lblReceivedBy.Text = Membership.GetCurrentLoggedInUser().UserName;
                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                PurchaseOrder po = pom.FindPurchaseOrderByID(int.Parse(Request.QueryString["ID"]));
                po.DONumber = txtDONumber.Text.ToString();
                po.IsDelivered = true;
                po.DateReceived = Convert.ToDateTime(txtReceivedDate.Text.ToString());
                po.ReceivedBy = Membership.GetCurrentLoggedInUser().UserID;
                pom.UpdatePurchaseOrder(po);

                using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
                {
                    SA33.Team12.SSIS.DAL.AdjustmentVoucher av = new SA33.Team12.SSIS.DAL.AdjustmentVoucher();
                    av.VoucherNumber = avm.GenerateVoucherNumber();
                    av.CreatedBy = Membership.GetCurrentLoggedInUser().UserID;
                    av.DateIssued = po.DateOfOrder;
                    av.DateApproved = DateTime.Now;
                    av.ApprovedBy = Membership.GetCurrentLoggedInUser().UserID;

                    
                    foreach (PurchaseOrderItem item in po.PurchaseOrderItems)
                    {
                        // generate stocklog for each poItem
                        StockLog log = new StockLog();
                        log.AdjustmentVoucher = av;
                        log.Balance =  item.Stationery.QuantityInHand;
                        log.Quantity = item.QuantityToOrder;
                        log.Reason = "Supplier - " + po.Supplier.CompanyName;
                        log.StationeryID = item.StationeryID;
                        log.Type = 3;           // "replenishment" accroding to enu in DAL.AdjustmentVoucherDAO
                        log.Price = item.Price;
                        log.DateCreated = DateTime.Now;
                        av.StockLogs.Add(log) ;

                        // update replensih stationery stock
                        item.Stationery.QuantityInHand = item.Stationery.QuantityInHand + item.QuantityToOrder;
                    }
                    avm.CreateAdjustmentVoucher(av);
                }
            }

            Response.Redirect("ViewPurchaseOrder.aspx");
        }

        protected void btnAdjust_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplyAdjustmentVoucher.aspx");
        }
    }
}