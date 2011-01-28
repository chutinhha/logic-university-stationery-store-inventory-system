using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Test
{
    public partial class DisbursementTest : System.Web.UI.Page
    {
        DisbursementDAO disbursementDAO = new DisbursementDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Disbursement> Disbursements = disbursementDAO.GetAllDisbursement();
            this.GridView1.DataSource = Disbursements;
            this.GridView1.DataBind();
        }

        protected void btnGetDisbursementByID_Click(object sender, EventArgs e)
        {
            int disbursementID = Convert.ToInt32(tbxDisbursementID.Text.ToString());
            Disbursement disbursement = disbursementDAO.GetDisbursementByID(disbursementID);
            List<Disbursement> disbursements=new List<Disbursement>();
            disbursements.Add(disbursement);
            this.GridView1.DataSource= disbursements;
            this.GridView1.DataBind();
        }

  

     
    }
}