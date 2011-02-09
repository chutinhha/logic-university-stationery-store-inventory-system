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
    public partial class PlacePurchaseOrderSpecial : AppCode.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack) 
            Populate();
        }

        private void Populate()
        {
            using (CatalogManager cm = new CatalogManager())
            {
                List<SpecialStationery> stationeries = cm.GetSpecialStationeryToBeOrdered();
                this.gvPOItems.DataSource = stationeries;
                this.gvPOItems.DataBind();
            }

            lblCreatedBy.Text = Membership.GetCurrentLoggedInUser().UserName;
            lblDate.Text = DateTime.Now.ToShortDateString();
        }

        protected void gvPOItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int specialStationeryID = (int)DataBinder.Eval(e.Row.DataItem, "SpecialStationeryID");
                if (specialStationeryID != 0)
                {
                    TextBox tb = e.Row.FindControl("txtOrderQuantity") as TextBox;
                    Literal requestedQuantity = e.Row.FindControl("ltlQuantity") as Literal;

                    using (PurchaseOrderManager pom = new PurchaseOrderManager())
                    {
                        requestedQuantity.Text = pom.GetQuantityToOrderSpecial(specialStationeryID).ToString();
                        tb.Text = requestedQuantity.Text;
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
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
                            item.SpecialStationeryID = (int)gvPOItems.DataKeys[r.RowIndex].Value;
                            item.QuantityToOrder = Convert.ToInt32(((TextBox)r.FindControl("txtOrderQuantity")).Text.ToString());
                            item.Price = 5.0m;
                            purchaseOrder.SupplierID = Convert.ToInt32(((DropDownList)r.FindControl("ddlSupplier")).SelectedValue);
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
    }
}