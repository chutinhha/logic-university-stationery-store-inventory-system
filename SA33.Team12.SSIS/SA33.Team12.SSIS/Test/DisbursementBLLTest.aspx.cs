using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.BLL;
using System.Data;
using SA33.Team12.SSIS.Utilities;



namespace SA33.Team12.SSIS.Test
{
    public partial class DisbursementBLLTest : AppCode.PageBase
    {
        DisbursementManager disbursementBLL = new DisbursementManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<Disbursement> disbursement = disbursementBLL.FindAllDisbursement();
            //GridView1.DataSource = disbursement;
            //GridView1.DataBind();
        }
    }
}