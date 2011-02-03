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
                lblPONumber.Text = ((PurchaseOrder)Session["PurchaseOrderSelected"]).PONumber;
                lblOrderDate.Text = ((PurchaseOrder)Session["PurchaseOrderSelected"]).DateOfOrder.ToShortDateString();
                lblDateToSupply.Text = ((PurchaseOrder)Session["PurchaseOrderSelected"]).DateToSupply.ToShortDateString();
            }
        }
    }
}