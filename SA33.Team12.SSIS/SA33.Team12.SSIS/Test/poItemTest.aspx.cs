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
    public partial class poItemTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                List<PurchaseOrderItem> items = pom.FindAllPurchaseOrderItem();
                GridView1.DataSource = items;
                GridView1.DataBind();
            }
        }

        // test findPOItemByCriteria - POID
        protected void Button1_Click(object sender, EventArgs e)
        {
            PurchaseOrderItemSearchDTO criteria = new PurchaseOrderItemSearchDTO();
            criteria.PurchaseOrderID = Convert.ToInt32(TextBox1.Text.ToString());

            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                List<PurchaseOrderItem> items = pom.FindPurchaseOrderItemByCriteria(criteria);
                GridView1.DataSource = items;
                GridView1.DataBind();
            }
        }
        // test findPOItemByPOItemID
        protected void Button2_Click(object sender, EventArgs e)
        {
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                PurchaseOrderItem item = pom.FindPurchaseOrderItemByID(Convert.ToInt32(TextBox2.Text.ToString()));
                item.QuantityToOrder = 80;
                PurchaseOrderItem itemUpdate = pom.UpdatePurchaseOrderItem(item);
            }
        }
        //test CreatPOItem
        protected void ButtonAddNew_Click(object sender, EventArgs e)
        {
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                PurchaseOrderItem item = pom.FindPurchaseOrderItemByID(509);
                PurchaseOrderItem newItem = new PurchaseOrderItem();
                newItem.QuantityToOrder = item.QuantityToOrder;
                newItem.StationeryID = item.StationeryID;
                newItem.PurchaseOrderID = item.PurchaseOrderID;
                newItem.Price = item.Price;
                newItem.DeliveryRemarks = item.DeliveryRemarks;
                PurchaseOrderItem newItem1 = pom.CreatePurchaseOrderItem(newItem);
            }
          
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                PurchaseOrderItem item = pom.FindPurchaseOrderItemByID(Convert.ToInt32(TextBox2.Text.ToString()));
                pom.DeletePurchaseOrderItem(item);
            }
        }
    }
}