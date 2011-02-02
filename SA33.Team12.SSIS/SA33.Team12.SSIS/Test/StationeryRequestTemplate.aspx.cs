using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.BLL;
using System.Collections.Specialized;

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

            if (requisition.RequisitionID != 0 && requisition != null)
            {
                PopulateData(requisition);
            }
            else
            {
                Response.Write("Requested item not found");
            }
        }

        private Requisition CreateRequisition()
        {
            Requisition req = new Requisition();
            req.DepartmentID = 1;
            req.DateRequested = DateTime.Now.Date;
            req.UrgencyID = 1;
            req.RequisitionForm = requisitionManager.GetRequisitionNumber(req);

            return req;
        }

        protected void PopulateData(Requisition requisition)
        {
            if (requisition.RequisitionID != 0)
            {
                List<RequisitionItem> reqItems = requisitionManager.GetAllRequisitionItems(requisition);
                List<SpecialRequisitionItem> splReqItems = requisitionManager.GetAllSpecialRequisitionItems(requisition);

                if (reqItems.Count > 0)
                {
                    RequestItemGridView.DataSource = reqItems;
                    RequestItemGridView.DataKeyNames = new string[] { "RequisitionItemID" };
                }

                if (splReqItems.Count > 0)
                {
                    SpecialRequestItemGridView.DataSource = splReqItems;
                }

            }

            DataBind();
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

            
            if (reqItem != null)
            {
                reqItem.StationeryID = Convert.ToInt32(((TextBox)row.FindControl("TextBox1")).Text);
                reqItem.QuantityRequested = Convert.ToInt32(((TextBox)row.FindControl("TextBox2")).Text);
                
            }
            requisitionManager.UpdateRequisitionItem(reqItem);
            RequestItemGridView.EditIndex = -1;
            RequestItemGridView.DataBind();
        }

        protected void RequestItemGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            RequestItemGridView.EditIndex = -1;
            RequestItemGridView.DataBind();
        }
    }
}