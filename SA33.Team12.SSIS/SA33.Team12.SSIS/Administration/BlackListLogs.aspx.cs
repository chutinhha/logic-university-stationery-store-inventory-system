using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Administration
{
    public partial class BlackListLogs : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                DataBindDepartmentDropDownList();
                DataBindBlackListLogGridView(new BlackListLogSearchDTO() {BlackListLogID = 0});
            }
        }

        protected void DataBindBlackListLogGridView(BlackListLogSearchDTO criteria)
        {
            using (UserManager um = new UserManager())
            {
                List<DAL.BlacklistLog> blacklistLogs = um.FindBlacklistLogByCriteria(criteria);
                this.BlackListLogGridView.DataSource = blacklistLogs;
                this.BlackListLogGridView.DataBind();
            }
        }

        protected void DataBindDepartmentDropDownList()
        {
            using (UserManager um = new UserManager())
            {
                List<Department> departments = um.GetAllDepartments();
                this.DepartmentDropDownList.DataSource = departments;
                this.DepartmentDropDownList.DataBind();
                this.DepartmentDropDownList.Items.Insert(0, new ListItem("All Department", "0"));
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                BlackListLogSearchDTO criteria = new BlackListLogSearchDTO();
                DateTime StartDate = DateTime.MinValue;
                DateTime EndDate = DateTime.MaxValue;
                if (StartBlackListedDateTextBox.Text.Trim() != "")
                    StartDate = DateTime.Parse(StartBlackListedDateTextBox.Text);
                if (EndBlackListedDateTextBox.Text.Trim() != "")
                    EndDate = DateTime.Parse(EndBlackListedDateTextBox.Text);

                criteria.DepartmentID = Convert.ToInt32(this.DepartmentDropDownList.SelectedValue);
                criteria.StartDateBlackListed = StartDate;
                criteria.EndDateBlackListed = EndDate;
                DataBindBlackListLogGridView(criteria);
            }
            catch (FormatException)
            {
                this.ErrorMessage.Text = "Please enter valid date.";
            }
        }

        protected void BlackListLogGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.BlackListLogGridView.PageIndex = e.NewPageIndex;
            SearchButton_Click(null, null);
        }
    }
}