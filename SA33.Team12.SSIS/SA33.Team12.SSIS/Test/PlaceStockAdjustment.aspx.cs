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
        private void AddDataToTable(int stationeryID, int quantity, string reason, DataTable myTable)
        {
            DataRow row;

            row = myTable.NewRow();

            row["StationeryID"] = stationeryID;
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
        
        //Add data to datatable which we have created 
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text.Trim() == "")
            {
                this.ltQuantity.Text = "Required";
                return;
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
                int quantity = Convert.ToInt32(this.txtQuantity.Text.ToString().Trim());
                String reason = this.txtReason.Text.Trim();

                try
                {
                    DataRow foundRow = ((DataTable)Session["myDatatable"]).Rows.Find(stationeryID);
                    int rowNum = Convert.ToInt32(foundRow);
                    AddDataToTable(stationeryID, quantity, reason, (DataTable)Session["myDatatable"]);

                    this.GridView1.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
                    this.GridView1.DataBind();

                    this.ltQuantity.Text = "";
                    this.ltDescription.Text = "";
                    this.ltReason.Text = "";
                    this.txtQuantity.Text = "";
                    this.txtReason.Text = "";
                }
                catch
                {
                    this.ltQuantity.Text = "";
                    this.ltReason.Text = "";
                    this.ltDescription.Text = "Duplicate";
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int stationeryID = Convert.ToInt32(this.ddlStationeryID.Text.ToString());
            //((DataTable)Session["myDatatable"])
            DeleteDataFromTable(0, (DataTable)Session["myDatatable"]);
            this.GridView1.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
            this.GridView1.DataBind();
        } 
   }
}