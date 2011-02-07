/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Catalog
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.DynamicDataManager.RegisterControl(this.CategoryGridView);
            this.DynamicDataManager.RegisterControl(this.StationeryGridView);
            
            this.CategoryGridView.EnableDynamicData(typeof(Category));
            this.StationeryGridView.EnableDynamicData(typeof(Stationery));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClearStationeryGridViewData();
            }
        }

        private void ClearStationeryGridViewData()
        {
            List<Stationery> stationeries = new List<Stationery>();
            this.StationeryGridView.DataSource = stationeries;
            this.StationeryGridView.DataBind();
        }

        protected void CategoryGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = (int) CategoryGridView.SelectedDataKey.Value;
            Category category = null;
            using (CatalogManager cm = new CatalogManager())
            {
                category = cm.GetCategoryByID(selectedIndex);
                if (category != null)
                {
                    List<Stationery> stationeries = category.Stationeries.ToList();
                    this.StationeryGridView.DataSource = stationeries;
                    this.StationeryGridView.DataBind();
                }
            }
        }

        protected void transactionButton_Click(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                StationeryEntities ctx = new StationeryEntities();
                ApprovalAudit audit = (from s in ctx.ApprovalAudits
                                select s).FirstOrDefault();
                ctx.ApprovalAudits.Attach(audit);
                ctx.ApprovalAudits.DeleteObject(audit);
                ctx.SaveChanges();
                ts.Complete();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Name = NameTextBox.Text;
            category.CreatedBy = Utilities.Membership.GetCurrentLoggedInUser().UserID;
            category.ModifiedBy = Utilities.Membership.GetCurrentLoggedInUser().UserID;

            CatalogManager categoryManager = new CatalogManager();

            try
            {
                categoryManager.CreateCategory(category);
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Category Creation Failed";
            }

        }

        protected void StationeryGridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                ErrorLabel.Text = "The category can't be deleted";
                e.ExceptionHandled = true;
            }
        }

        protected void StationeryGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                
                 ErrorLabel.Text = "The category can't be deleted";
            }
        }     
       
    }
}