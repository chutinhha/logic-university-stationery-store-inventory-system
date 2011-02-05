using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.Utilities;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Stock
{
    public partial class PurchaseOrderDetail : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Populate();
                prevPage = Request.UrlReferrer.ToString();
            }
        }

        protected void Populate()
        {
            if (Request.QueryString["ID"] != "")
            {
                int purchaseOrderID = int.Parse(Request.QueryString["ID"]);
                using (PurchaseOrderManager pom = new PurchaseOrderManager())
                {
                    PurchaseOrder po = pom.FindPurchaseOrderByID(purchaseOrderID);
                    lblPONumber.Text = po.PONumber;
                    lblOrderDate.Text = po.DateOfOrder.ToShortDateString();
                    lblDateToSupply.Text = po.DateToSupply.ToShortDateString();
                    lblStatus.Text = (po.IsDelivered  ? "Delivered" : "Outstanding");
                    List<PurchaseOrderItem> items = po.PurchaseOrderItems.ToList<PurchaseOrderItem>();
                    this.gvPODetails.DataSource = items;
                    this.gvPODetails.DataBind();

                    // enable this button to go to replenish stock page when this PO has not yet been delivered
                    btnReplenish.Enabled = (po.IsDelivered ? false : true);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void btnReplenish_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }
    }
}