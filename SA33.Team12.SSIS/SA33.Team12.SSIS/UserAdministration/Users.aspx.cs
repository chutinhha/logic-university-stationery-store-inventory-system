using System;
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
            DynamicDataManager.RegisterControl(this.UserDetailView);
            this.UserDetailView.EnableDynamicData(typeof (User));
            //this.UserGridView.EnableDynamicData(typeof(User)););

            if (!Page.IsPostBack)
            {
                // DataBindUserGridView();
            }
        }


        protected void UserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "disable":
                    int rowID = int.Parse(e.CommandArgument.ToString());
                    int UserID = (int) UserGridView.DataKeys[rowID].Value;
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
    }
}