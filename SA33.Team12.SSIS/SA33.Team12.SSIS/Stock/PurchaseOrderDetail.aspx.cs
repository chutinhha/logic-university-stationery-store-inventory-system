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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Populate();
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
                    //               List<PurchaseOrderItem> items = po.PurchaseOrderItems.ToList<PurchaseOrderItem>();
                    //              not working ?!?!!?!
                    PurchaseOrderItemSearchDTO criteria = new PurchaseOrderItemSearchDTO();
                    criteria.PurchaseOrderID = purchaseOrderID;
                    List<PurchaseOrderItem> items = pom.FindPurchaseOrderItemByCriteria(criteria);
                    this.gvPODetails.DataSource = items;
                    this.gvPODetails.DataBind();
                }
            }
        }
    }
}