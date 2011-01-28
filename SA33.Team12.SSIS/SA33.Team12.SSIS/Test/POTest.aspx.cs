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
    public partial class POTest : System.Web.UI.Page
    {
        PurchaseOrderDAO poDAO = new PurchaseOrderDAO();
        protected void Page_Load(object sender, EventArgs e)
        {

            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                List<PurchaseOrder> pos = pom.FindAllPurchaseOrder();
                this.GridView1.DataSource = pos;
                this.GridView1.DataBind();
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PurchaseOrder po1 = poDAO.FindPurchaseOrderByID(1);
            Label1.Text = po1.DONumber;
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            PurchaseOrderDAO poDAO = new PurchaseOrderDAO();
            PurchaseOrder po = new PurchaseOrder();
            DateTime t1 = new DateTime(2010, 12, 12);
            DateTime t2 = new DateTime(2010, 12, 20);
            po.SupplierID = 2;
            po.PONumber = "90000";
            po.DateOfOrder = t1;
            po.DateToSuppy = t2;
            po.AttentionTo = 2;
            po.CreatedBy = 2;
            po.DateReceived = t2;
            po.IsDelivered = false;

            poDAO.CreatePurchaseOrder(po);
        }

        protected void ButtonFind_Click(object sender, EventArgs e)
        {
            PurchaseOrderSearchDTO criteria = new PurchaseOrderSearchDTO();

            criteria.SupplierID = Convert.ToInt32(TextBoxSupplier.Text.ToString());
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                List<PurchaseOrder> pos = pom.FindPurchaseOrderByCriteria(criteria);
                this.GridView1.DataSource = pos;
                this.GridView1.DataBind();
            }


        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            PurchaseOrderSearchDTO dto = new PurchaseOrderSearchDTO();
            dto.SupplierID = 3;
            using (PurchaseOrderManager pom = new PurchaseOrderManager())
            {
                List<PurchaseOrder> po = pom.FindPurchaseOrderByCriteria(dto);
                po[0].DONumber = "88888";
                PurchaseOrder poUpdated = pom.UpdatePurchaseOrder(po[0]);
            }
        }
    }
}