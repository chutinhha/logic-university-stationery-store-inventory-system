using System;
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
using System.Collections;

namespace SA33.Team12.SSIS.Test
{
    public partial class StationeryRequest : System.Web.UI.Page
    {
        private RequisitionManager requisitionManager;
        private Requisition requisition;

        protected void Page_Load(object sender, EventArgs e)
        {
            requisitionManager = new RequisitionManager();
            CancelButton.Visible = false;
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

            string key = string.Empty;
            int val = 0;
            NameValueCollection nv = Request.QueryString;
            if (nv.HasKeys())
            {
                key = nv.GetKey(0);
                try
                {
                    val = Convert.ToInt32(nv.Get(0));
                }
                catch (Exception)
                {

                }

            }
            if (key == "RequestID" && val > 0)
            {
                requisition = requisitionManager.GetRequisitionByID(val);

                if (requisition != null)
                {
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    RequestItemGridView.Columns[0].Visible = false;
                    RequestItemGridView.Columns[1].Visible = false;
                    SpecialRequestItemGridView.Columns[0].Visible = false;
                    SubmitButton.Visible = false;
                    CancelButton.Visible = true;
                    GridDataBind();
                }
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
            GridDataBind();
        }

        protected void RequestItemGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = RequestItemGridView.Rows[e.RowIndex];
            foreach (RequisitionItem temp in requisition.RequisitionItems)
            {
                if (temp.StationeryID == Convert.ToInt32(e.Keys["StationeryID"]))
                {

                    temp.QuantityRequested = Convert.ToInt32(((TextBox)row.FindControl("QtyTextBox")).Text);
                    temp.StationeryID = Convert.ToInt32(((DropDownList)row.FindControl("stationeryDDL")).SelectedValue);
                    break;
                }
            }

            if (requisition.RequisitionID > 0)
            {
                RequisitionItem item = requisitionManager.GetRequisitionItemsByID(Convert.ToInt32(e.Keys["RequisitionItemID"]));
                if (item != null)
                {
                    item.QuantityRequested = Convert.ToInt32(((TextBox)row.FindControl("QtyTextBox")).Text);
                    item.StationeryID = Convert.ToInt32(((DropDownList)row.FindControl("stationeryDDL")).SelectedValue);
                    requisitionManager.UpdateRequisitionItem(item);
                }
            }

            Session["Requisition"] = requisition;
            RequestItemGridView.EditIndex = -1;
            GridDataBind();
        }

        protected void RequestItemGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            RequestItemGridView.EditIndex = -1;
            GridDataBind();
        }

        protected void RequestItemGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            foreach (var item in requisition.RequisitionItems)
            {
                if (item.RequisitionItemID == Convert.ToInt32(e.Keys["RequisitionItemID"]))
                {
                    requisition.RequisitionItems.Remove(item);
                    break;
                }
            }
            GridDataBind();
        }

        protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            RequisitionItem item = new RequisitionItem();
            item.StationeryID = Convert.ToInt32(((DropDownList)DetailsView1.FindControl("stDDL")).SelectedValue);
            item.QuantityRequested = Convert.ToInt32(((TextBox)DetailsView1.FindControl("stTextBox")).Text);
            item.QuantityIssued = 0;
            item.Price = 0;

            if (requisition.RequisitionItems.Count == 0)
            {
                requisition.RequisitionItems.Add(item);
            }
            else
            {
                foreach (var req in requisition.RequisitionItems)
                {
                    if (item.StationeryID == req.StationeryID)
                    {
                        req.QuantityRequested += item.QuantityRequested;
                        break;
                    }
                    else
                    {
                        requisition.RequisitionItems.Add(item);
                        break;
                    }
                }

            }
            GridDataBind();
        }

        private void GridDataBind()
        {
            RequestItemGridView.DataSource = requisition.RequisitionItems;
            SpecialRequestItemGridView.DataSource = requisition.SpecialRequisitionItems;
            if (requisition.RequisitionID > 0)
            {
                RequestItemGridView.DataSource = requisitionManager.GetAllRequisitionItems(requisition);
                SpecialRequestItemGridView.DataSource = requisitionManager.GetAllSpecialRequisitionItems(requisition);
            }
            DataBind();
        }

        protected void DetailsView2_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            SpecialRequisitionItem splItem = new SpecialRequisitionItem();

            //splItem.SpecialStationeryID = 1;
            splItem.QuantityRequested = Convert.ToInt32(((TextBox)DetailsView2.FindControl("QtyNeededTextBox")).Text);
            splItem.QuantityIssued = 0;
            splItem.Name = ((TextBox)DetailsView2.FindControl("itemNameTextBox")).Text;
            splItem.Description = ((TextBox)DetailsView2.FindControl("DescriptionTextBox")).Text;
            splItem.RemarkByRequester = ((TextBox)DetailsView2.FindControl("ReasonTextBox")).Text;
            splItem.Price = 0;

            requisition.SpecialRequisitionItems.Add(splItem);
            GridDataBind();
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (requisition.RequisitionItems.Count > 0 || requisition.SpecialRequisitionItems.Count > 0)
                {
                    requisition.UrgencyID = Convert.ToInt32(UrgencyDDL.SelectedValue);
                    Requisition temp = requisitionManager.CreateRequisition(requisition);
                    // UtilityFunctions.SendEmail(temp.RequisitionID + " - Requisition Created Successfully ", "The requisition has been created successfully. You can view the status of requisition from the below link.<br /> <a href=>", temp.CreatedByUser);

                    if (temp != null)
                    {
                        Response.Redirect("~/RequestStationery/StationeryRequest.aspx?RequestID=" + temp.RequisitionID, false);
                    }

                    requisition = null;
                }
            }
            catch (Exception)
            {
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

                //UtilityFunctions.SendEmail("Cancelled Requisition - " + requisition.RequisitionID, "Your request has been cancelled successfully", requisition.CreatedByUser);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error Occuered");
            }
        }

        protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {

        }

        protected void DetailsView2_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {

        }

        protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {

        }

        protected void RequestItemGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void RequestItemGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void SpecialRequestItemGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            foreach (var item in requisition.SpecialRequisitionItems)
            {
                if (item.Name == e.Keys["Name"].ToString())
                {
                    requisition.SpecialRequisitionItems.Remove(item);
                    break;
                }
            }
            GridDataBind();
        }
    }
}