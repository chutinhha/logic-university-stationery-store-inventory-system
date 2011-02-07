using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.Utilities;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Stock
{
    public partial class ViewPurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PurchaseOrderSearchDTO criteria = new PurchaseOrderSearchDTO();
            criteria.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue) ;
 //           criteria.StartDateOfOrder = Convert.ToDateTime(txtStartDateOfOrder.Text.ToString());
  //          criteria.EndDateOfOrder = Convert.ToDateTime(txtEndDateOfOrder.Text.ToString());
            using (PurchaseOrderManager pom = new PurchaseOrderManager() )
            {
                List<PurchaseOrder> purchaseOrders = pom.FindPurchaseOrderByCriteria(criteria);
                gvPurchaseOrder.DataSource = purchaseOrders;
                gvPurchaseOrder.DataBind();
            }
        }

        protected void gvPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}