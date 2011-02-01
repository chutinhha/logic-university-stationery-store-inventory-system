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
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["requisition"] != null)
            {
                requisition = (Requisition)Session["requisition"];                
            }
            else
            {
                requisition = CreateNewRequisition();
                Session["requisition"] = requisition;

            }

            if (!IsPostBack)
            {
                PopulateFields();
            }

            GridView1.DataSource = requisition.RequisitionItems;
            DataBind();
        }

        private void PopulateFields()
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RequisitionItem item = new RequisitionItem()
            {               
                QuantityRequested = 45
            };

            requisition.RequisitionItems.Add(item);
            Session["requisition"] = requisition;
            Response.Write(requisition.RequisitionItems.Count);
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
           
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = -1;
        }
    }
}