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
        List<PurchaseOrderItem> purchaseOrderItems = new List<PurchaseOrderItem>();
        List<Stationery> stationeryToOrder = new List<Stationery>();

        protected void Page_Init(object sender, EventArgs e)
        {
            this.DynamicDataManager.RegisterControl(this.gvPOItems);
            this.gvPOItems.EnableDynamicData(typeof(Stationery));
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        //populate all the stationeries whose current quantity in hand are less than reorder level
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (GridViewRow r in gvPOItems.Rows)
            {
                Control control =  (TextBox)r.FindControl("TextBox2");
            }
      //      lblCreatedBy.Text = Membership.GetCurrentLoggedInUser().UserName ;
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //StationerySearchDTO sdto = new StationerySearchDTO();
            //sdto.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            //using (CatalogManager cm = new CatalogManager())
            //{
            //    List<Stationery> stationeries = cm.FindStationeriesByCriteria(sdto);
            //    ddlDescription.DataSource = stationeries;
            //    ddlDescription.DataBind();

            //    ddlDescription.DataTextField = "Description";
            //    ddlDescription.DataValueField = "StationeryID";
            //}
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
                        item.Price = cm.FindStationeryPricesByCriteria(criteria)[0].Price;  
                        // record supplier ID for the PO
                        suppID = criteria.SupplierID;
                    }
                    pom.CreatePurchaseOrderItem(item);
                }
     //           purchaseOrder.PONumber = "";
                purchaseOrder.SupplierID = suppID;
                purchaseOrder.DateOfOrder = DateTime.Now;
                purchaseOrder.AttentionTo = Convert.ToInt32(ddlAttentionTo.SelectedValue);
                purchaseOrder.CreatedBy = 1; //testing 
                purchaseOrder.IsDelivered = false;
                purchaseOrder.DateToSupply = Convert.ToDateTime( txtDateToSupply.Text);  //dont know working or not

                PurchaseOrder newOrder = pom.CreatePurchaseOrder(purchaseOrder);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

    }
}