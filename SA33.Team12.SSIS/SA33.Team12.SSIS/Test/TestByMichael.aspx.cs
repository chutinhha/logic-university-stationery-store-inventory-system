using System;
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
    public partial class TestByMichael : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDisbursementGridView();
            }
        }


        protected void BindDisbursementGridView()
        {
            using (BLL.DisbursementManager dm = new DisbursementManager())
            {
                
                this.DisbursementGridView.DataSource = dm.FindAllDisbursement();
                this.DisbursementGridView.DataBind();
            }
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

        protected void DisbursementGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DisbursementGridView.PageIndex = e.NewPageIndex;
            BindDisbursementGridView();
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
                        Disbursement disbursement = dm.FindDisbursementByID(disbursementID);
                        List<Disbursement> disbursements = new List<Disbursement>();
                        disbursements.Add(disbursement);

                        DisbursementDetailView.DataSource = disbursements;
                        DisbursementDetailView.DataBind();

                        List<DisbursementItem> disbursementItems
                            =
                            dm.FindDisbursementItemByCriteria(new DisbursementItemSearchDTO() {DisbursementID = disbursementID});
                        this.DisbursementItemGridView.DataSource = disbursementItems;
                        DisbursementItemGridView.DataBind();
                    }

                    break;

                case "submit":
                    int disbursementIDToSubmit = int.Parse(e.CommandArgument.ToString());
                    Response.Redirect("~/Test/TestMichaelDetail.aspx?ID=" + disbursementIDToSubmit);
                    break;;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(this.CreatedByDropDownList.SelectedValue);

            DAL.DTO.DisbursementSearchDTO criteria = new DisbursementSearchDTO();
            criteria.CreatedBy = UserID;
            using(DisbursementManager dm = new DisbursementManager())
            {
                this.DisbursementGridView.DataSource = dm.FindDisbursementByCriteria(criteria);
                this.DisbursementGridView.DataBind();

            }
        }
    }
}