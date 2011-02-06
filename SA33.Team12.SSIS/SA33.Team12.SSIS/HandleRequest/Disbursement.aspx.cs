using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS;
using SA33.Team12.SSIS.DAL.DTO;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using System.Collections.Specialized;
using System.Diagnostics;

namespace SA33.Team12.SSIS.Handle_Request
{
    public partial class Disbursement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindSRFGridView();
                BindDisbursementGridView();
            }
        }

        protected void BindSRFGridView()
        {
            using (BLL.StationeryRetrievalManager srm = new BLL.StationeryRetrievalManager())
            {

                this.SRFGridView.DataSource = srm.GetAllStationeryRetrievalForms();
                this.SRFGridView.DataBind();
            }
        }

        protected void SRFGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SRFGridView.PageIndex = e.NewPageIndex;
            BindSRFGridView();
        }

        protected void SRFGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int UserID = (int)DataBinder.Eval(e.Row.DataItem, "RetrievedBy");
                if (UserID != 0)
                {
                    Literal aa = e.Row.FindControl("RetrievedByLiteral") as Literal;
                    if (aa != null)
                    {
                        using (UserManager um = new UserManager())
                        {
                            User user = um.GetUserByID(UserID);
                            if (user != null) aa.Text = user.UserName;
                        }
                    }
                }
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            string selectedItem = DDLIsDisbursed.SelectedValue.ToString();
            bool status = true;
            if (selectedItem == "false") {
                status = false;
            }
            StationeryRetrievalFormSearchDTO searchCriteria = new StationeryRetrievalFormSearchDTO();
            searchCriteria.IsDistributed = status;
            using (BLL.StationeryRetrievalManager srm = new BLL.StationeryRetrievalManager())
            {
                List<StationeryRetrievalForm> SRF = srm.FindStationeryRetrievalFormByCriteria(searchCriteria);
                SRFGridView.DataSource = SRF;
                SRFGridView.DataBind();
            }
        }

        protected void SRFGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowId = int.Parse(e.CommandArgument.ToString());
            int SRFID = (int)SRFGridView.DataKeys[rowId].Value;
            Label2.Text = SRFID.ToString();

        }

        protected void BindDisbursementGridView()
        {
            using (BLL.DisbursementManager dm = new BLL.DisbursementManager())
            {

                this.DisbursementGridView.DataSource = dm.FindAllDisbursement();
                this.DisbursementGridView.DataBind();
            }
        }

        protected void DisbursementGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DisbursementGridView.PageIndex = e.NewPageIndex;
            BindDisbursementGridView();
        }

        protected void DisbursementGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int UserID = (int)DataBinder.Eval(e.Row.DataItem, "CreatedBy");
                if (UserID != 0)
                {
                    Literal aa = e.Row.FindControl("CreatedByLiteral") as Literal;
                    if (aa != null)
                    {
                        using (UserManager um = new UserManager())
                        {
                            User user = um.GetUserByID(UserID);
                            if (user != null) aa.Text = user.UserName;
                        }
                    }
                }
            }
        }

        protected void DisbursementGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "select":
                    int rowId = int.Parse(e.CommandArgument.ToString());
                    int disbursementID = (int)DisbursementGridView.DataKeys[rowId].Value;

                    using (DisbursementManager dm = new DisbursementManager())
                    {
                        DAL.Disbursement disbursement = dm.FindDisbursementByID(disbursementID);
                        List<DAL.Disbursement> disbursements = new List<DAL.Disbursement>();
                        disbursements.Add(disbursement);
                        List<DAL.DisbursementItem> disbursementItems =
                            dm.FindDisbursementItemByCriteria(new DisbursementItemSearchDTO() { DisbursementID = disbursementID });
                        this.DisbursementItemGridView.DataSource = disbursementItems;
                        DisbursementItemGridView.DataBind();
                    }
                    break;
                   
            }
        }

        /*protected void DisbursementItemGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "select":
                    int rowId = int.Parse(e.CommandArgument.ToString());
                    int disbursementItemID = (int)DisbursementItemGridView.DataKeys[rowId].Value;
                    Label1.Text = disbursementItemID.ToString();
                    break;
                case "edit":
                    int rowId1 = int.Parse(e.CommandArgument.ToString());
                    int ItemID = (int)DisbursementItemGridView.DataKeys[rowId1].Value;
                    int Quantity = Convert.ToInt32(tbxQuantity.Text.ToString());
                    using (BLL.DisbursementManager dm = new DisbursementManager())
                    {
                       DAL.DisbursementItem Item=dm.FindDisbursementItemByID(ItemID);
                       DAL.DisbursementItem newItem = new DisbursementItem();
                       newItem.QuantityDisbursed = Quantity;
                       DAL.DisbursementItem UpdatedItem = dm.UpdateDisbursementItem(newItem);
                       List<DAL.DisbursementItem> Items = new List<DisbursementItem>();
                       Items.Add(UpdatedItem);
                       DisbursementItemGridView.DataSource = Items;
                       DisbursementItemGridView.DataBind();
                    }
                    break;
            }
        }*/

        protected void BtnUpdateQuantity_Click(object sender, EventArgs e)
        {
            int ItemID =Convert.ToInt32(Label1.Text.ToString());
            int Quantity = Convert.ToInt32(tbxQuantity.Text.ToString());
            using (BLL.DisbursementManager dm = new DisbursementManager())
            {
                DAL.DisbursementItem Item=dm.FindDisbursementItemByID(ItemID);
                DAL.DisbursementItem newItem = new DisbursementItem();
                newItem.QuantityDisbursed = Quantity;
                DAL.DisbursementItem UpdatedItem = dm.UpdateDisbursementItem(newItem);
                List<DAL.DisbursementItem> Items = new List<DisbursementItem>();
                Items.Add(UpdatedItem);
                DisbursementItemGridView.DataSource = Items;
                DisbursementItemGridView.DataBind();
            }
        }

        protected void DisbursementItemGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DisbursementItemGridView.EditIndex = e.NewEditIndex;
            DisbursementItemGridView.DataBind();
        }

        protected void DisbursementItemGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ItemID = Convert.ToInt32(((Label)DisbursementItemGridView.Rows[e.RowIndex].FindControl("ItemIDLabel")).Text);
            int Quantity = Convert.ToInt32(((TextBox)DisbursementItemGridView.Rows[e.RowIndex].FindControl("QuantityDisbursedtxb")).Text);
            using (BLL.DisbursementManager dm = new DisbursementManager())
            {
                DAL.DisbursementItem Item = dm.FindDisbursementItemByID(ItemID);
                DAL.DisbursementItem newItem = new DisbursementItem();
                newItem.QuantityDisbursed = Quantity;
                DAL.DisbursementItem UpdatedItem = dm.UpdateDisbursementItem(newItem);
            }
            DisbursementItemGridView.EditIndex = -1;
            DisbursementItemGridView.DataBind();
        }

        protected void DisbursementItemGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DisbursementItemGridView.EditIndex = -1;
            DisbursementItemGridView.DataBind();
        }

        protected void DisbursementItemGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int StationeryID = (int)DataBinder.Eval(e.Row.DataItem, "StationeryID");
                if (StationeryID != 0)
                {
                    Literal aa = e.Row.FindControl("StationeryIDLiteral") as Literal;
                    if (aa != null)
                    {
                        StationeryManager sm = new StationeryManager();
                        DAL.Stationery stationery = sm.FindStationeryByID(StationeryID);
                        if (stationery != null)
                        {
                            int categoryID = stationery.CategoryID;
                            CatalogManager cm = new CatalogManager();
                            DAL.Category category = cm.GetCategoryByID(categoryID);
                            if (category != null)
                            {
                                aa.Text = category.Name.ToString();
                            }
                        }
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SpecialStationeryID = (int)DataBinder.Eval(e.Row.DataItem, "SpecialStationeryID");
                if (SpecialStationeryID != 0)
                {
                    Literal aa = e.Row.FindControl("SpecialStationeryIDLiteral") as Literal;
                    if (aa != null)
                    {
                        CatalogManager cm=new CatalogManager();
                        DAL.SpecialStationery SpeStationery = cm.GetSpecialStationeryByID(SpecialStationeryID);
                        if (SpeStationery != null)
                        {
                            int categoryID = SpeStationery.CategoryID;
                            DAL.Category category = cm.GetCategoryByID(categoryID);
                            if (category != null)
                            {
                                aa.Text = category.Name.ToString();
                            }
                        }
                    }
                }
            }
            
        }

       



       

        

       
       

     


    }
}