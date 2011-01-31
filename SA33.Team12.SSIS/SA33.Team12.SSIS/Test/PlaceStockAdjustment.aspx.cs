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
    public partial class PlaceStockAdjustment : System.Web.UI.Page
    {
        AdjustmentVoucherDAO adjDAO = new AdjustmentVoucherDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            //using (AdjustmentVoucherManager adjm = new AdjustmentVoucherManager())
            //{
            //    List<StockLogTransaction> adj = adjm.FindAllStockLogTransaction();
            //    this.GridView1.DataSource = adj;
            //   this.GridView1.DataBind();
            //}

            CatalogDAO cat = new CatalogDAO();
            List<Category> data = new List<Category>();
            data = cat.GetAllCategories();
            List<String> st= new List<String>();
            foreach (Category c in data){
                st.Add(c.Name);

            }
            //for (int i = 0; i < data.Count; i++)
            //{
            ddlCategoryID.DataSource = (st);
            ddlCategoryID.DataBind();
            //}
        }

        protected void ddlCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CatalogDAO.GetStationeriesByCategory(Convert.ToInt32(ddlCategoryID.ToString));
        }

        //Test Search by Criteria
 


    }
}