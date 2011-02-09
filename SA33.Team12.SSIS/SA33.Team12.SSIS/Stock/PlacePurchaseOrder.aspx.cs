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
using System.Globalization;

namespace SA33.Team12.SSIS.Stock
{
    public partial class PlacePurchaseOrder : AppCode.PageBase
    {
        private List<PurchaseOrderItem> items = new List<PurchaseOrderItem>();
        private List<Stationery> stationeries = new List<Stationery>();
        //populate all the stationeries whose current quantity in hand are less than reorder level
        protected void Page_Load(object sender, EventArgs e)
        {
            Populate();
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
            //justify how many suppliers involed
            List<Supplier> suppliers = SupplierInvolved();

            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                foreach (Supplier s in suppliers)
                {
                    PurchaseOrder purchaseOrder = new PurchaseOrder();

                    purchaseOrder.PONumber = pom.CreatePONumber();
                    purchaseOrder.DateOfOrder = DateTime.Now;
                    purchaseOrder.AttentionTo = Convert.ToInt32(ddlAttentionTo.SelectedValue);
                    purchaseOrder.CreatedBy = Membership.GetCurrentLoggedInUser().UserID;
                    purchaseOrder.IsDelivered = false;
                    DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                    dtfi.ShortDatePattern = "MM/dd/yyyy";
                    dtfi.DateSeparator = "/";

                    purchaseOrder.DateToSupply = Convert.ToDateTime(txtDateToSupply.Text, dtfi);
                    purchaseOrder.SupplierID = s.SupplierID;

                    foreach (GridViewRow r in gvPOItems.Rows)
                    {
                        if (Convert.ToInt32(((DropDownList)r.FindControl("ddlSupplier")).SelectedValue.ToString()) == s.SupplierID)
                        {
                            PurchaseOrderItem item = new PurchaseOrderItem();
                            item.PurchaseOrder = purchaseOrder;
                            item.StationeryID = (int)gvPOItems.DataKeys[r.RowIndex].Value;
                            //item.QuantityToOrder = 5;
                            item.QuantityToOrder = Convert.ToInt32(((TextBox)r.FindControl("txtRecommend")).Text.ToString());
                            using (CatalogManager cm = new CatalogManager())
                            {
                                StationeryPriceSearchDTO criteria = new StationeryPriceSearchDTO();
                                criteria.SupplierID = Convert.ToInt32(((DropDownList)r.FindControl("ddlSupplier")).SelectedValue.ToString());
                                criteria.StationeryID = (int)item.StationeryID;
                                item.Price = cm.FindStationeryPricesByCriteria(criteria)[0].Price;

                                // record supplier ID for the PO
                                purchaseOrder.SupplierID = criteria.SupplierID;
                            }
                            purchaseOrder.PurchaseOrderItems.Add(item); // only this way works
                        }
                    }
                    pom.CreatePurchaseOrder(purchaseOrder);
                }
            }
        }

        private List<Supplier> SupplierInvolved()
        {
            List<Supplier> suppliers = new List<Supplier>();
            foreach (GridViewRow r in gvPOItems.Rows)
            {
                bool existing = false;
                int SupplierId = Convert.ToInt32(((DropDownList)r.FindControl("ddlSupplier")).SelectedValue.ToString());
                foreach (Supplier s in suppliers)
                {
                    if (SupplierId == s.SupplierID)
                        existing = true;
                }
                if (!existing)
                    using (CatalogManager cm = new CatalogManager())
                    {
                        Supplier supplier = cm.GetSupplierByID(SupplierId);
                        suppliers.Add(supplier);
                    }
            }
            return suppliers;
        }

        // to show recommended order quantity
        protected void gvPOItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int stationeryID = (int)DataBinder.Eval(e.Row.DataItem, "StationeryID");
                if (stationeryID != 0)
                {
                    TextBox tb = e.Row.FindControl("txtRecommend") as TextBox;
                    DropDownList SupplierDrowDownList = e.Row.FindControl("ddlSupplier") as DropDownList;
                    List<Supplier> suppliers = new List<Supplier>();

                    using (CatalogManager cm = new CatalogManager())
                    {
                        List<StationeryPrice> prices = cm.GetStationeryPricesByStationeryID(stationeryID);
                        Stationery stationery = cm.FindStationeryByID(stationeryID);
                        foreach (StationeryPrice p in prices)
                        {
                            suppliers.Add(p.Supplier);
                        }
                        SupplierDrowDownList.DataSource = suppliers;
                        SupplierDrowDownList.DataBind();

                        if (tb != null)
                        {
                            using (PurchaseOrderManager pom = new PurchaseOrderManager())
                            {
                                tb.Text = (pom.GetQuantityToOrder(stationeryID) - stationery.QuantityInHand 
                                    + stationery.ReorderLevel + stationery.ReorderQuantity).ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void gvPOItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPOItems.PageIndex = e.NewPageIndex;
            DataBind();
        }



    }
}