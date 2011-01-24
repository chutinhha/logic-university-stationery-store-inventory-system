using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Test
{
    public partial class Stationeries : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            DynamicDataManager.RegisterControl(StationeryGridView);

            StationeryGridView.EnableDynamicData(typeof(Stationery));
        }
    }
}