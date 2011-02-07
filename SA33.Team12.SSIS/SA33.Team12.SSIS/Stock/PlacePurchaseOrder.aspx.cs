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
            lblCreatedBy.Text = Membership.GetCurrentLoggedInUser().UserName;
            lblDate.Text = DateTime.Now.ToShortDateString();
        }

        // create Purchase Order and together with Purchase Order Items
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder();

                purchaseOrder.PONumber = "88888";
                purchaseOrder.DateOfOrder = DateTime.Now;
                purchaseOrder.AttentionTo = Convert.ToInt32(ddlAttentionTo.SelectedValue);
                purchaseOrder.CreatedBy = Membership.GetCurrentLoggedInUser().UserID; 
                purchaseOrder.IsDelivered = false;
                purchaseOrder.DateToSupply = DateTime.Now;
                //      purchaseOrder.DateToSupply = Convert.ToDateTime(txtDateToSupply.Text);  //dont know working or not

                foreach (GridViewRow r in gvPOItems.Rows)
                {
                    PurchaseOrderItem item = new PurchaseOrderItem();

                    item.PurchaseOrder = purchaseOrder;
                    item.StationeryID = (int)gvPOItems.DataKeys[r.RowIndex].Value;
                    item.QuantityToOrder = 5;
                  //   item.QuantityToOrder = Convert.ToInt32(((TextBox)r.FindControl("txtRecommend")).Text.ToString());
                    using (CatalogManager cm = new CatalogManager())
                    {
                        StationeryPriceSearchDTO criteria = new StationeryPriceSearchDTO();
                        criteria.SupplierID = Convert.ToInt32(((DropDownList)r.FindControl("ddlSupplier")).SelectedValue.ToString());
                        criteria.StationeryID = (int)item.StationeryID;
                        item.Price = (decimal)88.88;
                        //         item.Price = cm.FindStationeryPricesByCriteria(criteria).Price;    
                        // the above command encounterd nullreference exception

                        // record supplier ID for the PO
                        purchaseOrder.SupplierID = criteria.SupplierID;
                    }
                   // pom.CreatePurchaseOrderItem(item);
                    purchaseOrder.PurchaseOrderItems.Add(item);
                }
                pom.CreatePurchaseOrder(purchaseOrder);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        // to show recommended order quantity
        protected void gvPOItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    int StationeryID = (int)DataBinder.Eval(e.Row.DataItem, "StationeryID");
            //    if (StationeryID != 0)
            //    {
            //        TextBox tb = e.Row.FindControl("txtRecommend") as TextBox;
            //        DropDownList supplier = e.Row.FindControl("ddlSupplier") as DropDownList;
            //        if (tb != null)
            //        {
            //            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            //            {
            //                tb.Text = pom.GetQuantityToOrder(StationeryID).ToString();
            //            }
            //        }
            //        if (supplier != null)
            //        {
            //            using (CatalogManager cm = new CatalogManager())
            //            {
            //                //supplier.DataSource = cm.FindSuppliersByCriteria(new SupplierSearchDTO {  = });
            //            }
            //        }
            //    }

            //}
        }

    }
}