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
    public partial class PlacePurchaseOrderSpecial : System.Web.UI.Page
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

        //protected void gvPOItems_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int stationeryID = (int)DataBinder.Eval(e.Row.DataItem, "StationeryID");
        //        if (stationeryID != 0)
        //        {
        //            TextBox tb = e.Row.FindControl("txtRecommend") as TextBox;
        //            DropDownList SupplierDrowDownList = e.Row.FindControl("ddlSupplier") as DropDownList;
        //            List<Supplier> suppliers = new List<Supplier>();

        //            using (CatalogManager cm = new CatalogManager())
        //            {
        //                List<StationeryPrice> prices = cm.GetStationeryPricesByStationeryID(stationeryID);
        //                Stationery stationery = cm.FindStationeryByID(stationeryID);
        //                foreach (StationeryPrice p in prices)
        //                {
        //                    suppliers.Add(p.Supplier);
        //                }
        //                SupplierDrowDownList.DataSource = suppliers;
        //                SupplierDrowDownList.DataBind();

        //                if (tb != null)
        //                {
        //                    using (PurchaseOrderManager pom = new PurchaseOrderManager())
        //                    {
        //                        tb.Text = (pom.GetQuantityToOrder(stationeryID) - stationery.QuantityInHand
        //                            + stationery.ReorderLevel + stationery.ReorderQuantity).ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

    }
}