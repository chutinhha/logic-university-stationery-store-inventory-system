using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Test
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadUserGridView();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // Create filter
            UserSearchDTO criteria = new UserSearchDTO();

            // set filter from ui controls
            criteria.FirstName = this.FirstNameTextBox.Text;
            criteria.LastName = this.LastNameTextBox.Text;
            using (UserManager um = new UserManager())
            {
                // get users by fileter
                List<User> users = um.FindUsersByCriteria(criteria);
                this.UserGridView.DataSource = users;
                this.UserGridView.DataBind();

            }
        }

        protected void LoadUserGridView()
        {
            using (UserManager um = new UserManager())
            {
                List<User> users = um.FindUsersByCriteria(new UserSearchDTO());
                this.UserGridView.DataSource = users;
                this.UserGridView.DataBind();
            }
        }

        protected void ShowAllButton_Click(object sender, EventArgs e)
        {
            this.FirstNameTextBox.Text = "";
            this.LastNameTextBox.Text = "";
            LoadUserGridView();
        }
    }
}