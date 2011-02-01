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

        //populate all the stationeries whose current quantity in hand are less than reorder level
        protected void Page_Load(object sender, EventArgs e)
        {
            //using (CatalogManager cm = new CatalogManager())
            //{
            //    List<Stationery> stationeries = cm.GetAllStationeries();

            //    foreach (Stationery s in stationeries)
            //    {
            //        if (s.QuantityInHand <= s.ReorderLevel)
            //        {
            //            stationeryToOrder.Add(s);
            //        }
            //    }
            //}
            //gvPOItems.DataSource = stationeryToOrder;
            //gvPOItems.DataBind();
            //lblCreatedBy.Text = Membership.GetCurrentLoggedInUser().LastName + " " + Membership.GetCurrentLoggedInUser().FirstName;
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            StationerySearchDTO sdto = new StationerySearchDTO();
            sdto.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue) ;
            using (CatalogManager cm = new CatalogManager())
            {
                List<Stationery> stationeries = cm.FindStationeriesByCriteria(sdto);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
        }

    }
}