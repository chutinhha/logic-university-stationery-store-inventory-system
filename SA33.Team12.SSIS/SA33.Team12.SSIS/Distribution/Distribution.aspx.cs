using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

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

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    for (int i = 0; i < this.DistributionGridView.Rows.Count; i++)
                    {
                        GridViewRow gridViewRow = DistributionGridView.Rows[i];
                        HiddenField IsSpecialHiddenField = gridViewRow.FindControl("IsSpecialHiddenField") as HiddenField;
                        HiddenField RequisitionIDHiddenField = gridViewRow.FindControl("RequisitionIDHiddenField") as HiddenField;
                        HiddenField StationeryIDHiddenField = gridViewRow.FindControl("StationeryIDHiddenField") as HiddenField;
                        HiddenField SpecialStationeryIDHiddenField =
                            gridViewRow.FindControl("SpecialStationeryIDHiddenField") as HiddenField;

                        HiddenField QuantityDisbursedHiddenField = gridViewRow.FindControl("QuantityDisbursedHiddenField") as HiddenField;
                        TextBox QuantityTextBox = gridViewRow.FindControl("QuantityTextBox") as TextBox;

                        bool isSpecial = Convert.ToBoolean(IsSpecialHiddenField.Value);
                        int RequisitionID = Convert.ToInt32(RequisitionIDHiddenField.Value);
                        int StationeryID = Convert.ToInt32(StationeryIDHiddenField.Value);
                        int SpecialStationeryID = Convert.ToInt32(SpecialStationeryIDHiddenField.Value);
                        int QuantityDisbursed = Convert.ToInt32(QuantityDisbursedHiddenField.Value);
                        int QuantityDistributed = Convert.ToInt32(QuantityTextBox.Text.Trim());

                        using (RequisitionManager rm = new RequisitionManager())
                        {
                            Requisition rq = rm.GetRequisitionByID(RequisitionID);

                            if (!isSpecial)
                            {
                                List<RequisitionItem> rqItems = (from item in rq.RequisitionItems
                                                                 where item.StationeryID == StationeryID
                                                                 select item).ToList();
                                for (int j = 0; j < rqItems.Count && QuantityDisbursed > 0; j++)
                                {
                                    RequisitionItem rqItem = rqItems[j];
                                    if (QuantityDisbursed > rqItem.QuantityRequested)
                                    {
                                        rqItem.QuantityIssued = rqItem.QuantityRequested;
                                        QuantityDisbursed = QuantityDisbursed - rqItem.QuantityRequested;
                                    }
                                    else if (QuantityDisbursed > 0)
                                    {
                                        rqItem.QuantityIssued = QuantityDisbursed;
                                        QuantityDisbursed = 0;
                                    }
                                }

                            }
                            else
                            {
                                List<SpecialRequisitionItem> srqItems = (from sitem in rq.SpecialRequisitionItems
                                                                         where sitem.SpecialStationeryID == SpecialStationeryID
                                                                         select sitem).ToList();
                                for (int j = 0; j < srqItems.Count && QuantityDisbursed > 0; j++)
                                {
                                    SpecialRequisitionItem srqItem = srqItems[j];
                                    if (QuantityDisbursed > srqItem.QuantityRequested)
                                    {
                                        srqItem.QuantityIssued = srqItem.QuantityRequested;
                                        QuantityDisbursed = QuantityDisbursed - srqItem.QuantityRequested;
                                    }
                                    else if (QuantityDisbursed > 0)
                                    {
                                        srqItem.QuantityIssued = QuantityDisbursed;
                                        QuantityDisbursed = 0;
                                    }
                                }
                            }
                            Status status = rm.GetStatusByName(new StatusSearchDTO() { Name = "Fulfilled" });
                            rq.Status = status;
                            rm.UpdateRequisition(rq);
                        }
                    }
                    using (DisbursementManager dm = new DisbursementManager())
                    {
                        Disbursement disbursement = dm.FindDisbursementByID(this.DisbursementId);
                        disbursement.IsDistributed = true;
                        dm.UpdateDisbursement(disbursement);
                    }
                    ts.Complete();
                }
            }
            catch (Exception exception)
            {
                this.ErrorMessage.Text = exception.Message;
            }

        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Distribution/Disbursements.aspx");
        }
    }
}