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

namespace SA33.Team12.SSIS.Handle_Request
{
    public partial class Disbursement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindSRFGridView();
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


        protected void DisbursementGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int UserID = (int) DataBinder.Eval(e.Row.DataItem, "CreatedBy");
                if (UserID != 0)
                {
                    using (BLL.UserManager um = new BLL.UserManager())
                    {
                        DAL.User user = um.GetUserByID(UserID);
                        Label UserNameLabel = e.Row.FindControl("UserNameLabel") as Label;
                        if (UserNameLabel != null) UserNameLabel.Text = user.UserName;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                int SRFID = (int)DataBinder.Eval(e.Row.DataItem, "StationeryRetrievalFormID");
                if (SRFID != 0)
                {
                    using (BLL.StationeryRetrievalManager srm = new BLL.StationeryRetrievalManager())
                    {
                        DAL.StationeryRetrievalForm SRF = srm.GetStationeryRetrievalFormByID(SRFID);
                        Label IsdisbursedLabel = e.Row.FindControl("IsdisbursedLabel") as Label;
                        if (IsdisbursedLabel != null) IsdisbursedLabel.Text = SRF.IsDistributed.ToString();
                    }
                }
                if (SRFID == 0)
                {
                    Label IsdisbursedLabel = e.Row.FindControl("IsdisbursedLabel") as Label;
                    IsdisbursedLabel.Text = "no SRF";
                }
            }
        }

        

       
       

     


    }
}