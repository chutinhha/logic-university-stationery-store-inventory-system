﻿using System;
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
    public partial class StationeryRequest : System.Web.UI.Page
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
            myDataColumn.ColumnName = "CategoryID";
            myDataTable.Columns.Add(myDataColumn);
            

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "StationeryID";
            myDataTable.Columns.Add(myDataColumn);
            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["StationeryID"] };
           
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Quantity";
            myDataTable.Columns.Add(myDataColumn);


            return myDataTable;
        }

        //Insert data into datatable.
        private void AddDataToTable(int categoryID, int stationeryID, int quantity, DataTable myTable)
        {
            DataRow row;

            row = myTable.NewRow();

            row["CategoryID"] = categoryID;
            row["StationeryID"] = stationeryID;
            row["Quantity"] = quantity;            

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
            if (txtQuantity.Text.Trim() == "")
            {
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
          
            else
            {
                int categoryID = Convert.ToInt32(this.DropDownList1.Text.ToString());
                int stationeryID = Convert.ToInt32(this.ddlStationeryID.Text.ToString());
                int quantity = Convert.ToInt32(this.txtQuantity.Text.ToString().Trim());
                
                try
                {
                    DataRow foundRow = ((DataTable)Session["myDatatable"]).Rows.Find(stationeryID);
                    int rowNum = Convert.ToInt32(foundRow);
                    AddDataToTable(categoryID, stationeryID, quantity, (DataTable)Session["myDatatable"]);

                    this.GridView1.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
                    this.GridView1.DataBind();

                    this.ltQuantity.Text = "";
                    this.ltDescription.Text = "";                    
                    this.txtQuantity.Text = "";
                    
                }
                catch
                {
                    this.ltQuantity.Text = "";                    
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

        } 
   }
}