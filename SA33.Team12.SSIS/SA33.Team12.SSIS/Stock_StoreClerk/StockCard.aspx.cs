using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Stock_StoreClerk
{
    public partial class StockCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void Populate()
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int stationeryID = int.Parse(ddlDescription.SelectedValue);

            using (CatalogManager cm = new CatalogManager())
            {
                Stationery stationery = cm.FindStationeryByID(stationeryID);
                List<Stationery> stationeries = new List<Stationery>();
                stationeries.Add(stationery);
                dvStockCard.DataSource = stationeries;
                dvStockCard.DataBind();
            }

            using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
            {

                List<StockLogTransaction> trans
                    =
                    avm.GetAllStockLogTransactionByCriteria(new AdjustmentVoucherTransactionSearchDTO { StationeryID = stationeryID });
                this.gvTransactions.DataSource = trans;
                this.gvTransactions.DataBind();
            }

            btnPrint.Enabled = true;
            btnPrint.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}