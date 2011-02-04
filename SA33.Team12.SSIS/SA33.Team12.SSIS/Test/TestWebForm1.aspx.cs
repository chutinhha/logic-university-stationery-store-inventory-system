using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;
using System.Diagnostics;

namespace SA33.Team12.SSIS.Test
{
    public partial class TestWebForm1 : System.Web.UI.Page
    {
        RequisitionDAO rDAO = new RequisitionDAO();
        CatalogDAO cDAO = new CatalogDAO();
        Requisition requisition = new Requisition();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((DropDownList)DetailsView1.FindControl("CategoryDDL")).DataSource = cDAO.GetAllCategories();
                ((DropDownList)DetailsView1.FindControl("CategoryDDL")).DataTextField = "Name";
                ((DropDownList)DetailsView1.FindControl("CategoryDDL")).DataValueField = "CategoryID";
                ((DropDownList)DetailsView1.FindControl("CategoryDDL")).DataBind();
                int category = 1;
                if (((DropDownList)DetailsView1.FindControl("CategoryDDL")).SelectedItem.Text != string.Empty)
                {
                     category = Convert.ToInt32(((DropDownList)DetailsView1.FindControl("CategoryDDL")).SelectedValue);
                }
                    ((DropDownList)DetailsView1.FindControl("StationeryDDL")).DataSource = cDAO.GetStationeriesByCategory(category);
                    ((DropDownList)DetailsView1.FindControl("StationeryDDL")).DataValueField = "StationeryID";
                    ((DropDownList)DetailsView1.FindControl("StationeryDDL")).DataTextField = "Description";
                    ((DropDownList)DetailsView1.FindControl("StationeryDDL")).DataBind();
                
            }
        }

        private void TestMethod()
        {
            requisition.CreatedBy = 1;
            requisition.DepartmentID = 1;
            requisition.RequisitionForm = "dds/111/11";
            requisition.StatusID = 1;
            requisition.UrgencyID = 1;
            requisition.DateRequested = DateTime.Now;

            SpecialRequisitionItem splItem = new SpecialRequisitionItem()
            {
                QuantityIssued = 0,
                QuantityRequested = 10,
                Name = "test",
                Description = "desc",
                RemarkByRequester = "remarks",
                Price = 0
            };

            RequisitionItem item = new RequisitionItem()
            {
                StationeryID = 1,
                QuantityRequested = 12,
                QuantityIssued = 0,
                Price = 0
            };

            requisition.RequisitionItems.Add(item);
            requisition.SpecialRequisitionItems.Add(splItem);

            try
            {
                rDAO.CreateRequisition(requisition);
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            RequisitionItem item = new RequisitionItem();

            item.StationeryID = Convert.ToInt32(((DropDownList)DetailsView1.FindControl("StationeryDDL")).SelectedValue);
            item.QuantityRequested = Convert.ToInt32(((TextBox)DetailsView1.FindControl("QuantityTextBox")).Text);
            item.QuantityIssued = 0;
            item.Price = 0;

            Debug.WriteLine(item.StationeryID);
            Debug.WriteLine(item.QuantityRequested);
        }

        protected void StationeryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void CategoryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((DropDownList)DetailsView1.FindControl("CategoryDDL")).DataBind();   
        }
    }
}