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
    public partial class SpecialStationeries : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.DynamicDataManager.RegisterControl(this.SpecialStationeryGridView);                       
            this.SpecialStationeryGridView.EnableDynamicData(typeof(SpecialStationery));
          
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void SpecialStationeryGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            CatalogManager categoryManager = new CatalogManager();
            SpecialStationery specialStationery = new SpecialStationery();  
            specialStationery.Description = NameTextBox.Text;
            int count = categoryManager.GetSpecialStationeryCount();
            specialStationery.ItemCode = categoryManager.GenerateItemCode(specialStationery.Description, count);
            specialStationery.Quantity = 0;
            specialStationery.UnitOfMeasure = UOMTextBox.Text;
            specialStationery.DateCreated = DateTime.Now;
            specialStationery.DateModified = DateTime.Now;
            specialStationery.CreatedBy = Utilities.Membership.GetCurrentLoggedInUser().UserID;
            specialStationery.ModifiedBy = Utilities.Membership.GetCurrentLoggedInUser().UserID;
            specialStationery.IsApproved = false;
            specialStationery.CategoryID = Convert.ToInt32(CategoryDDL.SelectedValue);

            try
            {
                categoryManager.CreateSpecialStationery(specialStationery);
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Category Creation Failed";
            }

        }

       

       
    }
}