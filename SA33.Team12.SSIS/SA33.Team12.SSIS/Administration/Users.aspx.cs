using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.UserAdministration
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            DynamicDataManager.RegisterControl(this.UserFormView);
            this.UserFormView.EnableDynamicData(typeof(User));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
                DataBindUserGridView();
        }
        protected void DataBindUserGridView()
        {
            DAL.User loggedInUser = Utilities.Membership.GetCurrentLoggedInUser();
            string[] roles = Utilities.Membership.GetCurrentLoggedInUserRole();

            List<User> UserList = null;

            var isAdmin = (from r in roles
                           where r.Contains("Administrators")
                           select r);
            var isDeptHead = (from r in roles
                              where r.Contains("DepartmentHeads") || r.Contains("TemporaryDepartmentHeads")
                              select r);
            using (UserManager um = new UserManager())
                if (isAdmin.Count() > 0)
                {
                    UserList = um.GetAllUsers();
                }
                else if (isDeptHead.Count() > 0)
                {
                    UserList = um.FindUsersByCriteria(
                        new UserSearchDTO() { DepartmentID = loggedInUser.DepartmentID });
                }
                else
                {
                    UserList = um.FindUsersByCriteria(
                        new UserSearchDTO() { UserID = loggedInUser.UserID });
                }
            this.UserGridView.DataSource = UserList;
            this.UserGridView.DataBind();
        }

        protected void UserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int UserID = int.Parse(e.CommandArgument.ToString());
            switch (e.CommandName.ToLower())
            {
                case "disable":
                    try
                    {
                        DAL.User user = Utilities.Membership.GetUserById(UserID);
                        Utilities.Membership.DisableUser(user);
                    }
                    catch (Exception exception)
                    {
                        this.ErrorMessage.Text = exception.Message;
                    }

                    break;
                case "enable":
                    try
                    {
                        DAL.User user = Utilities.Membership.GetUserById(UserID);
                        Utilities.Membership.EnableUser(user);
                    }
                    catch (Exception exception)
                    {
                        this.ErrorMessage.Text = exception.Message;
                    }

                    break;
                case "edit":

                    break;
                case "delete": break;
            }
            DataBindUserGridView();
        }

        protected void UserFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            FormView userFormView = sender as FormView;
            if (UserFormView != null)
            {
                DropDownList departmentDropDownList =
                    userFormView.FindControl("DepartmentDropDownList") as DropDownList;

                e.Values["DepartmentID"] = departmentDropDownList.SelectedValue.ToString();

                DropDownList MemebershipRoleDropDownList =
                 UserFormView.FindControl("MemebershipRoleDropDownList") as DropDownList;
                string roles = string.Empty;
                foreach (ListItem item in MemebershipRoleDropDownList.Items)
                {
                    if (item.Selected) roles += item.Value + ",";
                }
                e.Values["Role"] = roles;
            }
        }

        protected void UserFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            FormView userFormView = sender as FormView;
            if (UserFormView != null)
            {
                DropDownList departmentDropDownList =
                    userFormView.FindControl("DepartmentDropDownList") as DropDownList;
                e.NewValues["DepartmentID"] = departmentDropDownList.SelectedValue.ToString();

                DropDownList MemebershipRoleDropDownList =
                  UserFormView.FindControl("MemebershipRoleDropDownList") as DropDownList;
                string roles = string.Empty;
                foreach (ListItem item in MemebershipRoleDropDownList.Items)
                {
                    if (item.Selected) roles += item.Value + ",";
                }
                e.NewValues["Role"] = roles;
            }
        }

        protected void UserFormView_DataBound(object sender, EventArgs e)
        {
            if (UserFormView.CurrentMode == FormViewMode.Edit || UserFormView.CurrentMode == FormViewMode.Insert)
            {
                DataBindMemebershipDropDownList();
            }
            else if (UserFormView.CurrentMode == FormViewMode.ReadOnly)
            {

            }
        }

        protected void DataBindMemebershipDropDownList()
        {
            DropDownList MemebershipRoleDropDownList =
                UserFormView.FindControl("MemebershipRoleDropDownList") as DropDownList;
            if (MemebershipRoleDropDownList != null)
            {
                MemebershipRoleDropDownList.DataSource = Roles.GetAllRoles();
                MemebershipRoleDropDownList.DataBind();
                string role =
                    DataBinder.Eval(UserFormView.DataItem, "Role").ToString().Split(new string[] { "," },
                                                                                    StringSplitOptions.
                                                                                        RemoveEmptyEntries)[0].ToString();
                MemebershipRoleDropDownList.SelectedValue = role;
            }
        }

        protected void UserFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            switch (e.CommandArgument.ToString().ToLower())
            {
                case "new":
                    break;
                case "insert":
                    break;
                case "edit":
                    break;
                case "update":
                    break;
                case "delete":
                    break;
                case "cancel":
                    break;
                default: break;
            }
        }

        protected void UserGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UserGridView.PageIndex = e.NewPageIndex;
        }

        protected void UserFormView_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            DataBindUserGridView();
        }
    }
}