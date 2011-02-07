using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Stock
{
    public partial class AdjustmentVoucherDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Populate();
            }
        }

        private void Populate()
        {
            if (Request.QueryString["ID"] != "")
            {
                int adjID = int.Parse(Request.QueryString["ID"]);
                using (AdjustmentVoucherManager avm = new AdjustmentVoucherManager())
                {
                    AdjustmentVoucherTransaction tran= avm.GetAdjustmentVoucherTransactionByID(adjID);
                    this.gvAdjustmentItems.DataSource = tran.StockLogTransactions.ToList<StockLogTransaction>();
                    this.gvAdjustmentItems.DataBind();

                    lblVoucherNumber.Text = tran.VoucherNumber;
                    lblIssueDate.Text = tran.DateIssued.ToShortDateString(); 
                    using (UserManager um = new UserManager()){
                        User u = um.GetUserByID(tran.CreatedBy);
                        lblCreatedBy.Text = u.UserName;
                    }
                    decimal totalCost = 0;
                    foreach(StockLogTransaction logTran in tran.StockLogTransactions)
                    {
                        totalCost += logTran.Quantity * logTran.Price;
                    }
                    lblCost.Text = String.Format("{0:C}", totalCost); 
                }
            }
        }


    }
}