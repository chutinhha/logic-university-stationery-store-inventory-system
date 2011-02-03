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
    public partial class PlacePurchaseOrder : System.Web.UI.Page
    {

        //populate all the stationeries whose current quantity in hand are less than reorder level
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Populate();
            }
        }

        protected void Populate()
        {
            using (CatalogManager cm = new CatalogManager())
            {
                List<Stationery> stationeries = cm.GetStationeriesByQuantityInHandLessThanReorderLevel();
                this.gvPOItems.DataSource = stationeries;
                this.gvPOItems.DataBind();
            }
            //          lblCreatedBy.Text = Membership.GetCurrentLoggedInUser().UserName ;
            lblDate.Text = DateTime.Now.ToShortDateString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int suppID = -1;
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder();

                foreach (GridViewRow r in gvPOItems.Rows)
                {
                    PurchaseOrderItem item = new PurchaseOrderItem();

                    item.PurchaseOrderID = purchaseOrder.PurchaseOrderID;
                    item.StationeryID = Convert.ToInt32(r.Cells[0].Text.ToString());
                    item.QuantityToOrder = Convert.ToInt32(((TextBox)r.FindControl("TextBox2")).Text.ToString());
                    using (CatalogManager cm = new CatalogManager())
                    {
                        StationeryPriceSearchDTO criteria = new StationeryPriceSearchDTO();
                        criteria.SupplierID = Convert.ToInt32(((DropDownList)r.FindControl("DropDownList2")).SelectedValue.ToString());
                        criteria.StationeryID = item.StationeryID;
                        item.Price = (decimal) 88.88;
               //         item.Price = cm.FindStationeryPricesByCriteria(criteria).Price;    
                // the above command encounterd nullreference exception

                        // record supplier ID for the PO
                        suppID = criteria.SupplierID;
                    }
                    pom.CreatePurchaseOrderItem(item);
                }
                purchaseOrder.PONumber = "88888";
                purchaseOrder.SupplierID = suppID;
                purchaseOrder.DateOfOrder = DateTime.Now;
                purchaseOrder.AttentionTo = Convert.ToInt32(ddlAttentionTo.SelectedValue);
                purchaseOrder.CreatedBy = 1; //testing purpose only
                purchaseOrder.IsDelivered = false;
                purchaseOrder.DateToSupply = Convert.ToDateTime( txtDateToSupply.Text);  //dont know working or not

                PurchaseOrder newOrder = pom.CreatePurchaseOrder(purchaseOrder);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void gvPOItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}