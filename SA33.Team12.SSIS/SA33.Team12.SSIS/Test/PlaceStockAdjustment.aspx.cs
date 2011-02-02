using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using System.Collections;
using System.Data;

namespace SA33.Team12.SSIS.Test
{
    public partial class PlaceStockAdjustment : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable myDt = new DataTable();
                myDt = CreateDataTable();
                Session["myDatatable"] = myDt;
                this.GridView1.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
                this.GridView1.DataBind();
            }
        }

        //Create the datatable structure
        private DataTable CreateDataTable()
        {
            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;
            
            myDataColumn = new DataColumn();
            
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "StationeryID";
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

        //Insert data into datatable.
        private void AddDataToTable(int stationeryID, string description, int type, int quantity, string reason, DataTable myTable)
        {
            DataRow row;

            row = myTable.NewRow();

            row["StationeryID"] = stationeryID;
            row["Description"] = description;
            row["Type"] = type;
            row["Quantity"] = quantity;
            row["Reason"] = reason;

            myTable.Rows.Add(row);
        }

        //Delete data from datatable.
        private void DeleteDataFromTable(int stationeryID, DataTable myTable)
        {
            DataRow foundRow = myTable.Rows.Find(stationeryID);
            int rowNum = Convert.ToInt32(foundRow);
            myTable.Rows[rowNum].Delete();
        }
        
        //Add data to session datatable 
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlType.Text.Trim() == "")
            {
                this.ltStock.Text = "Required";
                return;
            }
            
            if (txtQuantity.Text.Trim() == "")
            {
                this.ltStock.Text = "";
                this.ltQuantity.Text = "Required";
                return;
            }
            if (txtQuantity.Text.Trim() != "")
            {
                try
                {
                    int i = Convert.ToInt32(txtQuantity.Text.ToString().Trim());
                }
                catch
                {
                    this.ltQuantity.Text = "Numeric!";
                    return;
                }
            }
            if (txtReason.Text.Trim() == "")
            {
                this.ltQuantity.Text = "";
                this.ltReason.Text = "Required";
                return;
            }
            else
            {
                int stationeryID = Convert.ToInt32(this.ddlStationeryID.Text.ToString());
                int type = Convert.ToInt32(this.ddlType.Text.ToString());
                int quantity = Convert.ToInt32(this.txtQuantity.Text.ToString().Trim());
                String reason = this.txtReason.Text.Trim();

                try
                {
                    //Start Query the stationery description
                    CatalogDAO cat = new CatalogDAO();
                    String description = cat.GetStationeryByID(stationeryID).Description;
                    //End Query

                    DataRow foundRow = ((DataTable)Session["myDatatable"]).Rows.Find(stationeryID);
                    int rowNum = Convert.ToInt32(foundRow);
                    AddDataToTable(stationeryID, description, type, quantity, reason, (DataTable)Session["myDatatable"]);
                    
                    this.GridView1.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
                    this.GridView1.DataBind();

                    this.ltStock.Text = "";
                    this.ltQuantity.Text = "";
                    this.ltDescription.Text = "";
                    this.ltReason.Text = "";
                    this.txtQuantity.Text = "";
                    this.txtReason.Text = "";
                }
                catch
                {
                    this.ltStock.Text = "";
                    this.ltQuantity.Text = "";
                    this.ltReason.Text = "";
                    this.ltDescription.Text = "Duplicate";
                }
            }
        }

        //To delete a datarow in session datatable
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int stationeryID = Convert.ToInt32(this.ddlStationeryID.Text.ToString());
            DeleteDataFromTable(0, (DataTable)Session["myDatatable"]);
            this.GridView1.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
            this.GridView1.DataBind();
        }

        //To write the data in the table into database
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ltMessage.Text != "")
            {
                ltMessage.Text = "Data saved already saved. No further action taken";
            }
            if (ltMessage.Text == "")
            {
                using (AdjustmentVoucherManager adjustmentVoucherManager = new AdjustmentVoucherManager())
                {
                    AdjustmentVoucherTransaction adjustmentVoucherTransaction = new AdjustmentVoucherTransaction();

                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        StockLogTransaction item = new StockLogTransaction();
                        item.AdjustmentVoucherTransactionID = adjustmentVoucherTransaction.AdjustmentVoucherTransactionID;
                        item.StationeryID = Convert.ToInt32(r.Cells[1].Text);
                        item.Type = Convert.ToInt32(r.Cells[3].Text.ToString());
                        item.Quantity = Convert.ToInt32(r.Cells[4].Text);
                        item.Reason = r.Cells[5].Text;

                        adjustmentVoucherManager.CreateStockLogTransaction(item);

                        adjustmentVoucherTransaction.DateIssued = DateTime.Now;
                        adjustmentVoucherTransaction.VoucherNumber = "ME001"; //Must be unique system generated number
                        adjustmentVoucherTransaction.CreatedBy = 1; //Must be the userid of the person who creates it

               //         AdjustmentVoucherTransaction newAdjustmentVoucherTransaction = adjustmentVoucherManager.CreateAdjustmentVoucherTransaction(adjustmentVoucherTransaction);

                        ltMessage.Text = "Data saved";
                    }
                }
            }
        }

        //To clear all data in GridView
        protected void btnBack_Click(object sender, EventArgs e)
        {
            //No idea on how to clear the gridview data
        }
    }
}