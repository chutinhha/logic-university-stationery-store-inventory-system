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
    public partial class TestMichaelDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(Request.QueryString["ID"] != "")
                {
                    int disID = int.Parse(Request.QueryString["ID"]);
                    using(DisbursementManager disbursementManager = new DisbursementManager())
                    {
                        this.Detail.DataSource = new List<Disbursement>()
                                                     {disbursementManager.FindDisbursementByID(disID)};
                        this.Detail.DataBind();
                    }
                }
                

            }
        }
    }
}