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
    public partial class UpdateStationeryRetrievalForm : System.Web.UI.Page
    {
        private bool isRetrieved = false;
        public bool IsRetrieved
        {
            get { return isRetrieved; }
        }

        private bool isCollected = false;
        public bool IsCollected
        {
            get { return isCollected; }
        }

        private int stationeryRetrievalFormID = 0;
        public int StationeryRetrievalFormID
        {
            get { return stationeryRetrievalFormID; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToInt32(Request.QueryString["ID"]) == 0)
                    Response.Redirect("~/HandleRequest/StationeryRetrievalList.aspx");
                if (Request.QueryString["ID"].Trim() == "")
                    Response.Redirect("~/HandleRequest/StationeryRetrievalList.aspx");
                DataBindStationeryRetrievalFormView();
            }
        }

        protected void DataBindStationeryRetrievalFormView()
        {
            stationeryRetrievalFormID = Convert.ToInt32(Request.QueryString["ID"]);
            using (StationeryRetrievalManager sm = new StationeryRetrievalManager())
            {
                List<StationeryRetrievalForm> srfs = new List<StationeryRetrievalForm>();
                StationeryRetrievalForm stationeryRetrievalForm
                    = sm.GetStationeryRetrievalFormByID(this.StationeryRetrievalFormID);

                isRetrieved = (bool)stationeryRetrievalForm.IsRetrieved;
                isCollected = (bool)stationeryRetrievalForm.IsCollected;

                this.UpdateRetrievedQuantityButton.Visible = (!IsRetrieved);
                this.UpdateActualQuantityButton.Visible = (IsRetrieved && !IsCollected);

                srfs.Add(stationeryRetrievalForm);
                this.StationeryRetrievalFormView.DataSource = srfs;
                this.StationeryRetrievalFormView.DataBind();
            }
        }

        protected void StationeryRetrievalFormView_DataBound(object sender, EventArgs e)
        {
            if (this.StationeryRetrievalFormView.CurrentMode == FormViewMode.ReadOnly)
            {

                if (!this.IsRetrieved)
                {
                    GridView StationeryRetrievalFormItemGridView =
                        StationeryRetrievalFormView.FindControl("StationeryRetrievalFormItemGridView") as GridView;
                    EntityCollection<StationeryRetrievalFormItem> items =
                       DataBinder.Eval(this.StationeryRetrievalFormView.DataItem, "StationeryRetrievalFormItems") as EntityCollection<StationeryRetrievalFormItem>;

                    StationeryRetrievalFormItemGridView.DataSource = items;
                    StationeryRetrievalFormItemGridView.DataBind();
                }
                else
                {
                    GridView StationeryRetrievalFormItemByDeptGridView =
                        StationeryRetrievalFormView.FindControl("StationeryRetrievalFormItemByDeptGridView") as GridView;
                    using (StationeryRetrievalManager sm = new StationeryRetrievalManager())
                    {
                        List<vw_GetStationeryRetrievalFormItemByDept> srfiByDept =
                            sm.GetVwStationeryRetrievalFormItemByDeptsByFormID(this.StationeryRetrievalFormID);
                        StationeryRetrievalFormItemByDeptGridView.DataSource = srfiByDept;
                        StationeryRetrievalFormItemByDeptGridView.DataBind();
                    }
                    Utilities.Format.MergeRowBySameValue(StationeryRetrievalFormItemByDeptGridView, 1);

                }
            }
        }

        protected void UpdateRetrievedQuantity()
        {
            GridView StationeryRetrievalFormItemGridView =
                StationeryRetrievalFormView.FindControl("StationeryRetrievalFormItemGridView") as GridView;

            if (StationeryRetrievalFormItemGridView != null)
            {
                int srfID = Convert.ToInt32(this.StationeryRetrievalFormView.DataKey.Value);
                using (StationeryRetrievalManager srm = new StationeryRetrievalManager())
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
                    StationeryRetrievalForm newSRF = srm.UpdateReceivedQuantity(srf);
                    newSRF = srm.SetRecommendedQuantity(newSRF.StationeryRetrievalFormID);
                }


            }

        }

        protected void UpdateActualQuantity()
        {
            GridView StationeryRetrievalFormItemByDeptGridView =
                StationeryRetrievalFormView.FindControl("StationeryRetrievalFormItemByDeptGridView") as GridView;

            if (StationeryRetrievalFormItemByDeptGridView != null)
            {
                int srfID = Convert.ToInt32(this.StationeryRetrievalFormView.DataKey.Value);
                using (StationeryRetrievalManager srm = new StationeryRetrievalManager())
                {
                    StationeryRetrievalForm srf = srm.GetStationeryRetrievalFormByID(srfID);
                    List<StationeryRetrievalFormItem> srfis = srf.StationeryRetrievalFormItems.ToList();
                    List<StationeryRetrievalFormItemByDept> srfiByDepts
                        = new List<StationeryRetrievalFormItemByDept>();

                    foreach (StationeryRetrievalFormItem srfi in srfis)
                    {
                        srfiByDepts.AddRange(srfi.StationeryRetrievalFormItemByDepts);
                    }

                    foreach (GridViewRow row in StationeryRetrievalFormItemByDeptGridView.Rows)
                    {
                        HiddenField srfByDeptIDHiddenField =
                            row.FindControl("srfByDeptIDHiddenField") as HiddenField;
                        int srfiByDeptID = Convert.ToInt32(srfByDeptIDHiddenField.Value);

                        TextBox ActualQty = row.FindControl("QuantityActualTextBox") as TextBox;

                        StationeryRetrievalFormItemByDept srfiByDept
                            = (from s in srfiByDepts
                               where
                                   s.StationeryRetrievalFormItemByDeptID ==
                                   srfiByDeptID
                               select s).FirstOrDefault();
                        int qty = Convert.ToInt32(ActualQty.Text.Trim() == "" ? "0" : ActualQty.Text.Trim());
                        srfiByDept.QuantityActual = qty;
                    }
                    StationeryRetrievalForm newSRF = srm.UpdateActualQuantity(srf);
                }


            }

        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HandleRequest/StationeryRetrievalList.aspx");
        }


        protected void UpdateButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "retrieved")
                UpdateRetrievedQuantity();
            else if (e.CommandName.ToLower() == "actual")
                UpdateActualQuantity();

            DataBindStationeryRetrievalFormView();
        }
    }
}