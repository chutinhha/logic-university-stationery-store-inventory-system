using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.BLL;
using System.Data;

namespace SA33.Team12.SSIS.Test
{
    public partial class StationeryRequest : System.Web.UI.Page
    {        
        private Requisition requisition;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable myDt = new DataTable();
                myDt = CreateDataTable();
                Session["myDatatable"] = myDt;
                this.RequisitionItemGridView.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
                this.RequisitionItemGridView.DataBind();
            }


            //if (Session["Requistion"] != null)
            //{
            //    requisition = (Requisition)Session["Requistion"];
            //}
            //else
            //{
            //    requisition = new Requisition();
            //    Session["Requistion"] = requisition;
            //}

            //RequisitionItemGridView.DataSource = requisition.RequisitionItems;
            ////RequisitionItemGridView.DataKeyNames = new string[] { "StationeryID" };
            //SpecialRequisitionItemGridView.DataSource = requisition.SpecialRequisitionItems;


            if (!IsPostBack)
            {                
                CategoryDDL.DataSource = CategoryDS;
                CategoryDDL.DataTextField = "Name";
                CategoryDDL.DataValueField = "CategoryID";

                StationeryDDL.DataSource = StationeryDS;
                StationeryDDL.DataTextField = "Description";
                StationeryDDL.DataValueField = "StationeryID";

                DropDownList1.DataSource = CategoryDS;
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "CategoryID";
                
            }
            DataBind();
        }

        protected void AddItemButton_Click(object sender, EventArgs e)
        {
            RequisitionItem item = new RequisitionItem()
            {
                StationeryID = Convert.ToInt32(StationeryDDL.SelectedItem.Value),
                QuantityRequested = Convert.ToInt32(QuantityNeededTextBox.Text)
            };

            AddReqitem(item);

            RequisitionItemGridView.DataBind();
        }

        protected void AddReqitem(RequisitionItem item)
        {
            if (item != null)
            {
                ((Requisition)Session["Requistion"]).RequisitionItems.Add(item);
            }
        }

        protected void AddSpecialReqitem(SpecialStationery splStationery, SpecialRequisitionItem item)
        {
            SpecialStationery specialStationery = null;
            if (splStationery != null)
            {
                CatalogDAO d = new CatalogDAO();
                specialStationery = d.CreateSpecialStationery(splStationery);
            }
            if (item != null && specialStationery != null)
            {
                item.SpecialStationeryID = specialStationery.SpecialStationeryID;
                ((Requisition)Session["Requistion"]).SpecialRequisitionItems.Add(item);
            }
            else
            {
                
            }
        }

        protected List<RequisitionItem> GetReqItems()
        {
            return ((Requisition)Session["Requistion"]).RequisitionItems.ToList<RequisitionItem>();
        }

        protected List<SpecialRequisitionItem> GetSpecialReqItems()
        {
            return ((Requisition)Session["Requistion"]).SpecialRequisitionItems.ToList<SpecialRequisitionItem>();
        }

        protected void CategoryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

            StationeryDDL.DataSource = StationeryDS;
            StationeryDDL.DataTextField = "Description";
            StationeryDDL.DataValueField = "StationeryID";
            DataBind();
        }

        protected void RequisitionItemGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RequisitionItemGridView.EditIndex = e.NewEditIndex;
            
        }

        protected void RequisitionItemGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {               
            e.NewValues["StatusID"] = ((TextBox)RequisitionItemGridView.FindControl("StationeryIDTextBox")).Text;
            e.NewValues["QuantityRequested"] = ((TextBox)RequisitionItemGridView.FindControl("QuantityRequestedTextBox")).Text;
        }     

        protected void AddSpecialItemButton_Click(object sender, EventArgs e)
        {
            SpecialStationery splStationery = new SpecialStationery()
            {                 
                ItemCode = "s001",
                CategoryID = Convert.ToInt32(DropDownList1.SelectedItem.Value),
                Description = StationeryDescriptionTextBox.Text,                
                Quantity = Convert.ToInt32(SpecialQuantityRequestedTextBox.Text),
                UnitOfMeasure = UOMTextBox.Text,
                CreatedBy = 1,
                ModifiedBy = 1,
                DateCreated = DateTime.Now.Date,
                DateModified = DateTime.Now.Date,
                IsApproved = false                
            };

            SpecialRequisitionItem item = new SpecialRequisitionItem()
            {                
                Name = "s001",
                Description = StationeryDescriptionTextBox.Text,   
                RemarkByRequester = ReasonTextBox.Text,
                QuantityRequested = Convert.ToInt32(SpecialQuantityRequestedTextBox.Text)
            };

           // AddSpecialReqitem(splStationery, item);
            SpecialRequisitionItemGridView.DataBind();
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            CreateRequisition(requisition);
        }

        protected void CreateRequisition(Requisition requisition)
        {
            RequisitionManager reqManager = new RequisitionManager();
            requisition.CreatedBy = 1;
            requisition.DateRequested = DateTime.Now.Date;
            requisition.DepartmentID = 1;
            requisition.RequisitionForm = "dds/123/112";
            requisition.UrgencyID = 2;
            reqManager.CreateRequisition(requisition);
        }

        private DataTable CreateDataTable()
        {
            DataTable requisitionItemsDT = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();

            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "CategoryID";
            requisitionItemsDT.Columns.Add(myDataColumn);           


            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "StationeryID";
            requisitionItemsDT.Columns.Add(myDataColumn);
            requisitionItemsDT.PrimaryKey = new DataColumn[] { requisitionItemsDT.Columns["StationeryID"] };

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "QuantityNeeded";
            requisitionItemsDT.Columns.Add(myDataColumn);

            return requisitionItemsDT;
        }

        //Insert data into datatable.
        private void AddDataToTable(int categoryID, int stationeryID,int quantity, DataTable myTable)
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

        //To delete a datarow in session datatable
        protected void RequisitionItemGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int stationeryID = Convert.ToInt32(this.StationeryDDL.Text.ToString());
            DeleteDataFromTable(0, (DataTable)Session["myDatatable"]);
            this.RequisitionItemGridView.DataSource = ((DataTable)Session["myDatatable"]).DefaultView;
            this.RequisitionItemGridView.DataBind();
        }

        

    }
}