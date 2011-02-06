using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Administration
{
    public partial class UpdateCollectionPoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                User loggedInUser = Utilities.Membership.GetCurrentLoggedInUser();
                string[] roles = Utilities.Membership.GetCurrentLoggedInUserRole();

                List<Department> departments = new List<Department>() {loggedInUser.Department};
                this.DepartmentDetailView.DataSource = departments;
                this.DepartmentDetailView.DataBind();
            }
        }
    }
}