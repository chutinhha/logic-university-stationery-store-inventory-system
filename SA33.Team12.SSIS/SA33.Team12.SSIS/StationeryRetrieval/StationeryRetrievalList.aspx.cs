using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.StationeryRetrieval
{
    public partial class StationeryRetrievalList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBindStationeryRetrievalFormGridView();
            }
        }

        protected void DataBindStationeryRetrievalFormGridView()
        {
            using (StationeryRetrievalManager srm = new StationeryRetrievalManager())
            {
                this.StationeryRetrievalFormGridView.DataSource = srm.GetAllStationeryRetrievalForms();
                this.StationeryRetrievalFormGridView.DataBind();
            }
        }

        protected void StationeryRetrievalFormGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int UserID = (int)DataBinder.Eval(e.Row.DataItem, "RetrievedBy");
            }
        }
    }
}