using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Stock
{
    public partial class AdjustmentVoucher : System.Web.UI.Page
    {
        List<AdjustmentVoucherTransaction> adjustments = new List<AdjustmentVoucherTransaction>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using(CatalogManager cm = new CatalogManager())
            {
                
            }
        }
    }
}