using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Test
{
    public partial class StockLogTransactionTest : System.Web.UI.Page
    {
        AdjustmentVoucherDAO adjDAO = new AdjustmentVoucherDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            using (AdjustmentVoucherManager adjm = new AdjustmentVoucherManager())
            {
                List<StockLogTransaction> adj = adjm.FindAllStockLogTransaction();
                this.GridView1.DataSource = adj;
                this.GridView1.DataBind();
            }
        }

        //Test Search by Criteria
        protected void ButtonFind_Click(object sender, EventArgs e)
        {

            AdjustmentVoucherTransactionSearchDTO criteria = new AdjustmentVoucherTransactionSearchDTO();

            if (rbnStockLogID.Checked)
            {
                criteria.StockLogTransactionID = Convert.ToInt32(TextBox1.Text.ToString());
            }

            if (rbnAdjVoucherTran.Checked)
            {
                criteria.AdjustmentVoucherTransactionID = Convert.ToInt32(TextBox1.Text.ToString());
            }

            if (rbnStationeryID.Checked)
            {
                criteria.StationeryID = Convert.ToInt32(TextBox1.Text.ToString());
            }

            if (rbnType.Checked)
            {
                criteria.Type = Convert.ToInt32(TextBox1.Text.ToString());
            }

            if (rbnReason.Checked)
            {
                criteria.Reason = TextBox1.Text.ToString();
            }

            if (rbnQty.Checked)
            {
                criteria.Quantity = Convert.ToInt32(TextBox1.Text.ToString());
            }

            if (rbnBal.Checked)
            {
                criteria.Balance = Convert.ToInt32(TextBox1.Text.ToString());
            }

            using (AdjustmentVoucherManager adjm = new AdjustmentVoucherManager())
            {
                List<StockLogTransaction> slt = adjm.GetAllStockLogTransactionByCriteria(criteria);
                this.GridView1.DataSource = slt;
                this.GridView1.DataBind();
            }

        }


    }
}