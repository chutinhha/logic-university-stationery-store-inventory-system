using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Administration
{
    public partial class UpdateCollectionPoint : AppCode.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                DataBindDepartmentDetailView();
            }
        }

        protected void DataBindDepartmentDetailView()
        {
            User loggedInUser = Utilities.Membership.GetCurrentLoggedInUser();
            string[] roles = Utilities.Membership.GetCurrentLoggedInUserRole();

            List<Department> departments = new List<Department>() { loggedInUser.Department };
            this.DepartmentDetailView.DataSource = departments;
            this.DepartmentDetailView.DataBind();
        }

        protected void UpdateCollectionPointButton_Click(object sender, EventArgs e)
        {
            try
            {
                int departmentID = (int) this.DepartmentDetailView.DataKey.Value;
                DropDownList CollectionPointDropDownList =
                    this.DepartmentDetailView.FindControl("CollectionPointDropDownList") as DropDownList;
                using (UserManager um = new UserManager())
                {
                    Department department = um.GetDepartmentByID(departmentID);
                    department.CollectionPointID = Convert.ToInt32(CollectionPointDropDownList.SelectedValue);
                    um.UpdateDepartment(department);
                }
                DataBindDepartmentDetailView();
            }
            catch (Exception exception)
            {
                this.ErrorMessage.Text = exception.Message;
            }
        }
    }
}