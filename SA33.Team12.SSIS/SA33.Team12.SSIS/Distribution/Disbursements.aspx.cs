using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Distribution
{
    public partial class Disbursements : System.Web.UI.Page
    {
        public int stationeryRetrievalFormId
        {
            get
            {
                try
                {
                    if (Request.QueryString["SRFId"] != null && Request.QueryString["SRFId"].ToString() != "")
                        return Convert.ToInt32(Request.QueryString["SRFId"].Trim());
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public int DepartmentId
        {
            get { User loggedInUser = Utilities.Membership.GetCurrentLoggedInUser();
                return loggedInUser.DepartmentID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                DataBindDisbursementGridView();
            }
        }

        protected void DataBindDisbursementGridView()
        {
            using(BLL.DisbursementManager dm = new DisbursementManager())
            {
                DisbursementSearchDTO criteria = new DisbursementSearchDTO();
                criteria.StationeryRetrievalFormID = this.stationeryRetrievalFormId;
                criteria.DepartmentID = this.DepartmentId;

                this.DisbursementGridView.DataSource =
                    dm.FindDisbursementByCriteria(criteria);
                this.DisbursementGridView.DataBind();
            }
        }
    }
}