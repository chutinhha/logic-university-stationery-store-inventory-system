using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Administration
{
    public partial class MaintainDepartment : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.DynamicDataManager.RegisterControl(this.DepartmentGridView);
            this.DepartmentGridView.EnableDynamicData(typeof(Department));
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DepartmentGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList CollectionPointDropDownList =
                this.DepartmentGridView.Rows[e.RowIndex].FindControl("CollectionPointDropDownList") as DropDownList;
            e.NewValues["CollectionPointID"] = CollectionPointDropDownList.SelectedValue.ToString();
        }

        protected void DepartmentDetailsView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            DropDownList CollectionPointDropDownList =
                this.DepartmentDetailsView.FindControl("CollectionPointDropDownList") as DropDownList;
            e.Values["CollectionPointID"] = CollectionPointDropDownList.SelectedValue.ToString();
        }

    }
}