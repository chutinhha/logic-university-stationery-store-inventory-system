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

namespace SA33.Team12.SSIS.Test
{
    public partial class StationeryRequestTemplate : System.Web.UI.Page
    {
        private RequisitionManager requisitionManager;
        private Requisition requisition;
        protected void Page_Load(object sender, EventArgs e)
        {
            requisitionManager = new RequisitionManager();

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
            }


            if (requisition.RequisitionID >= 0 && requisition != null)
            {
                PopulateData(requisition);
            }

            if (requisition.ApprovedBy > 0)
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
            }
        }

        private Requisition CreateRequisition()
        {
            Requisition req = new Requisition();
            req.RequisitionID = 0;
            req.DepartmentID = 1;
            req.DateRequested = DateTime.Now.Date;
            req.UrgencyID = 1;
            req.CreatedBy = 1;
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

            RequisitionItem reqItem = requisitionManager.GetRequisitionItemsByID(Convert.ToInt32(((TextBox)RequestItemGridView.Rows[e.RowIndex].FindControl("TextBox3")).Text));
            GridViewRow row = RequestItemGridView.Rows[e.RowIndex];
            DropDownList t = (DropDownList)row.FindControl("DropDownList1");
            if (reqItem != null)
            {
                reqItem.StationeryID = Convert.ToInt32(t.Text);
                reqItem.QuantityRequested = Convert.ToInt32(((TextBox)row.FindControl("TextBox2")).Text);
                requisitionManager.UpdateRequisitionItem(reqItem);
            }
            else
            {
                foreach (var req in requisition.RequisitionItems)
                {
                    if (reqItem.StationeryID == req.StationeryID)
                    {
                        reqItem.QuantityRequested = reqItem.QuantityRequested;
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
            if (Session["Requisition"] != null)
            {
                requisition = (Requisition)Session["Requisition"];
            }
            else
            {
                requisition = CreateRequisition();
                Session["Requisition"] = requisition;
            }
            RequestItemGridView.DataKeyNames = new string[] { "StationeryID" };
            //   Label id = (Label)RequestItemGridView.Rows[e.RowIndex].FindControl("Label1");
            foreach (var req in requisition.RequisitionItems)
            {
                //if (req.StationeryID == Convert.ToInt32(id))
                //{                    
                //    requisition.RequisitionItems.Remove(req);                    
                //    break;
                //}               
            }

            PopulateData(requisition);
            DataBind();
        }

        protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            RequisitionItem item = new RequisitionItem();

            item.StationeryID = Convert.ToInt32(((DropDownList)DetailsView1.FindControl("DropDownList3")).SelectedValue);
            if (((TextBox)DetailsView1.FindControl("TextBox5")).Text != string.Empty)
            {
                item.QuantityRequested = Convert.ToInt32(((TextBox)DetailsView1.FindControl("TextBox5")).Text);
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

            if (((TextBox)DetailsView2.FindControl("TextBox1")).Text != string.Empty)
            {
                item.Name = ((TextBox)DetailsView2.FindControl("TextBox1")).Text;
            }

            if (((TextBox)DetailsView2.FindControl("TextBox2")).Text != string.Empty)
            {
                item.Description = ((TextBox)DetailsView2.FindControl("TextBox2")).Text;
            }

            if (((TextBox)DetailsView2.FindControl("TextBox3")).Text != string.Empty)
            {
                item.QuantityRequested = Convert.ToInt32(((TextBox)DetailsView2.FindControl("TextBox3")).Text);
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

            requisition.SpecialRequisitionItems.Add(item);

            PopulateData(requisition);
            DataBind();
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                CatalogManager cManager = new CatalogManager();
                foreach (var item in requisition.SpecialRequisitionItems)
                {
                    SpecialStationery temp = cManager.CreateSpecialStationery(new SpecialStationery()
                    {
                        ItemCode = "s001",
                      Description = item.Name,
                      Quantity = item.QuantityRequested,
                      CreatedBy = 1,
                      ModifiedBy = 1,
                      CategoryID = 1,
                      DateCreated = DateTime.Now.Date,
                      UnitOfMeasure = "box",
                      IsApproved = false
                    }
                );

                    //item.SpecialStationeryID = temp.SpecialStationeryID;
                }


                requisitionManager.CreateRequisition(requisition);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

    }
}