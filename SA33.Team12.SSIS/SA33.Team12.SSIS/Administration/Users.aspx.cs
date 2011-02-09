using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Administration
{
    public partial class Users : AppCode.PageBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            DynamicDataManager.RegisterControl(this.UserFormView);
            this.UserFormView.EnableDynamicData(typeof(User));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                DataBindUserGridView();
        }

        protected void DataBindUserGridView()
        {
            List<User> UserList = null;

            using (UserManager um = new UserManager())
                if (Utilities.Membership.IsAdmin)
                {
                    UserList = um.GetAllUsers();
                }
                else if (Utilities.Membership.IsDeptHead || Utilities.Membership.IsTempDeptHead)
                {
                    UserList = um.FindUsersByCriteria(
                        new UserSearchDTO() { DepartmentID = Utilities.Membership.LoggedInuser.DepartmentID });
                }
                else
                {
                    UserList = um.FindUsersByCriteria(
                        new UserSearchDTO() { UserID = Utilities.Membership.LoggedInuser.UserID });
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


                CheckBoxList MembershipRoleCheckBoxList =
                 UserFormView.FindControl("MembershipRoleCheckBoxList") as CheckBoxList;
                string roles = string.Empty;
                foreach (ListItem item in MembershipRoleCheckBoxList.Items)
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

                CheckBoxList MembershipRoleCheckBoxList =
                 UserFormView.FindControl("MembershipRoleCheckBoxList") as CheckBoxList;
                string roles = string.Empty;
                foreach (ListItem item in MembershipRoleCheckBoxList.Items)
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
            if (this.UserFormView.CurrentMode == FormViewMode.Edit)
            {
                CheckBoxList MembershipRoleCheckBoxList =
                    UserFormView.FindControl("MembershipRoleCheckBoxList") as CheckBoxList;
                if (MembershipRoleCheckBoxList != null)
                {
                    MembershipRoleCheckBoxList.DataSource = Roles.GetAllRoles();
                    MembershipRoleCheckBoxList.DataBind();
                    string[] roles =
                        DataBinder.Eval(UserFormView.DataItem, "Role").ToString().Split(new string[] {","},
                                                                                        StringSplitOptions.
                                                                                            RemoveEmptyEntries);
                    foreach (ListItem item in MembershipRoleCheckBoxList.Items)
                        foreach (string role in roles)
                            if (item.Text == role) item.Selected = true;


                    foreach (ListItem item in MembershipRoleCheckBoxList.Items)
                    {
                        if (Utilities.Membership.IsDeptHead)
                        {
                            if (item.Text == "Administrators") item.Enabled = false;
                            if (item.Text == "DepartmentHeads") item.Enabled = false;
                            if (item.Text.Contains("Store")) item.Enabled = false;
                        }
                    }
                }
            }
            if (this.UserFormView.CurrentMode == FormViewMode.Insert)
            {
                CheckBoxList MembershipRoleCheckBoxList =
                    UserFormView.FindControl("MembershipRoleCheckBoxList") as CheckBoxList;
                if (MembershipRoleCheckBoxList != null)
                {
                    MembershipRoleCheckBoxList.DataSource = Roles.GetAllRoles();
                    MembershipRoleCheckBoxList.DataBind();
                    foreach (ListItem item in MembershipRoleCheckBoxList.Items)
                    {
                        if (Utilities.Membership.IsDeptHead)
                        {
                            if (item.Text == "Administrators") item.Enabled = false;
                            if (item.Text == "DepartmentHeads") item.Enabled = false;
                            if (item.Text.Contains("Store")) item.Enabled = false;
                        }
                    }
                }
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
            this.DataBindUserGridView();
        }

        protected void UserFormView_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            DataBindUserGridView();
        }
    }
}