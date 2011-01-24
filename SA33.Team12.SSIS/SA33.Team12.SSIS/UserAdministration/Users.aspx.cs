using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Transactions;
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
             //the code below is commented because for testing purpose it is not needed yet
             //other than that it is working fine ;)

            string userName = e.Values[0].ToString();
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
            {
                using (BLL.UserManager um = new BLL.UserManager())
                {
                    um.DeleteUser(userName);
                }
                Membership.DeleteUser(userName);
            }
        }

        protected void UserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "disable":
                    int rowID = int.Parse(e.CommandArgument.ToString());
                    string userName = UserGridView.DataKeys[rowID].Value.ToString();
                    try
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            using (BLL.UserManager um = new BLL.UserManager())
                            {
                                um.DisableUser(userName);
                            }

                            // probably good to have Membership BLL Layer to deal with this
                            MembershipUser membershipUser = Membership.GetUser(userName);
                            membershipUser.IsApproved = false;
                            Membership.UpdateUser(membershipUser);
                        }
                    }
                    catch (Exception exception)
                    {
                        this.ErrorMessage.Text = exception.Message;
                    }

                break;

                case "delete": break;
            }
        }
    }
}