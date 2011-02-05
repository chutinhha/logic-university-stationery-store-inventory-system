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
    public partial class ReplenishStock : System.Web.UI.Page
    {
        PurchaseOrder po = new PurchaseOrder();
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
                    po = pom.FindPurchaseOrderByID(purchaseOrderID);
                    List<PurchaseOrderItem> items = po.PurchaseOrderItems.ToList<PurchaseOrderItem>();
                    this.gvPOitems.DataSource = items;
                    this.gvPOitems.DataBind();

                    lblPONumber.Text = po.PONumber;
                    lblSupplier.Text = po.Supplier.CompanyName;
                    lblOrderDate.Text = po.DateOfOrder.ToShortDateString();
                    txtReceivedDate.Text = DateTime.Now.ToShortDateString();
                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                po.DONumber = txtDONumber.Text.ToString();
                po.IsDelivered = true;
                po.DateReceived = Convert.ToDateTime(txtReceivedDate.Text.ToString());
          //      po.ReceivedBy = 
                pom.UpdatePurchaseOrder(po);
            }
            

        }
    }
}