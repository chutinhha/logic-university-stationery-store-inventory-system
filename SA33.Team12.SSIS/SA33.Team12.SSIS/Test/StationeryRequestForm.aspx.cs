using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Test
{
    public partial class StationeryRequestForm : System.Web.UI.Page
    {
        RequisitionManager requisitionManager;
        Requisition requisition;
        CatalogDAO catalogDAO;       
        UserDAO userdao;

        protected void Page_Init(object sender, EventArgs e)
        {
            requisitionManager = new RequisitionManager();
            catalogDAO = new CatalogDAO();
            userdao = new UserDAO();
            requisition = new Requisition();
            
            if (!IsPostBack)
            {
                requisition.DateRequested = DateTime.Now.Date;
                requisition.CreatedByUser = userdao.GetUserByID(1);
                requisition.ApprovedByUser = userdao.GetUserByID(2);
                requisition.Department = userdao.GetUserByID(1).Department;
                requisition.RequisitionForm = requisitionManager.GetRequisitionNumber(requisition);                
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (StationeryDDL.Text != string.Empty)
            {
                ItemCodeLiteral.Text = catalogDAO.GetStationeryByID(int.Parse(StationeryDDL.Text)).ItemCode;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateFields();
            }            
        }

        private void PopulateFields()
        {
            RequisitionDateLiteral.Text = requisition.DateRequested.Date.ToShortDateString();
            DepartmentNameLiteral.Text = requisition.Department.Code;
            DepartmentCodeLiteral.Text = requisition.Department.Name;
            EmployeeNameLiteral.Text = requisition.CreatedByUser.FirstName;
            EmployeeNumberLiteral.Text = requisition.CreatedByUser.UserName;
            EmployeeEmailLiteral.Text = requisition.CreatedByUser.Email;
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            RequisitionItem item = new RequisitionItem()
            {
                RequisitionID = requisition.RequisitionID,
                StationeryID = Convert.ToInt32(StationeryDDL.Text),
                QuantityRequested = Convert.ToInt32(QuantityTextBox.Text),                                
            };

            requisition.RequisitionItems.Add(item);
            
            if (requisition.RequisitionItems.Count > 0)
            {
                GridView1.DataSource = requisition.RequisitionItems;
                DataBind();
            }
        }
    }
}