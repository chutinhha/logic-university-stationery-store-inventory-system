using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.Utilities;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Stock
{
    public partial class ViewPurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    DataTable myDt = new DataTable();
            //    myDt = CreateDataTable();
            //    Session["myDatatable"] = myDt;
            //    this.gvPurchaseOrders.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
            //    this.gvPurchaseOrders.DataBind();
            //}
        }

         private DataTable CreateDataTable()
        {
            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;
            
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Purchase Order ID";
            myDataTable.Columns.Add(myDataColumn);
            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["StationeryID"] };

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Description";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Type";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Quantity";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Reason";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PurchaseOrderSearchDTO criteria = new PurchaseOrderSearchDTO();
            criteria.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue) ;
 //           criteria.StartDateOfOrder = Convert.ToDateTime(txtStartDateOfOrder.Text.ToString());
  //          criteria.EndDateOfOrder = Convert.ToDateTime(txtEndDateOfOrder.Text.ToString());
            using (PurchaseOrderManager pom = new PurchaseOrderManager() )
            {
                List<PurchaseOrder> purchaseOrders = pom.FindPurchaseOrderByCriteria(criteria);
                gvPurchaseOrder.DataSource = purchaseOrders;
                gvPurchaseOrder.DataBind();
            }
        }

        protected void gvPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PurchaseOrderSelected"] = (PurchaseOrder)(gvPurchaseOrder.SelectedValue);
            Response.Redirect("PurchaseOrderDetail.aspx");
        }
    }
}