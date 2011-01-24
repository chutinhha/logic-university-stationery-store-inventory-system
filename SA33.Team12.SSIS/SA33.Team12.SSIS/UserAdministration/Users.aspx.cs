using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using SA33.Team12.SSIS;

namespace SA33.Team12.SSIS.UserAdministration
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                // DataBindUserGridView();
            }
        }

        public void DataBindUserGridView()
        {
            this.UserGridView.DataSource = Membership.GetAllUsers();
            this.UserGridView.DataBind();
        }

        protected void UserGridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            // the code below is commented because for testing purpose it is not needed yet
            // other than that it is working fine ;)

            /*
            string userName = e.Values[0].ToString();
            using (BLL.UserManager um = new BLL.UserManager())
            {
                um.DeleteUser(userName);
            }
             */
        }

        protected void UserGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if ("administrator".CompareTo(e.Keys[0].ToString().ToLower()) == 0)
            {
                this.ErrorMessage.Text = "Oh, ho! You are not allow to delete the almighty Administrator account!";
                e.Cancel = true;
            }
        }

        protected void UserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower().CompareTo("disable") == 0)
            {
                int rowID = int.Parse(e.CommandArgument.ToString());
                string userName = UserGridView.DataKeys[rowID].Value.ToString();
                using (BLL.UserManager um = new BLL.UserManager())
                {
                    um.DisableUser(userName);
                }
            }
        }
    }
}