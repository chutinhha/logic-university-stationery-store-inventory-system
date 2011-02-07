using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SA33.Team12.SSIS.Administration
{
    public partial class BlackListLogs : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            DynamicDataManager.RegisterControl(this.BlackListLogGridView);
            DynamicDataManager.RegisterControl(this.BlackListLogDetailsView);

            this.BlackListLogGridView.EnableDynamicData(typeof(BlackListLogs));
            this.BlackListLogDetailsView.EnableDynamicData(typeof(BlackListLogs));
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}