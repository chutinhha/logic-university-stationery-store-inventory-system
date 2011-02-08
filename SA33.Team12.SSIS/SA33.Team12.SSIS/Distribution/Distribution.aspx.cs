using System;
using System.Collections.Generic;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Distribution
{
    public partial class Distribution : System.Web.UI.Page
    {
        public int DisbursementId
        {
            get
            {
                try
                {
                    if (Request.QueryString["DisbursementID"] != null && Request.QueryString["DisbursementID"].ToString() != "")
                        return Convert.ToInt32(Request.QueryString["DisbursementID"].Trim());
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                DataBindDistributionGridView();
        }

        protected void DataBindDistributionGridView()
        {
            using (DisbursementManager dm = new DisbursementManager())
            {
                List<vw_GetStationeryDistributionList> distributionLists 
                    = dm.GetDistributionListByDisbursementID(this.DisbursementId);
                this.DistributionGridView.DataSource = distributionLists;
                this.DistributionGridView.DataBind();
                Utilities.Format.MergeRowBySameValue(this.DistributionGridView, 1);
            }
        }
    }
}