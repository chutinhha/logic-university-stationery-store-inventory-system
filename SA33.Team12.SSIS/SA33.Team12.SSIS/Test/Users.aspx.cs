using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;

namespace SA33.Team12.SSIS.Test
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                using (UserManager um = new UserManager())
                {
                  //  this.UserGridView.DataSource = um.FindUserByCriteria(new BLL.DTO.UserSearchDTO());
                  //  this.UserGridView.DataBind();
                }
            }
        }
    }
}