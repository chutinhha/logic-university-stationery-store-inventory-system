using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Test
{
    public partial class DisbursementItemTest : System.Web.UI.Page
    {
        DisbursementDAO disbursementDAO = new DisbursementDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<DisbursementItem> disbursementItems = disbursementDAO.GetAllDisbursementItem();
            this.GridView1.DataSource = disbursementItems;
            this.GridView1.DataBind();
        }

        protected void btnGetDisbursementItemByID_Click(object sender, EventArgs e)
        {
            int disbursementItemID = Convert.ToInt32(txbDisbursementItemID.Text.ToString());
            DisbursementItem disbursementItem = disbursementDAO.GetDisbursementItemByID(disbursementItemID);
            List<DisbursementItem> disbursementItems = new List<DisbursementItem>();
            disbursementItems.Add(disbursementItem);
            this.GridView1.DataSource = disbursementItems;
            this.GridView1.DataBind();
        }


    }
}