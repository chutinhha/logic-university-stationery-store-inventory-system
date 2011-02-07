﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.BLL;
using System.Collections.Specialized;
using System.Diagnostics;
using SA33.Team12.SSIS.Exceptions;

namespace SA33.Team12.SSIS.Test
{
    public partial class StationeryRequest : System.Web.UI.Page
    {
        private RequisitionManager requisitionManager;
        
        private Requisition requisition;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            CancelButton.Visible = false;
            requisitionManager = new RequisitionManager();

            if (!IsPostBack)
            {
               UrgencyDDL.DataSource = requisitionManager.GetAllUrgencies();
               UrgencyDDL.DataTextField = "Name";
               UrgencyDDL.DataValueField = "UrgencyID";
               DataBind();
            }

            if (Session["Requisition"] != null)
            {
                requisition = (Requisition)Session["Requisition"];
            }
            else
            {
                requisition = CreateRequisition();
                Session["Requisition"] = requisition;
            }



            NameValueCollection nv = Request.QueryString;
            string key = string.Empty;
            string val = string.Empty;
            if (nv.HasKeys())
            {
                key = nv.GetKey(0);
                val = nv.Get(0);
            }            

            if (key == "RequestID")
            {                
                requisition = requisitionManager.GetRequisitionByID(Convert.ToInt32(val));
                Panel1.Visible = false;
                Panel2.Visible = false;
                SubmitButton.Visible = false;
                Panel3.Visible = false;
                CancelButton.Visible = true;
                if (requisition != null)
                {
                    if (requisition.ApprovedBy > 0)
                    {
                        RequestItemGridView.AutoGenerateEditButton = false;
                        RequestItemGridView.AutoGenerateDeleteButton = false;
                        
                    }
                }
                else
                {
                    Response.Redirect("~/RequestStationery/StationeryRequest.aspx");
                }
            }

            if (requisition.RequisitionID >= 0 && requisition != null)
            {
                PopulateData(requisition);
            }

        }

        private Requisition CreateRequisition()
        {
            Requisition req = new Requisition();
            User currentUser = Utilities.Membership.GetCurrentLoggedInUser();            
            req.DepartmentID = currentUser.DepartmentID;
            req.DateRequested = DateTime.Now.Date;            
            req.CreatedBy = currentUser.UserID;
            req.RequisitionForm = requisitionManager.GetRequisitionNumber(req);

            return req;
        }

        protected void PopulateData(Requisition requisition)
        {
            List<RequisitionItem> reqItems = null;
            List<SpecialRequisitionItem> splReqItems = null;
            if (requisition.RequisitionID > 0)
            {
                reqItems = requisitionManager.GetAllRequisitionItems(requisition);
                splReqItems = requisitionManager.GetAllSpecialRequisitionItems(requisition);

                if (reqItems.Count > 0)
                {
                    RequestItemGridView.DataSource = reqItems;
                    RequestItemGridView.DataKeyNames = new string[] { "RequisitionItemID" };

                }

                if (splReqItems.Count > 0)
                {
                    SpecialRequestItemGridView.DataSource = splReqItems;
                    SpecialRequestItemGridView.DataKeyNames = new string[] { "SpecialStationeryID" };

                }
                
                DetailsView1.DataSource = reqItems;
                DetailsView2.DataSource = splReqItems;
                DataBind();
            }

            if (requisition.RequisitionID == 0)
            {
                reqItems = requisition.RequisitionItems.ToList<RequisitionItem>();
                splReqItems = requisition.SpecialRequisitionItems.ToList<SpecialRequisitionItem>();

                if (reqItems.Count > 0)
                {
                    RequestItemGridView.DataSource = reqItems;
                    RequestItemGridView.DataKeyNames = new string[] { "RequisitionItemID" };

                }

                if (splReqItems.Count > 0)
                {
                    SpecialRequestItemGridView.DataSource = splReqItems;

                }
                DetailsView1.DataSource = reqItems;
            }
        }

        protected void RequestItemGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RequestItemGridView.EditIndex = e.NewEditIndex;
            RequestItemGridView.DataBind();
        }

        protected void RequestItemGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            RequisitionItem reqItem = requisitionManager.GetRequisitionItemsByID(Convert.ToInt32(((TextBox)RequestItemGridView.Rows[e.RowIndex].FindControl("TextBox2")).Text));
            GridViewRow row = RequestItemGridView.Rows[e.RowIndex];
            DropDownList t = (DropDownList)row.FindControl("DropDownList1");
            if (reqItem != null)
            {
                reqItem.StationeryID = Convert.ToInt32(t.Text);
                reqItem.QuantityRequested = Convert.ToInt32(((TextBox)row.FindControl("TextBox2")).Text);
                requisitionManager.UpdateRequisitionItem(reqItem);
                //UtilityFunctions.SendEmail("Update requisition successful.", requisition.RequisitionID + " - Requestion has been updated successfully.", requisition.CreatedByUser);
            }
            else
            {
                if (Session["Requisition"] != null)
                {
                    requisition = (Requisition)Session["Requisition"];
                }
                else
                {
                    requisition = CreateRequisition();
                    Session["Requisition"] = requisition;
                }
                foreach (var req in ((Requisition)Session["Requisition"]).RequisitionItems)
                {
                    if (reqItem.StationeryID == req.StationeryID)
                    {
                        reqItem.QuantityRequested += req.QuantityIssued;
                        requisition.RequisitionItems.Remove(req);
                        requisition.RequisitionItems.Add(reqItem);                      
                        break;
                    }
                }
            }
            RequestItemGridView.EditIndex = -1;
            PopulateData(requisition);
            RequestItemGridView.DataBind();
        }

        protected void RequestItemGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            RequestItemGridView.EditIndex = -1;
            RequestItemGridView.DataBind();
        }

        protected void RequestItemGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            RequisitionItem reqItem = null;
            Label t = null;
            GridViewRow row = null;
            if (e.RowIndex != 0)
            {
                row = RequestItemGridView.Rows[e.RowIndex];
                t = (Label)row.FindControl("Label3");
            }
            if (t != null)
            {
                reqItem = requisitionManager.GetRequisitionItemsByID(Convert.ToInt32(t.Text));
            }
            if (reqItem != null)
            {
                requisitionManager.DeleteRequisitionItem(requisitionManager.GetRequisitionItemsByID(reqItem.RequisitionItemID));                   
            }
            else
            {
                foreach (var req in ((Requisition)Session["Requisition"]).RequisitionItems)
                {
                    if (Convert.ToInt32(t.Text) == req.RequisitionItemID)
                    {
                        requisitionManager.DeleteRequisitionItem(reqItem);
                    }
                }
            }

            PopulateData(requisition);
            DataBind();
        }

        protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            RequisitionItem item = new RequisitionItem();

            item.StationeryID = Convert.ToInt32(((DropDownList)DetailsView1.FindControl("DropDownList3")).SelectedValue);
            item.QuantityIssued = 0;
            item.Price = 0;
            int qty = 0;
            try
            {
                if (((TextBox)DetailsView1.FindControl("TextBox5")).Text != string.Empty)
                {
                    qty = Convert.ToInt32(((TextBox)DetailsView1.FindControl("TextBox5")).Text);
                }
                else
                {
                    qty = 1;
                }
            }
            catch (Exception)
            {
                ((RangeValidator)DetailsView1.FindControl("RangeValidator1")).ErrorMessage = "Enter a valid number";
            }
            if (qty > 0)
            {
                item.QuantityRequested = qty;
            }
            

            if (Session["Requisition"] != null)
            {
                requisition = (Requisition)Session["Requisition"];
            }
            else
            {
                requisition = CreateRequisition();
                Session["Requisition"] = requisition;
            }

            if (requisition.RequisitionItems.Count < 1)
            {
                requisition.RequisitionItems.Add(item);
            }

            else
            {
                foreach (var req in requisition.RequisitionItems)
                {
                    if (item.StationeryID == req.StationeryID)
                    {
                        item.QuantityRequested += req.QuantityRequested;
                        requisition.RequisitionItems.Remove(req);
                        requisition.RequisitionItems.Add(item);
                        break;
                    }
                    else
                    {
                        requisition.RequisitionItems.Add(item);
                        break;
                    }
                }
            }

            PopulateData(requisition);
            DataBind();

        }

        protected void DetailsView2_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            SpecialRequisitionItem item = new SpecialRequisitionItem();
            item.QuantityIssued = 0;
            item.Price = 0;
            int qty = 0;

            if (((TextBox)DetailsView2.FindControl("TextBox1")).Text != string.Empty)
            {
                item.Name = ((TextBox)DetailsView2.FindControl("TextBox1")).Text;
            }
            else
            {
                ((RequiredFieldValidator)DetailsView2.FindControl("RequiredFieldValidator2")).ErrorMessage = "Name required.";
            }

            if (((TextBox)DetailsView2.FindControl("TextBox2")).Text != string.Empty)
            {
                item.Description = ((TextBox)DetailsView2.FindControl("TextBox2")).Text;
            }
          
            try
            {
                if (((TextBox)DetailsView2.FindControl("TextBox3")).Text != string.Empty)
                {
                    qty = Convert.ToInt32(((TextBox)DetailsView2.FindControl("TextBox3")).Text);
                }
                else
                {
                    qty = 1;
                }
            }
            catch (Exception)
            {
                ((RangeValidator)DetailsView2.FindControl("RangeValidator2")).ErrorMessage = "Enter a valid number";
            }
            if (qty > 0)
            {
                item.QuantityRequested = qty;
            }

            if (((TextBox)DetailsView2.FindControl("TextBox4")).Text != string.Empty)
            {
                item.RemarkByRequester = ((TextBox)DetailsView2.FindControl("TextBox4")).Text;
            }

            if (Session["Requisition"] != null)
            {
                requisition = (Requisition)Session["Requisition"];
            }
            else
            {
                requisition = CreateRequisition();
                Session["Requisition"] = requisition;
            }

            if (item.Name != null && item.Description != null && item.RemarkByRequester != null)
            {
                requisition.SpecialRequisitionItems.Add(item);
            }
            else
            {
                ((RangeValidator)DetailsView2.FindControl("RangeValidator2")).ErrorMessage = "All Fields are mandatory";
            }

            PopulateData(requisition);
            DataBind();
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                requisition.UrgencyID = Convert.ToInt32(UrgencyDDL.SelectedValue);
                Requisition temp = requisitionManager.CreateRequisition(requisition);
               // UtilityFunctions.SendEmail(temp.RequisitionID + " - Requisition Created Successfully ", "The requisition has been created successfully. You can view the status of requisition from the below link.<br /> <a href=>", temp.CreatedByUser);
                
               // Response.Redirect( Server.MapPath("~/RequestStationery/StationeryRequest.aspx?RequestID=" + temp.RequisitionID));
                Session["Requisition"] = null;

                requisition = null;
                
            }
            catch (Exception)
            {
                throw new RequisitionException("Error occured. Please try again.");
            }
            
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                requisitionManager.CancelRequisition(requisition);
                CancelButton.Visible = false;
                RequestItemGridView.AutoGenerateDeleteButton = false;
                RequestItemGridView.AutoGenerateEditButton = false;
                UtilityFunctions.SendEmail("Cancelled Requisition - " + requisition.RequisitionID, "Your request has been cancelled successfully", requisition.CreatedByUser);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {

        }

        protected void DetailsView2_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {

        }
    }
}