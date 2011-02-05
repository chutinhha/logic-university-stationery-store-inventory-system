﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Test
{
    public partial class ViewRequestHistory : System.Web.UI.Page
    {
        RequisitionManager reqManager;
        User currentUser;
        RequisitionSearchDTO reqSearchDTO = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            reqManager = new RequisitionManager();
            currentUser = Utilities.Membership.GetCurrentLoggedInUser();
            reqSearchDTO = new RequisitionSearchDTO();
            if (!IsPostBack)
            {
                for (int i = 2011; i > 1900; i--)
                {
                    YearDDL.Items.Add(i.ToString());
                }
                GridView1.DataSource = reqManager.GetAllRequisition(currentUser.UserID, null);
            }
            else
            {
                reqSearchDTO.ExactDateRequested = new DateTime(Convert.ToInt32(YearDDL.SelectedValue), Convert.ToInt32(MonthDDL.SelectedValue), 1);
                if (reqSearchDTO != null)
                {
                    GridView1.DataSource = reqManager.GetAllRequisition(currentUser.UserID, reqSearchDTO);
                    if (reqManager.GetAllRequisition(currentUser.UserID, reqSearchDTO).Count == 0)
                        ErrorLabel.Text = "No Record Found";
                }
                else
                {
                    GridView1.DataSource = reqManager.GetAllRequisition(currentUser.UserID, null);
                }
            }
            DataBind();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {

        }
    }
}