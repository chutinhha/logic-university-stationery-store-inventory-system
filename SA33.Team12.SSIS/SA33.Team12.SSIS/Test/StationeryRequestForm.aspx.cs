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
        }

        private Requisition CreateNewRequisition()
        {
            Requisition requisition = new Requisition();
            requisition.DateRequested = DateTime.Now.Date;
            requisition.CreatedByUser = userdao.GetUserByID(1);
            requisition.Department = userdao.GetUserByID(1).Department;
            requisition.RequisitionForm = requisitionManager.GetRequisitionNumber(requisition);
            return requisition;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (StationeryDDL.Text != string.Empty)
            {
                ItemCodeLiteral.Text = catalogDAO.GetStationeryByID(int.Parse(StationeryDDL.Text)).ItemCode;
            }
            if (UrgencyDDL.SelectedValue != string.Empty)
            {
                requisition.UrgencyID = Convert.ToInt32(UrgencyDDL.SelectedValue);
            }

            ViewState.Add("requisition", requisition);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["requisition"] != null)
            {
                requisition = (Requisition)ViewState["requisition"];
            }
            else
            {
                requisition = CreateNewRequisition();
            }

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            requisitionManager.CreateRequisition(requisition);
        }
    }
}