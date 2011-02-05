using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Handle_Request
{
    public partial class Disbursement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
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