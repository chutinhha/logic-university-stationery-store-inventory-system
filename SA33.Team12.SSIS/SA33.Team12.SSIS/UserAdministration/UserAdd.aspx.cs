/***
 * Author: Naing Myo Aung (A0076803A) (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS;
using System.Transactions;

namespace SA33.Team12.SSIS.UserAdministration
{
    public partial class UserAdd : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBindMemberRoleCheckBoxList();
                DataBindDepartmentDropDownList();
            }
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // get the user entered data from textboxes
                string userName = this.UserName.Text.Trim();
                string password = this.Password.Text.Trim();
                string email = this.Email.Text.Trim();
                string firstName = this.Email.Text.Trim();
                string lastName = this.Email.Text.Trim();
                int departmentID = int.Parse(this.DepartmentDropDownList.SelectedValue.ToString());

                // use the business logic to create user account
                try
                {
                    using (BLL.UserManager um = new BLL.UserManager())
                    {                
                        // populate the data into object
                        DAL.User user = new DAL.User();
                        user.UserName = userName;
                        user.Password = password;
                        user.Email = email;
                        user.FirstName = firstName;
                        user.LastName = lastName;

                        DAL.Department department = um.GetDepartmentByID(departmentID);
                        user.Department = department;

                        using (TransactionScope ts = new TransactionScope())
                        {
                            MembershipUser membershipUser = Membership.CreateUser(user.UserName,
                                    user.Password, user.Email);

                            um.CreateUser(user);
                        }

                    }
                }
                catch (Exception exception)
                {
                    // if something is wrong, display the error message
                    this.ErrorMessage.Text = exception.Message;
                }
            }
        }

        protected void DataBindMemberRoleCheckBoxList()
        {
            this.MemberRolesRadioButtonList.DataSource = Roles.GetAllRoles();
            this.MemberRolesRadioButtonList.DataBind();
            if (this.MemberRolesRadioButtonList.Items.Count > 0)
                this.MemberRolesRadioButtonList.Items[0].Selected = true;
        }

        protected void DataBindDepartmentDropDownList()
        {
            using (BLL.UserManager um = new BLL.UserManager())
            {
                this.DepartmentDropDownList.DataValueField = "DepartmentID";
                this.DepartmentDropDownList.DataTextField = "Name";
                this.DepartmentDropDownList.DataSource = um.GetAllDepartment();
                this.DepartmentDropDownList.DataBind();
            }
        }

    }
}
