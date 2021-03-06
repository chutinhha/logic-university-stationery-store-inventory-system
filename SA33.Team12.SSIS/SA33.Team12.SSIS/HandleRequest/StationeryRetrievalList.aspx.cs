﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.StationeryRetrieval
{
    public partial class StationeryRetrievalList : AppCode.PageBase
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

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (StationeryRetrievalManager sm = new StationeryRetrievalManager())
                {
                    DAL.User loggedInUser = Utilities.Membership.GetCurrentLoggedInUser();
                    StationeryRetrievalForm srf
                        = sm.CreateStationeryRetrievalFormByAllRequisitions(loggedInUser);
                    Response.Redirect("~/HandleRequest/StationeryRetrievalForm.aspx?ID="
                        + srf.StationeryRetrievalFormID);
                }
            }
            catch (Exception exception)
            {
                this.ErrorMessage.Text = exception.Message;
            }
        }

        protected void StationeryRetrievalFormGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower().CompareTo("disburse") == 0)
            {
                try
                {
                    int stationeryRetrievalFormID = int.Parse(e.CommandArgument.ToString());
                    using(DisbursementManager dm = new DisbursementManager())
                    {
                        DAL.User loggedInUser = Utilities.Membership.GetCurrentLoggedInUser();
                        dm.CreateDisbursementBySRF(loggedInUser, stationeryRetrievalFormID);
                        Response.Redirect("~/HandleRequest/Disbursements.aspx?SRFId=" + stationeryRetrievalFormID);
                    }
                }
                catch (Exception exception)
                {
                    this.ErrorMessage.Text = exception.Message;
                }
            }
        }
    }
}