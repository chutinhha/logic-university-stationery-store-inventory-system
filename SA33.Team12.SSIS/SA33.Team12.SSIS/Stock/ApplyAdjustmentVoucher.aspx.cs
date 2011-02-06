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
        private List<Stationery> stationeries = new List<Stationery>();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["stationeries"] != null)
            {
                stationeries = (List<Stationery>) Session["stationeries"];
            }
            else
            {
                stationeries = new List<Stationery>();
                Session["stationeries"] = stationeries;
            }
        }

        private void Populate()
        {
            gvAdjustmentItems.DataSource = stationeries;
            gvAdjustmentItems.DataBind();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool existing = false;
            using(CatalogManager cm = new CatalogManager())
            {
                Stationery stationery = cm.FindStationeryByID(int.Parse(ddlDescription.SelectedValue));
                foreach (Stationery s in stationeries)
                {
                    if (s.StationeryID == stationery.StationeryID)
                        existing = true;
                }
                if (!existing)
                {
                    stationeries.Add(stationery);
                }
                Populate();
            }
        }
    }
}