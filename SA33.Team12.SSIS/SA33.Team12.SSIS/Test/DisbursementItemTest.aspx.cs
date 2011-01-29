using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

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

        protected void btnGetItemsByID_Click(object sender, EventArgs e)
        {
            DisbursementItemSearchDTO searchCriteria = new DisbursementItemSearchDTO();
            searchCriteria.StationeryID = Convert.ToInt32(txbStationeryID.Text.ToString());
            List<DisbursementItem> items = disbursementDAO.FindDisbursementItemsByCriteria(searchCriteria);
            GridView1.DataSource = items;
            GridView1.DataBind();
        }

        protected void btnUpdateItem_Click(object sender, EventArgs e)
        {
            int disbursementItemID = Convert.ToInt32(txbDisbursementItemID.Text.ToString());
            DisbursementItem disbursementItem = disbursementDAO.GetDisbursementItemByID(disbursementItemID);
            int newQuantity = Convert.ToInt32(txbQuantity.Text.ToString());
            disbursementItem.QuantityDisbursed = newQuantity;
            DisbursementItem newItem = disbursementDAO.UpdateDisbursementItem(disbursementItem);
            List<DisbursementItem> Items = new List<DisbursementItem>();
            Items.Add(newItem);
            GridView1.DataSource = Items;
            GridView1.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int disbursementItemID = Convert.ToInt32(txbDisbursementItemID.Text.ToString());
            DisbursementItem disbursementItem = disbursementDAO.GetDisbursementItemByID(disbursementItemID);
            DisbursementItem newItem = new DisbursementItem();
            newItem.DisbursementID = disbursementItem.DisbursementID;
            newItem.StationeryRetrievalFormItemByDeptID = disbursementItem.StationeryRetrievalFormItemByDeptID;
            newItem.AdjustmentVoucherID = disbursementItem.AdjustmentVoucherID;
            newItem.StationeryID = disbursementItem.StationeryID;
            newItem.SpeicalStationeryID = disbursementItem.SpeicalStationeryID;
            newItem.QuantityDisbursed = disbursementItem.QuantityDisbursed;
            newItem.QuantityDamaged = disbursementItem.QuantityDamaged;
            newItem.Reason = disbursementItem.Reason;
            DisbursementItem createdItem = disbursementDAO.CreateDisbursementItem(newItem);
            List<DisbursementItem> Items = new List<DisbursementItem>();
            Items.Add(createdItem);
            GridView1.DataSource = Items;
            GridView1.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int ItemID = Convert.ToInt32(txbIDForDelete.Text.ToString());
            DisbursementItem Item = disbursementDAO.GetDisbursementItemByID(ItemID);
            disbursementDAO.DeleteDisbursementItem(Item);
            List<DisbursementItem> list = disbursementDAO.GetAllDisbursementItem();
            GridView1.DataSource = list;
            GridView1.DataBind();
        }


    }
}