using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.StationeryRetrieval
{
    public partial class StationeryRetrievalForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void StationeryRetrievalFormView_DataBound(object sender, EventArgs e)
        {
            if(this.StationeryRetrievalFormView.CurrentMode == FormViewMode.ReadOnly)
            {

                GridView StationeryRetrievalFormItemGridView =
    StationeryRetrievalFormView.FindControl("StationeryRetrievalFormItemGridView") as GridView;

                EntityCollection<StationeryRetrievalFormItem> items =
                   DataBinder.Eval(this.StationeryRetrievalFormView.DataItem, "StationeryRetrievalFormItems") as EntityCollection<StationeryRetrievalFormItem>;

                StationeryRetrievalFormItemGridView.DataSource = items;
                StationeryRetrievalFormItemGridView.DataBind();
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {

        }

        protected void UpdateStationeryRetrievalFormView()
        {
            GridView StationeryRetrievalFormItemGridView =
                StationeryRetrievalFormView.FindControl("StationeryRetrievalFormItemGridView") as GridView;

            if (StationeryRetrievalFormItemGridView != null)
            {
                int srfID = Convert.ToInt32(this.StationeryRetrievalFormView.DataKey.Value);
                using(StationeryRetrievalManager srm = new StationeryRetrievalManager())
                {
                    DAL.StationeryRetrievalForm srf = srm.GetStationeryRetrievalFormByID(srfID);
                    List<StationeryRetrievalFormItem> srfis = srf.StationeryRetrievalFormItems.ToList();
                    foreach (GridViewRow row in StationeryRetrievalFormItemGridView.Rows)
                    {
                        HiddenField StationeryRetrievalFormItemIDHiddenField =
                            row.FindControl("StationeryRetrievalFormItemIDHiddenField") as HiddenField;
                        int srfiID = Convert.ToInt32(StationeryRetrievalFormItemIDHiddenField.Value);
                        TextBox QtyRetrieved = row.FindControl("QuantityRetrievedTextBox") as TextBox;
                        StationeryRetrievalFormItem srfi = (from s in srfis
                                                            where s.StationeryRetrievalFormItemID == srfiID
                                                            select s).FirstOrDefault();
                        srfi.QuantityRetrieved = Convert.ToInt32(QtyRetrieved.Text);
                    }
                    srm.UpdateReceivedQuantity(srf);
                }


            }

        }
    }
}