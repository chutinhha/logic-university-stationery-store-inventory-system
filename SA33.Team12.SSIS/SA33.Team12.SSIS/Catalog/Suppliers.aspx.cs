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
    public partial class Suppliers : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.DynamicDataManager.RegisterControl(this.SupplierGridView);            
            
            this.SupplierGridView.EnableDynamicData(typeof(Supplier));
          
        }

        protected void Page_Load(object sender, EventArgs e)
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
            Supplier supplier = new Supplier();
            supplier.SupplierCode = SupplierCodeTextBox.Text;
            supplier.CompanyName = NameTextBox.Text;
            supplier.TenderedYear = TenderYearCalender.SelectedDate;
            supplier.PreferredRank = Convert.ToInt32(RankingDDL.SelectedValue);

            CatalogManager categoryManager = new CatalogManager();

            try
            {
                categoryManager.CreateSupplier(supplier);
                SupplierGridView.DataBind();
            }
            catch (Exception)
            {
                ErrorLabel.Text = "Create Supplier Failed";
            }

        }

       
    }
}