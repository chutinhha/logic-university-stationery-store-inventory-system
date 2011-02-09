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
    public partial class Stationeries : AppCode.PageBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.DynamicDataManager.RegisterControl(this.StationeryGridView);                       
            this.StationeryGridView.EnableDynamicData(typeof(Stationery));
          
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
            CatalogManager Manager = new CatalogManager();

            Stationery stationery = new Stationery();
            StationeryPrice[] stationeryPrice = new StationeryPrice[3];

            stationery.ItemCode = NameTextBox.Text;
            stationery.Description = DescriptionTextBox.Text;
            stationery.UnitOfMeasure = UOMTextBox.Text;
            stationery.CategoryID = Convert.ToInt32(CategoryDDL.SelectedValue);
            stationery.LocationID = Convert.ToInt32(LocationDDL.SelectedValue);
            stationery.ReorderLevel = Convert.ToInt32(ReorderLevelTextBox.Text);
            stationery.ReorderQuantity = Convert.ToInt32(ReorderQtyTextBox.Text);
            stationery.CreatedBy = Utilities.Membership.GetCurrentLoggedInUser().UserID;
            stationery.ModifiedBy = Utilities.Membership.GetCurrentLoggedInUser().UserID;
            stationery.DateCreated = DateTime.Now;
            stationery.DateModified = DateTime.Now;
            stationery.IsApproved = false;

            stationeryPrice[0] = new StationeryPrice();
            stationeryPrice[0].SupplierID = Convert.ToInt32(Supplier1DDL.SelectedValue);
            stationeryPrice[0].Price = Convert.ToInt32(Price1TextBox.Text);
            stationeryPrice[1] = new StationeryPrice();
            stationeryPrice[1].SupplierID = Convert.ToInt32(Supplier2DDL.SelectedValue);
            stationeryPrice[1].Price = Convert.ToInt32(Price2TextBox.Text);
            stationeryPrice[2] = new StationeryPrice();
            stationeryPrice[2].SupplierID = Convert.ToInt32(Supplier3DDL.SelectedValue);
            stationeryPrice[2].Price = Convert.ToInt32(Price3TextBox.Text);

            try
            {
                Stationery temp = Manager.CreateStationery(stationery);              

                foreach (StationeryPrice st in stationeryPrice)
                {
                    st.StationeryID = temp.StationeryID;
                    Manager.CreateStationeryPrice(st);
                }
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Create Stationery Failed";
            }

        }

        protected void StationeryPriceGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
        }

        protected void StationeryPriceGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

       

       
    }
}