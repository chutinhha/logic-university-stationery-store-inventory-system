using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using SA33.Team12.SSIS.Utilities;

namespace SA33.Team12.SSIS.Stock_StoreSupervisor_Manager
{
    public partial class ViewAdjustmentVoucher : System.Web.UI.Page
    {

        List<AdjustmentVoucherTransaction> trans;

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void gvAdjustments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AdjustmentVoucherDetail")
            {
                Response.Redirect("~/Stock_StoreSupervisor_Manager/AdjustmentVoucherDetail.aspx?ID=" + e.CommandArgument);
            }
        }

       

        
    }
}