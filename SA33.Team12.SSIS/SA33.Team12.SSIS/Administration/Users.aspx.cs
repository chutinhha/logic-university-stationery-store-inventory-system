using System;
using System.Data;
using System.Web.DynamicData;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Collections.Generic;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.UserAdministration
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            DynamicDataManager.RegisterControl(this.UserFormView);
            this.UserFormView.EnableDynamicData(typeof(User));
        }

        protected void UserFormView_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if(exception.InnerException is Exceptions.UserException)
            {
                this.ErrorMessage.Text = exception.InnerException.Message;
            }else if (exception is Exceptions.UserException)
            {
                this.ErrorMessage.Text = exception.Message;
            }
            //else
            //{
            //    Server.Transfer("~/GenericErrorPage.aspx", true);
            //}
            Server.ClearError();
        }

        protected void UserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "disable":
                    int rowID = int.Parse(e.CommandArgument.ToString());
                    int UserID = (int)UserGridView.DataKeys[rowID].Value;
                    try
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            DAL.User user = Utilities.Membership.GetUserById(UserID);
                            Utilities.Membership.DisableUser(user);
                        }
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
            else if(UserFormView.CurrentMode == FormViewMode.ReadOnly)
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
                MemebershipRoleDropDownList.Items[0].Selected = true;
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
    }
}