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
    public partial class ViewRequestHistory : System.Web.UI.Page
    {
        RequisitionManager reqManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            reqManager = new RequisitionManager();
            if (!IsPostBack)
            {
                User u = new DAL.User();
                u.UserName = "Esther";
                RequisitionSearchDTO r = new RequisitionSearchDTO();
                r.ExactDateRequested = DateTime.Now;
            
            }
            GridView1.DataSource = reqManager.GetAllRequisition(1);
            GridView1.PageSize = 10;
            DataBind();
        }
    }
}