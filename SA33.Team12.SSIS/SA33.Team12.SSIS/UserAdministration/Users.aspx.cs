using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Collections.Generic;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.UserAdministration
{
    public partial class Users : System.Web.UI.Page
    {
        private Literal departmentLiteral;
        private DropDownList departmentDropDownList;

        protected void Page_Init(object sender, EventArgs e)
        {
            DynamicDataManager.RegisterControl(this.UserDetailView);
            this.UserDetailView.EnableDynamicData(typeof(User));
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
            UserDetailView.DataBind();
        }

        protected void DepartmentDropDownList_Init(object sender, EventArgs e)
        {
            departmentDropDownList = sender as DropDownList;
        }

        protected void DepartmentLiteral_Init(object sender, EventArgs e)
        {
            departmentLiteral = sender as Literal;
        }

        protected void UserDetailView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            var departmentID = Convert.ToInt32(departmentDropDownList.SelectedValue);
            e.Values["DepartmentID"] = departmentID;
        }

        protected void UserDetailView_DataBound(object sender, EventArgs e)
        {
            if(UserDetailView.CurrentMode == DetailsViewMode.ReadOnly)
            {
                List<DAL.User> users = UserDetailObjectDataSource.Select() as List<User>;
                if (users != null && users.Count > 0)
                {
                    DAL.User user = users[0];
                    Literal departmentLiteral = UserDetailView.FindControl("DepartmentLiteral") as Literal;
                    if (departmentLiteral != null)
                        departmentLiteral.Text = user.Department.Name;
                }
            }
        }

        protected void UserDetailView_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            var departmentID = Convert.ToInt32(departmentDropDownList.SelectedValue);
            e.NewValues["DepartmentID"] = departmentID;
        }

    }
}