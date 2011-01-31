using System;
using System.Data;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Collections.Generic;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.UserAdministration
{
    public partial class Users : System.Web.UI.Page
    {
        private DropDownList departmentDropDownList;

        protected void Page_Init(object sender, EventArgs e)
        {
            DynamicDataManager.RegisterControl(this.UserFormView);
            this.UserFormView.EnableDynamicData(typeof(User));
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

        protected void UserGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void DepartmentDropDownList_Init(object sender, EventArgs e)
        {
            departmentDropDownList = sender as DropDownList;
        }


        protected void UserDetailView_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            var departmentID = Convert.ToInt32(departmentDropDownList.SelectedValue);
            e.NewValues["DepartmentID"] = departmentID;
        }

        protected void UserDetailView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            var departmentID = Convert.ToInt32(departmentDropDownList.SelectedValue);
            e.Values["DepartmentID"] = departmentID;
        }

        protected void UserFormView_DataBinding(object sender, EventArgs e)
        {

        }

        protected void UserFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            FormView userFormView = sender as FormView;
            if (UserFormView != null)
            {
                DropDownList departmentDropDownList =
                    userFormView.FindControl("DepartmentDropDownList") as DropDownList;

                e.Values["DepartmentID"] = departmentDropDownList.SelectedValue.ToString();
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
            }
        }

    }
}